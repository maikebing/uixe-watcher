using System;
using System.Linq;
using System.Speech.Synthesis;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Extensions;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher.TCO
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
 
  
        private Prompt prompt = null;
        private MsgWeightTCOCALL _tce;

        public MSG_TCOConfirm GetTCOConfirm(bool _IsConfirm)
        {
            var r = new MSG_TCOConfirm()
            {
                IsConfirm = _IsConfirm,
                TransNo = TCE.MsgTcoTran.TransNO,
               // TCOStaffID = Owner..NowCollect?.UserId,
                DateTime = DateTime.UtcNow,
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

        public T_Lane Lane { get;  set; }
        public T_Plaza Plaza { get; set; }
        public  frmWeightTCOCall  Owner { get;  set; }

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
                CarKindComboBoxEdit.SelectedIndex = 0;
                msgWeightTCOCALLBindingSource.ResetCurrentItem();
                msgWeightTCOCALLBindingSource.DataSource = tce;
                Reset();
                CarPlateTextEdit.Enabled = tce.CallType == WATCHER_TYPE.WATCHER_State44_ModifyCarNumber;
                CarPlateTextEdit.ReadOnly = !CarPlateTextEdit.Enabled;
                Lane =  Plaza.Lanes.FirstOrDefault(f => f.LaneNo == tce.LaneNo);
                string url = string.Format($"http://{Lane.Ip}:10000/capture");
                picLane.ImageLocation = url;
                picBig.ImageLocation = url;
                var strct = Enum.GetName(typeof(WATCHER_TYPE), tce.CallType);
                string speechtext = $"{tce.LaneNo} {KeyItem.GetTCOCK().ToList().FirstOrDefault(ki => ki.KeyID == strct)?.KeyName}";
                bool sound = false;
                switch (tce.CallType)
                {
                    case WATCHER_TYPE.WATCHER_LTORNONGYONG:
                        sound = _settings.SpeechLvSeTongDao;
                        break;

                    case WATCHER_TYPE.WATCHER_BlacklistPlate:
                        sound = _settings.SpeedBlackListPlate;
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
        internal RuntimeSetting _runtimeSetting;
        internal AppSettings _settings;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (dtold.Second != DateTime.Now.Second)
                {
                    dtold = DateTime.Now;
                    pcPronow.Position = pcPronow.Position - 1;
                    pcPronow.Text = pcPronow.Position.ToString();
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

        private void btnVNC_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                TCOCallUtils.Submit(true, this);
                tcoHeart.Stop();
                this.Owner.RemoveNowTab(this.Name);
                SpeechUtils.Speecher.SpeakAsyncCancel(prompt);
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
                this.Owner.RemoveNowTab(this.Name);
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
                    this.Owner.RemoveNowTab(this.Name);
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