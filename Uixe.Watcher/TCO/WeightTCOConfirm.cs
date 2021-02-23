using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher.V1
{
    public partial class WeightTCOConfirm : DevExpress.XtraEditors.XtraUserControl
    {
        public WeightTCOConfirm()
        {
            InitializeComponent();
            ItemForText.Text = " 监 \n 控 \n 内 \n 容 ";
        }

 
         public MsgWeightTCOCALL TCE { get; set; }
        public bool CanDo { get; set; }
        public IMqttClient MqttClient { get; internal set; }


        private Prompt prompt = null;
        MsgWeightTCOCALL _tce;
        public MSG_TCOConfirm GetTCOConfirm(bool _IsConfirm)
        {
            var r = new MSG_TCOConfirm()
            {
                IsConfirm = _IsConfirm,
                TransNo = TCE.MsgTcoTran.TransNO,
                TCOStaffID = RuntimeSetting.NowCollect?.UserId,
                DateTime = DateTime.UtcNow,
                FreshAgri = dbxLSTD.GetSelectedDataRow() as FreshAgriProducts,
                CarPlate = TCE.CarPlate,
                AxleLastNo = int.Parse(TCE.CarAlex),
                CarClass = TCE.CarKind_INT,
                CarType = TCE.CarType_INT,
                ConfirmType = (int)TCE.CallType,
                DifKind = TCE.CarKind_INT != _tce.CarKind_INT,
                DifPlate = TCE.CarPlate != _tce.CarPlate,
                DifPlaza = TCE.Plaza != _tce.Plaza,
                DifType = TCE.CarType_INT != _tce.CarType_INT,
                EntryNetNo = TCE.Network,
                EntryPlazaNo = TCE.Plaza,
                WeightInput = int.Parse(TCE.CarFareWeight),
                WeightLimit = int.Parse(TCE.WeightLimit),
                IsDiscountCard = false,
                TCOType = TCE.CallType,
                WeightType = int.Parse(TCE.WeightType)

            };
            return r;
        }
        public void Show(MsgWeightTCOCALL tce)
        {
            try
            {

                _tce = tce.Clone();
                TCE = tce;
                bsHCCZ.ResetCurrentItem();
                bsHCCZ.DataSource = KeyItem.GetHueChe();
                bsTcotype.ResetCurrentItem();
                bsTcotype.DataSource = KeyItem.GetTCOCK();
                bsUD.ResetCurrentItem();
                bsUD.DataSource = KeyItem.GetUD();
                freshAgriProductsBindingSource.ResetCurrentItem();
                freshAgriProductsBindingSource.DataSource = FAP.GetFreshAgriProducts.FreshAgriProducts.ToArray();
                dbxLSTD.ResetText();
                dbxLSTD.Reset();
                dbxLSTD.Properties.View.SelectRow(dbxLSTD.Properties.View.GetRowHandle(0));
                freshAgriProductsBindingSource.Position = 0;
                CarKindComboBoxEdit.SelectedIndex = 0;
                msgWeightTCOCALLBindingSource.ResetCurrentItem();
                msgWeightTCOCALLBindingSource.DataSource = tce;
                Reset();
                CarPlateTextEdit.Enabled = tce.CallType == WATCHER_TYPE.WATCHER_State44_ModifyCarNumber;
                CarPlateTextEdit.ReadOnly = !CarPlateTextEdit.Enabled;
                lciHwleixing.Visibility =   DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                dbxLSTD.Enabled =tce.CallType == WATCHER_TYPE.WATCHER_LTORNONGYONG;
                //picLane.ImageLocation =;/、 string.Format("ftp://root:{0}@{1}/{2}/IMAGE/TEMP/TTEMP.JPG","future", TCS.Config.AppConfig.GetLaneIP(tce.Network + tce.Plaza, tce.LaneNo), AppConfig.RunTime.LaneAppDir);
                //picBig.ImageLocation = picLane.ImageLocation;
                var strct = Enum.GetName(typeof(WATCHER_TYPE), tce.CallType); 
               string speechtext = $"{tce.LaneNo} {KeyItem.GetTCOCK().ToList().FirstOrDefault(ki => ki.KeyID == strct)?.KeyName}";
                bool sound = false;
                switch (tce.CallType)
                {
                    case WATCHER_TYPE.WATCHER_LTORNONGYONG:
                        sound = Properties.Settings.Default.SpeechLvSeTongDao;
                        break;
                    case  WATCHER_TYPE.WATCHER_BlacklistPlate:
                        sound = Properties.Settings.Default.SpeedBlackListPlate;
                        break;

                    default:
                        sound = false;
                        break;
                }
                if (sound)
                {
                    prompt = SpeechUtils.Speecher.SpeakAsync(speechtext);//语音阅读方法
                }
            }
            catch (Exception ex)
            {
                //Log.LogException("Show(MsgWeightTCOCALL tce)", tce.ToString(), ex);
            }
        }

        public void Reset()
        {
            this.pcPronow.Properties.Maximum = TCE.TimeOut;
            pcPronow.Position = TCE.TimeOut;
            tcoHeart.Start();
            CanDo = true;
            btnOK.Enabled = true;
            btnCLose.Enabled = false;
            btnCancel.Enabled = true;
            picLane.Image = null;
            picLane.ImageLocation = "";
        }

        private DateTime dtold = DateTime.Now;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (dtold.Second != DateTime.Now.Second)
                {
                    dtold = DateTime.Now;
                    pcPronow.Position = pcPronow.Position - 1;
                    pcPronow.Text = pcPronow.Position.ToString();
                    if (TCE != null)
                    {
                        //BLLWatcher.TellLaneTCOIsRE(TCE.Network + TCE.Plaza, TCE.LaneNo);
                    }
                    if (pcPronow.Position == pcPronow.Properties.Minimum)
                    {
                        tcoHeart.Stop();
                        SpeechUtils.Speecher.SpeakAsyncCancel(prompt);
                        btnOK.Enabled = false;
                        CanDo = false;
                        btnCLose.Enabled = true;
                        btnCancel.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {
                //   Log.Write("btnVNC_Click", ex, "");
            }
        }

        private   void btnVNC_Click(object sender, EventArgs e)
        {
            try
            {
                //Vnc.Viewer.View vnc = await VNCUtils.Login(Uixe.Watcher.Program.MainForm,  (TCE.Network + TCE.Plaza, TCE.LaneNo), 5900, AppConfig.RunTime.VNCPassword);
                //vnc.Show();
                //vnc.Focus();
            }
            catch (Exception)
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if ( TCE.CallType ==  WATCHER_TYPE.WATCHER_LTORNONGYONG   && string.IsNullOrEmpty(dbxLSTD.EditValue as string))
                {
                    dbxLSTD.Focus();
                    dbxLSTD.PerformClick(dbxLSTD.Properties.Buttons[0]);
                }
                else
                {
                    TCOCallUtils.Submit(true, this);
                    tcoHeart.Stop();
                    TCOCallUtils.WeightTCOCall.RemoveNowTab(this.Name);
                    SpeechUtils.Speecher.SpeakAsyncCancel(prompt);
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CanDo)
                {
                    TCOCallUtils.Submit(false, this);
                }
                tcoHeart.Stop();
                SpeechUtils.Speecher.SpeakAsyncCancel(prompt);
                TCOCallUtils.WeightTCOCall.RemoveNowTab(this.Name);
            }
            catch (Exception)
            {
            }
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CanDo)
                {
                    TCOCallUtils.WeightTCOCall.RemoveNowTab(this.Name);
                }
                tcoHeart.Stop();
                SpeechUtils.Speecher.SpeakAsyncCancel(prompt);
            }
            catch (Exception)
            {
            }
        }

        private void picLane_DoubleClick(object sender, EventArgs e)
        {
            picBig.Visible = true;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            picBig.Visible = false;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            picBig.Visible = false;
        }

        private void YMDHMTextEdit_FormatEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != DBNull.Value && e.Value != null)
            {
                try
                {
                    if (e.Value.GetType() == typeof(string) && e.Value.ToString().Length >= 14)
                    {
                        YMDHMTextEdit.DateTime = DateTime.ParseExact(e.Value.ToString(), "yyyyMMddHHmmss", null);
                    }
                    else if (e.Value.GetType() == typeof(DateTime))
                    {
                        YMDHMTextEdit.DateTime = Convert.ToDateTime(e.Value);
                    }
                }
                catch (Exception)
                {
                }
                e.Handled = true;
            }
        }
    }
}