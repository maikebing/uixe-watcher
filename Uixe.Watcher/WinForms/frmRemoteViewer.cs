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
    public partial class frmRemoteViewer : DevExpress.XtraEditors.XtraForm
    {
        bool ismax = false;
        private string baseinfo = "";
        private string ipaddresss = "";
        private string rtspurl = "";
        public frmRemoteViewer(Plaza plaza, LaneInfo lane)
        {
            baseinfo=   $"车道远程桌面 {plaza.station_name}({ lane.PlazaId}){lane.LaneName}   {lane.IPAddress} ";
            ipaddresss = lane.IPAddress;
            rtspurl = lane.VideoRtsp;
            InitializeComponent();
        }
 
        private async void frmRemoteLane_Load(object sender, EventArgs e)
        {
            var vnc = await VNCUtils.Login(this.vncScreen, ipaddresss, 5900, "kissme");
            if (vnc != null)
            {
                vnc.Text = $"{baseinfo} ";
                this.Text = vnc.Text;
                try
                {
                
                    if (!string.IsNullOrEmpty(rtspurl))
                    {
                        videoView1.StartPlay(rtspurl);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"视频初始化播放{rtspurl}失败，{ex.Message}");
                }
            }
        }
    

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