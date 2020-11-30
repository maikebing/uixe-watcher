using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraEditors;
using Uixe.Watcher.Controls;
using System.Threading.Tasks;
using Renci.SshNet;

namespace Uixe.Watcher
{
    public partial class frmTimeSync : XtraForm
    {
        #region 加载窗体和车道信息列表

        public frmTimeSync()
        {
            InitializeComponent();
        }

        private void frmTimeSync_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.LOGO;
            var tmlLaneNo = Uitls.TollInfo.GetTollInfo();
            tmlLaneNo.lanes.ForEach(dt =>
            {
                laneTimeBindingSource.Add(new LaneTime() { LaneName = dt.lane_id });
            });
        }

        #endregion 加载窗体和车道信息列表

        #region 用于设置指定车道的时间   支持多线程

        public void SetTime(string plaza , string laneno, string clienttime)
        {
            this.Invoke((MethodInvoker)delegate
            {
                cansynctime = true;
                try
                {
                    string key = plaza + laneno;
                    gvLane.SetRowCellValue(gvLane.LocateByDisplayText(gvLane.GetRowHandle(0), colLaneName, key), colLaneDateTime, clienttime);
                }
                catch (Exception)
                {
                }
            });
        }

        private bool cansynctime = true;

        #endregion 用于设置指定车道的时间   支持多线程

        #region 发送指令

        /// <summary>
        /// 要求所有车道把自己的时间告诉监控程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetAllClientTime_Click(object sender, EventArgs e)
        {
            if (cansynctime)
            {
                cansynctime = false;
            }
        }

        private static void CheckTime()
        {
            
        }

        public static void SyncTime()
        {
            try
            {
              ;
            }
            catch (Exception)
            {
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            CheckTime();
            if (cansynctime)
            {
                txtOut.Text = "正在发送指令" + System.Environment.NewLine;
                cansynctime = false;
            
            }
        }

        #endregion 发送指令

        private void tmSync_Tick(object sender, EventArgs e)
        {
        }

        private void chkAutoSync_CheckedChanged(object sender, EventArgs e)
        {
            this.tmSync.Enabled = (this.tmSync.Enabled == false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void btnSetTime_Click(object sender, EventArgs e)
        {
            try
            {
            
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public async void RunCommand(string ip)
        {
            await Task.Factory.StartNew((Action)delegate
            {
                try
                {
                    try
                    {
                        this.Invoke((Action)delegate
                        {
                            txtOut.Text += "正同步" + ip + Environment.NewLine;
                            txtOut.SelectionStart = txtOut.Text.Length;
                            txtOut.ScrollToCaret();
                        });
                    }
                    catch (Exception)
                    {
                    }
                    SshClient ssh = new SshClient(ip, "root", "future");
                    ssh.Connect();
                    if (ssh.IsConnected)
                    {
                        SshCommand sc = ssh.CreateCommand(string.Format("date  \"+%Y-%m-%d %H:%M:%S\"    -s \"{0}\"", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                        string s = sc.Execute();
                        this.Invoke((MethodInvoker)delegate
                        {
                            try
                            {
                                txtOut.Text +=ip+ ":"+ s.Replace("\n", "\r\n") + Environment.NewLine;
                                txtOut.SelectionStart = txtOut.Text.Length;
                                txtOut.ScrollToCaret();
                            }
                            catch (Exception)
                            {
                            }
                        });
                        ssh.Disconnect();
                        ssh.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            txtOut.Text += ip +":"+ex.Message+Environment.NewLine ;
                            txtOut.SelectionStart = txtOut.Text.Length;
                            txtOut.ScrollToCaret();
                        });
                    }
                    catch (Exception)
                    {
                    }
                }
            });
        }
    }
}