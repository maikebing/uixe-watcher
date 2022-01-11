using System;
using System.Data;
using System.Linq;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher
{
    public partial class TCOConfirm : DevExpress.XtraEditors.XtraUserControl
    {
        public TCOConfirm()
        {
            InitializeComponent();
        }

        public TCOCall TCE { get; set; }
        public bool CanDo { get; set; }
        public Lane Lane { get; set; }
        public Plaza Plaza {get;set;}
        public bool CheckPlazaInfo()
        {
            bool ret = true;
            if (TCE.TCOTYPE == WATCHER_TYPE.WATCHER_UnKnowPlaza)
            {
                var ppi = cbxModifyEntryPlaza.GetSelectedDataRow() as ProvPlazaInfo;
                if (ppi == null || string.IsNullOrEmpty(ppi.plazaId))
                {
                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }
            return ret;
        }
  
        private UixeClient uixeClient = new UixeClient();

        public void Show(TCOCall tce)
        {
            TCE = tce;
            IsShowed = false;
            InitInfo();
            _tce = tce.Clone();
            if (tce.TCOTYPE == WATCHER_TYPE.WATCHER_BlacklistPlate)
            {
                tcoPictureBox1.Visible = true;
            }
            else
            {
                tcoPictureBox1.Visible = false;
            }

            var l =  Plaza.lanes.FirstOrDefault(f => f.lane_no == tce.LaneNo);
            string url = string.Format($"http://{l.ip}:10000/capture");
            Lane = l;
            tcoPictureBox1.ImageLocation = url;
            tcoPictureBox1.ImageLocation = url;
            keyItem_Vehicle_Types_BindingSource.DataSource = KeyItem.GetVEHICLE_TYPES();
            _pbindingSource1.DataSource = uixeClient.GetProvCodes(Plaza.ip);
            _pbindingSource1.ResetCurrentItem();
            cbxProv.EditValue = 65;
            pLazaBindingSource.ResetCurrentItem();
            //tCOCallBindingSource.ResetBindings(false);
            tCOCallBindingSource.ResetCurrentItem();

            FillPlazaNameAndList(tce, true);
            tCOCallBindingSource.DataSource = TCE;
            chkCarKind.Checked = tce.DifClass != 0;
            chkCarPlate.Checked = tce.DifPlate;
            chkCarType.Checked = tce.DifType;
            chkEntryPlaza.Text = tce.DifPlaza ? "入口站不明" : "入口正常卡";
            chkEntryPlaza.Checked = tce.DifPlaza;
            chkIsU.Checked = tce.UCar;
            chkTimeoutCar.Checked = tce.TimeoutCar != 0;
            switch (tce.DifClass)
            {
                case 2:
                    chkCarKind.Visible = true;
                    chkCarKind.Text = "无卡公务";
                    chkCarKind.Checked = true;
                    break;

                case 3:
                    chkCarKind.Visible = true;
                    chkCarKind.Text = "绿色通道";
                    chkCarKind.Checked = true;
                    break;

                case 4:
                    chkCarKind.Visible = true;
                    chkCarKind.Text = "农用车";
                    chkCarKind.Checked = true;
                    break;

                default:
                    chkCarKind.Visible = false;
                    break;
            }
            switch (tce.TimeoutCar)
            {
                case 1:
                    chkTimeoutCar.Text = "超时车";
                    break;

                case 2:
                    chkTimeoutCar.Text = "超速车";
                    break;
            }
            txtcarnumber.Text = tce.ExitPlate;
            txtocartype.Text = tce.ExitCarType;

            switch (tce.TCOTYPE)
            {
                case WATCHER_TYPE.WATCHER_MenuCardBox://WATCHER_MenuCardBox
                    gcCardInfo.Enabled = false;
                    break;

                default:
                    break;
            }
            if (string.IsNullOrEmpty(cbxModifyEntryPlaza.EditValue as string))
            {
                if (string.IsNullOrEmpty(txtentrysite.Text))
                {
                    cbxModifyEntryPlaza.EditValue = txtentrysite.Text;
                }
                else if (string.IsNullOrEmpty(txtexitsite.Text))
                {
                    cbxModifyEntryPlaza.EditValue = txtexitsite.Text;
                }
            }
            IsShowed = true;
        }

        private bool IsShowed = false;

        private void InitInfo()
        {
            try
            {
                tcoPictureBox1.Image = null;
                chkTimeoutCar.Checked = false;

                chkIsU.Checked = false;
                chkCarType.Checked = false;
                chkCarPlate.Checked = false;
                chkEntryPlaza.Checked = false;
                chkCarKind.Checked = false;

                tCOCallBindingSource.DataSource = null;
                tCOCallBindingSource.Clear();
                pLazaBindingSource.Clear();
                pLazaBindingSource.Position = -1;
                pLazaBindingSource.ResetBindings(false);
                pLazaBindingSource.ResetCurrentItem();

                tCOCallBindingSource.ResetBindings(false);
                tCOCallBindingSource.ResetCurrentItem();
                gcCardInfo.Enabled = true;
            }
            catch (Exception)
            {
            }
        }

        public void ShowPlazaPopup()
        {
            cbxModifyEntryPlaza.ShowPopup();
        }

        public void Reset()
        {
            this.pcPronow.Properties.Maximum = TCE.TimeOut;
            pcPronow.Position = TCE.TimeOut;
            timer1.Start();
            AUS = new MSG_TCOConfirm();
            CanDo = true;
            btnOK.Enabled = true;
        }

        private async void FillPlazaNameAndList(TCOCall tce, bool isfirst = false)
        {
            try
            {
                UixeClient client = new UixeClient();
                if (isfirst)
                {
                    var pc = await client.GetProvByPlaza(Plaza.ip, tce.EntryStationID);
                    cbxProv.EditValue = int.Parse(pc);
                }
                var prov = cbxProv.GetSelectedDataRow() as ProvCode;

                var ppi = await client.GetProvPlazaInfo(Plaza.ip, $"{prov?.provId ?? 65}");
                if (ppi != null)
                {
                    pLazaBindingSource.DataSource = ppi;
                    if (isfirst)
                    {
                        var r = from p in ppi where p.plazaId == tce.EntryStationID select p;
                        if (r.Any())
                        {
                            string psn = r.FirstOrDefault().plazaName;
                            pLazaBindingSource.Position = pLazaBindingSource.IndexOf(r.FirstOrDefault());
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }

        private void checkEdits_CheckedChanged(object sender, EventArgs e)
        {
            //CheckEdit ce = sender as CheckEdit;
            //ce.Properties.ValueChecked = ce.Checked;
            //ce.Properties.ValueUnchecked = !ce.Checked;
        }

        public const int WATCHER_UnKnowPlaza = 9;	//不明入口站
        public const int WATCHER_PlateInBlack = 20; //黑名单车辆
        private MSG_TCOConfirm AUS;

        public MSG_TCOConfirm GetAUS(bool IsSubmit)
        {
            TCOCall tc = TCE;

            AUS.DifPlate = tc.DifPlate;
            AUS.DifPlaza = tc.DifPlaza;
            AUS.DifType = tc.DifType;
            if (int.TryParse(AUS.DifKind && !string.IsNullOrEmpty(txtModifyCarKind.Text) ? txtModifyCarKind.Text : tc.ExitCarClass, out int cc))
            {
                AUS.CarClass = cc;
            }
            AUS.CarPlate = tc.DifPlate ? txtModifyCarNumber.Text : tc.ExitPlate;
            AUS.CarType = tc.DifType ? (txtModifyCarType.EditValue as int?).GetValueOrDefault() : int.Parse(tc.ExitCarType);
            var plaza = cbxModifyEntryPlaza.GetSelectedDataRow() as ProvPlazaInfo;
            if (plaza != null && !string.IsNullOrEmpty(plaza.plazaHEX) && plaza.plazaHEX.Length >= 8)
            {
                string txt = plaza.plazaHEX.Substring(4, 4);
                AUS.EntryPlazaHEX = plaza.plazaHEX;
                AUS.EntryPlazaId = plaza.plazaId;
                AUS.EntryPlazaName = plaza.plazaName;
                AUS.EntryNetNo = txt.Substring(0, 2);
                AUS.EntryPlazaNo = txt.Substring(2, 2);
            }
            AUS.DifPlaza = _tce.EntryStationID != tc.EntryStationID;
            AUS.DifEntryDateTime = _tce.EntryDHM != tc.EntryDHM;
            AUS.DifPlate = _tce.EntryPlate != tc.EntryPlate;
            AUS.TransNo = tc.TransNo;
            AUS.TimeoutCar = tc.TimeoutCar;
            //AUS.TCOStaffID = _runtimeSetting.NowCollect != null ? _runtimeSetting.NowCollect.UserId : "";
            AUS.UCar = tc.UCar ? 1 : 0;
            AUS.IsConfirm = IsSubmit;
            AUS.EntryDateTime = tc.EntryDHM;
            AUS.EntryLaneID = tc.EntryLaneID;
            AUS.EntryDHM = AUS.EntryDateTime.ToString("yyyyMMddHHmmss");
            AUS.DateTime = DateTime.Now;
            return AUS;
        }

        private DateTime dtold = DateTime.Now;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dtold.Second != DateTime.Now.Second)
            {
                dtold = DateTime.Now;
                pcPronow.Position = pcPronow.Position - 1;
                //if ( _tcocall != null && _tcocall.Visible)
                //{
                //    //  BLLWatcher.TellLaneTCOIsRE(TCO.Head.Network + TCO.Head.Plaza, TCO.Head.LaneNo);
                //}
                if (pcPronow.Position == pcPronow.Properties.Minimum)
                {
                    timer1.Stop();
                    //   btnOK.Enabled = false;
                    // CanDo = false;
                }
            }
        }
        
        private void txtModifyCarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AUS != null && (txtModifyCarType.EditValue as int?) > 0) AUS.DifType = true;
        }

        public Vnc.Viewer.View vnc;
        private TCOCall _tce;

        private void btnVNC_Click(object sender, EventArgs e)
        {
            try
            {
                frmRemoteViewer viewer = new frmRemoteViewer(Plaza, Lane);
                viewer.Show();
            }
            catch (Exception)
            {
            }
        }

        private void cbxProv_EditValueChanged(object sender, EventArgs e)
        {
            if (TCE != null && IsShowed)
            {
                FillPlazaNameAndList(TCE, false);
            }
        }

        private void tCOCallBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
        }

        private void tCOCallBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void tCOCallBindingSource_PositionChanged(object sender, EventArgs e)
        {
        }
    }
}