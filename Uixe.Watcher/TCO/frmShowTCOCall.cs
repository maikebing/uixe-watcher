using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher
{

    public partial class frmShowTCOCall : XtraForm
    {
        private readonly T_Plaza _plaza;
        internal RuntimeSetting _runtimeSetting;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        public TCOCall TCOCallxxx { get; set; }
       
        public frmShowTCOCall(frmPlaza owner, T_Plaza plaza) : this()
        {
            
            if (!DesignMode && !Disposing)
            {
                _plaza = plaza;
                _runtimeSetting = owner._runtimeSetting;
                _logger = owner._logger;
                _cache = owner._cache;
                try
                {
                    this.tsTabs.TabPages.Clear();
                    for (int i = 0; i < _plaza.Lanes.Count; i++)
                    {
                        string pname = _plaza.Id + _plaza.Lanes[i].LaneNo;
                        XtraTabPage t = new XtraTabPage();
                        TCOConfirm tms = new TCOConfirm();
                        tms.Plaza = _plaza;
                        tms.Lane = _plaza.Lanes[i];
                        tms.Owner = owner;
                        t.Name = pname;
                        tms.Name = pname;
                        tms.Parent = t;//由于在现实数据时使用到TabPage 在给TCO属性赋值前必须赋值Parent
                        tms.Dock = DockStyle.Fill;
                        t.Controls.Add(tms);
                        t.Tag = tms;
                        tsTabs.TabPages.Add(t);
                        t.PageVisible = false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"初始化监控确认窗口失败{_plaza?.Id}");
                }
            }
        }

        public frmShowTCOCall()
        {
            InitializeComponent();
        }
  

        public new void Show()
        {
            string pname = TCOCallxxx.ID;
            var x = from p in tsTabs.TabPages where p.Name == pname select p;
            if (x.Any() && x.Single() != null)
            {
                XtraTabPage t = x.Single();
                TCOConfirm tms = (TCOConfirm)t.Tag;
                try
                {
                    tms.Show(TCOCallxxx);
                    t.Text = string.Format("车道{0}", TCOCallxxx.LaneNo);
                    tms.btnOK.Click += BtnOK_Click;
                    tms.btnCancel.Click += btnCancel_Click;
                    tsTabs.SelectedTabPage = t;
                    tms.Reset();
                }
                catch (Exception ex)
                {
                    tms.btnOK.Click -= BtnOK_Click;
                    tms.btnCancel.Click -= btnCancel_Click;
                    Console.WriteLine($"Show{ex.Message}");
                }
                try
                {
                    t.PageVisible = true;
               
                    base.Show();
                    this.Focus();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"ERRORTCOCALL_SHOW:<{ex.Message}>\r\n{TCOCallxxx}\r\n");
                }
            }
            else
            {
                _logger.LogWarning($"内部错误{pname}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(SimpleButton) && tsTabs.SelectedTabPage != null && ((SimpleButton)sender).Parent != null && ((SimpleButton)sender).Parent.Name == tsTabs.SelectedTabPage.Name)
            {
                if (this.tsTabs.SelectedTabPage.Tag is TCOConfirm tms && tms.CanDo)
                {
                    tms.btnOK.Click -= BtnOK_Click;
                    tms.btnCancel.Click -= btnCancel_Click;
                    Submit(false);
                    RemoveNowTab();
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
           if (tsTabs.SelectedTabPage == null)
            {
                _logger.LogWarning($"选择的选项卡为空，无法继续。");
            }
           else if (sender != null && sender.GetType() == typeof(SimpleButton) && ((SimpleButton)sender).Parent != null && ((SimpleButton)sender).Parent.Name == tsTabs.SelectedTabPage.Name)
            {
                TCOConfirm tms = this.tsTabs.SelectedTabPage.Tag as TCOConfirm;
                if (tms != null)
                {
                    if (!tms.CheckPlazaInfo())
                    {
                        _logger.LogWarning($"收费站没有选择， 需要选择。 ");
                        tms.ShowPlazaPopup();
                    }
                    else
                    {
                        _logger.LogInformation($"开始提交确认 ");
                        tms.btnCancel.Enabled = false;
                        tms.btnOK.Enabled = false;
                        var result = Submit(true);
                        if (result)
                        {
                            tms.btnOK.Click -= BtnOK_Click;
                            tms.btnCancel.Click -= btnCancel_Click;
                            RemoveNowTab();
                        }
                        else
                        {
                            tms.btnCancel.Enabled = true;
                            tms.btnOK.Enabled = true;
                        }

                    }
                }
                else
                {
                    _logger.LogWarning($"提交确认时TAG无法转换为TCOConfirm ");
                }
            }
           else
            {
                _logger.LogWarning($"选择的选项卡为{tsTabs.SelectedTabPage.Name}条件不一致，无法继续。");
            }
        }

        private bool Submit(bool ok)
        {
            bool result = false;
            if (this.tsTabs.SelectedTabPage.Tag is TCOConfirm tms)
            {
                var aus = tms.GetAUS(ok);
                var task= Task.Run(async () =>
                {
                    try
                    {
                        var apiResult    = await tms.Lane.TCO_Confirm(aus);
                        _logger.LogInformation($"提交了{JsonConvert.SerializeObject(aus)}, 返回{JsonConvert.SerializeObject(apiResult)}");
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,$"提交失败");
                    }
                });
                task.Wait(TimeSpan.FromSeconds(10));
                _logger.LogInformation($"提交任务IsCompletedSuccessfully?{task.IsCompletedSuccessfully} IsCompleted:{task.IsCompleted} IsFaulted:{task.IsFaulted} IsCanceled:{task.IsCanceled} Exception:{task.Exception?.InnerException?.Message}");
            }
            else
            {
                _logger.LogWarning($"提交确认时TAG无法转换为TCOConfirm ");
            }
            return result;
        }

        private void RemoveNowTab()
        {
            if (tsTabs.SelectedTabPage != null)
            {
                tsTabs.SelectedTabPage.PageVisible = false;
            }
            var s = from p in tsTabs.TabPages where p.PageVisible == true select p;
            if (!s.Any())
            {
                this.Visible = false;
            }
        }

        private void frmShowTCOCall_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}