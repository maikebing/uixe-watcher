using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher.WinForms
{
    public partial class frmConfirmEnInfo : DevExpress.XtraEditors.XtraForm
    {
        public frmConfirmEnInfo()
        {
            InitializeComponent();
        }
        private UixeClient _client = new UixeClient();
        private T_Plaza _plaza;
        private T_Lane _lane;
        private IMemoryCache _cache;
        private ConfirmEnInfo _enInfo;

        public void ShowConfirmEnInfoAsync(ConfirmEnInfo enInfo, frmPlaza _fPlaza)
        {
            _enInfo = enInfo;
            _plaza = _fPlaza.Boss.Plazas.FirstOrDefault(p => p.Id == enInfo.plazaId);
            _lane = _plaza?.Lanes?.FirstOrDefault(l => l.LaneNo == enInfo.laneNo);
            if (_plaza != null && _lane != null)
            {
                _plaza = _fPlaza.GetPlaza(enInfo.plazaId);
                _cache = _fPlaza._cache;

                pLazaBindingSource.DataSource = new List<ProvPlazaInfo>();
                pLazaBindingSource.Position = -1;
                pLazaBindingSource.ResetBindings(false);
                pLazaBindingSource.ResetCurrentItem();
                keyItem_Vehicle_Types_BindingSource.DataSource = KeyItem.GetVEHICLE_TYPES();

                cbxProv.EditValue = 65;
                pLazaBindingSource.ResetCurrentItem();

                confirmEnInfoBindingSource.DataSource = enInfo;
                if (enInfo.resCount > 0)
                {
                    enStationsBindingSource.DataSource = enInfo.enStations;
                    enStationsBindingSource.ResetCurrentItem();
                }
                else
                {
                    enStationsBindingSource.DataSource = new EnStations()
                    {
                        enDateTime = DateTime.Now.AddDays(-1),
                        mediaType = 9,
                        mediaNo = "030",
                        resultVoucher = 2
                    };
                    _enInfo.retQuery = 2;
                }
                this.Show();
            }
            else
            {
                _fPlaza.Alert("运维故障", $"车道{enInfo.laneId}的入口查询确认未能正常显示");
                this.Close();

            }

        }


        private async void enStationsBindingSource_PositionChanged(object sender, EventArgs e)
        {
            var _enPlaza = enStationsBindingSource.Current as EnStations;
            if (_enPlaza != null)
            {
                _pbindingSource1.DataSource = await _cache.GetOrCreate(_plaza.Ip, async c => await _client.GetProvCodes(_plaza.Ip));

                var pc = await _cache.GetOrCreate($"{_plaza.Ip}|{_enPlaza.enStationId}", async c => await _client.GetProvByPlaza(_plaza.Ip, _enPlaza.enStationId));
                if (pc != null)
                {
                    repositoryItemLookUpEdit1.DataSource = pc;
                    cbxProv.EditValue = int.Parse(pc);
                    var prov = cbxProv.GetSelectedDataRow() as ProvCode;
                    var ppi = await _cache.GetOrCreate($"{_plaza.Ip}|{prov?.provId ?? 65}", async c => await _client.GetProvPlazaInfo(_plaza.Ip, $"{prov?.provId ?? 65}"));
                    if (ppi != null)
                    {
                        pLazaBindingSource.DataSource = ppi;
                        var r = from p in ppi where p.plazaId == _plaza.StationId select p;
                        if (r.Any())
                        {
                            string psn = r.FirstOrDefault().plazaName;
                            pLazaBindingSource.Position = pLazaBindingSource.IndexOf(r.FirstOrDefault());
                        }
                    }
                }
            }
        }
        private void StopCall()
        {
            btnOk.Enabled = true;
            btnCancel.Enabled = true;
            mpPorgress.Properties.Stopped = true;
        }

        private void StartCall()
        {
            btnOk.Enabled = false;
            btnCancel.Enabled = false;
            mpPorgress.Visible = true;
            mpPorgress.Properties.Stopped = false;
        }
        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (_lane != null)
            {
                StartCall();
                try
                {
                    var _enPlaza = enStationsBindingSource.Current as EnStations;
                    if (_enPlaza != null && !string.IsNullOrEmpty(_enPlaza.enStationId))
                    {
                        var result = await _lane.SendMsg("toll/confirm_online_entry_info",
                                                    new TCOConfirmEntryInfo(
                                                        _enPlaza.cardId,
                                                        _enPlaza.enStationId,
                                                        _enPlaza.enTime,
                                                        _enPlaza.enTollLaneId,
                                                        _enPlaza.mediaNo,
                                                        _enPlaza.mediaType,
                                                        _enPlaza.resultVoucher,
                                                        enStationsBindingSource.Position,
                                                         _enInfo.retQuery
                            ));

                        this.Invoke(new Action(() =>
                        {

                            if (result != null && result.code == 0)
                            {
                                this.Close();
                            }
                            else
                            {
                                StopCall();
                                mpPorgress.Text = $"{result.code}-{result.msg}";
                            }
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {

                            StopCall();
                            mpPorgress.Text = $"无入口信息,点取消收费员重新发起， 或者联系运维人员查询入口";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        mpPorgress.Text = ex.Message;
                        StopCall();
                    }));
                }

            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    mpPorgress.Text = "车道号为空， 无法下一步。";
                    StopCall();
                }));
            }
        }

        private async void cbxProv_EditValueChanged(object sender, EventArgs e)
        {
            var prov = cbxProv.GetSelectedDataRow() as ProvCode;

            var ppi = await _cache.GetOrCreate($"{_plaza.Ip}|{prov?.provId ?? 65}", async c => await _client.GetProvPlazaInfo(_plaza.Ip, $"{prov?.provId ?? 65}"));
            if (ppi != null)
            {
                pLazaBindingSource.DataSource = ppi;
            }
        }
    }
}