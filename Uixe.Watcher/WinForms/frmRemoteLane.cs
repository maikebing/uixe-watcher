using DevExpress.XtraEditors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher
{
    public partial class frmRemoteLane : DevExpress.XtraEditors.XtraForm
    {
        private readonly Plaza _plaza;
        private readonly LaneInfo _lane;
        public frmRemoteLane(Plaza  plaza,  LaneInfo lane)
        {
            _plaza = plaza;
            _lane = lane;
            InitializeComponent();
        }
        private async void frmRemoteLane_Load(object sender, EventArgs e)
        {
            var vnc = await VNCUtils.Login(this.vncScreen, _lane.IPAddress, 5900, "kissme");
            if (vnc != null)
            {
                vnc.Text = $"车道远程控制 {_plaza.station_name}({ _lane.PlazaId}){_lane.LaneName}   {_lane.IPAddress} ";
                this.Text = vnc.Text;
                keyboard1.IPAddress = _lane.IPAddress;
                this.keyboard1.Port = _runtimeSetting.KeyboardPort;
                try
                {
                    if (!string.IsNullOrEmpty(_lane.CameraRtspUrl))
                    {
                        videoView1.StartPlay(_lane.CameraRtspUrl);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"视频初始化播放{_lane.CameraRtspUrl}失败，{ex.Message}");
                }
            }
        }
        bool ismax = false;
        internal RuntimeSetting _runtimeSetting;

        private void videoView1_DoubleClick(object sender, EventArgs e)
        {
            if (ismax)
            {
                videoView1.Size = new Size(431, 372);
                ismax = false;
            }
            else
            {
                ismax = true;
                videoView1.Size = this.Size;
            }
         
        }
    }
}