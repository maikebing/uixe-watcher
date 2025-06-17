using DevExpress.XtraEditors;
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
using Uixe.Watcher.Uitls;
using WhoIamDtos;

namespace Uixe.Watcher.WinForms
{
    public partial class frmBulktrans : DevExpress.XtraEditors.XtraForm
    {
        public frmBulktrans()
        {
            InitializeComponent();
        }

        T_Plaza _plaza;
        T_Lane _lane;
        internal void ShowBulktrans(T_Plaza plaza, T_Lane lane, BulklyDto dto)
        {
            _plaza = plaza;
            _lane = lane;
            Text = $"大件运输车确认 {plaza.StationName} {lane.LaneNo}";
            txtVehId.Text = dto.VehId;
            tsIsValid.IsOn = dto.IsValid;
            numWeight.Value = dto.Weight;
            numAlex.Value = dto.Alex;
            lARGEWOODSBindingSource.DataSource = dto.LARGEWOODS;
            this.Show();
            this.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (_lane != null)
            {
                StartCall();
                try
                {
                    var result = await _lane.SendMsg("toll/bulktrans", new LaneBulkTransDto() { vehWeight =(float) numWeight.Value, vehAxles = (int)numAlex.Value });
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
    }
}