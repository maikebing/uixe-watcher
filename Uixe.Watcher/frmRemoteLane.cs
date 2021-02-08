using DevExpress.XtraEditors;
using LibVLCSharp.Shared;
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
using TCS.BOSS.VNC;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher
{
    public partial class frmRemoteLane : DevExpress.XtraEditors.XtraForm
    {
        private readonly Plaza _plaza;
        private readonly LaneInfo _lane;
       
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        public Color GetColor(IntPtr hdc, int x, int y)
        {
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
        public frmRemoteLane(Plaza  plaza,  LaneInfo lane)
        {
            _plaza = plaza;
            _lane = lane;
            InitializeComponent();
        }
        private async void frmRemoteLane_Load(object sender, EventArgs e)
        {
            var vnc = await VNCUtils.Login(this.vncScreen, _lane.IPAddress, 5900, "kissme");
            //  this.Size =new Size (vnc.Width+(this.Width- panel1.Width)*2,vnc.Height+(this.Height-panel1.Height));
            if (vnc != null) vnc.Text = $" {_plaza.station_name}({ _lane.PlazaId}){_lane.LaneName}   {_lane.IPAddress} ";
            this.Text = vnc.Text;
            Core.Initialize();
            var LibVLC = new LibVLC();
            var media = new Media(LibVLC,
                new Uri(_lane.VideoRTSP));
            this.videoView1.MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
            this.videoView1.MediaPlayer.Play();
            keyboard1.IPAddress = _lane.IPAddress;
            this.keyboard1.Port = Properties.Settings.Default.KeyboardPort;
            keyboard1.BackColor = vnc[1, 1];
                this.BackColor = keyboard1.BackColor;

        }
    }
}