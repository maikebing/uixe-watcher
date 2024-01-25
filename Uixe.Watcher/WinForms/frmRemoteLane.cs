using DevExpress.CodeParser;
using DevExpress.DataAccess.Native.Web;
using DevExpress.XtraEditors;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
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
            keyboard1.IPAddress = _lane.IPAddress;
            var auth1 = $"{_lane.PlazaId}{_lane.LaneName}{_lane.terminalId}";
            var auth2 = BitConverter.ToString(MD5.HashData(Encoding.ASCII.GetBytes(auth1))).Replace("-", "").ToLower();
            var auth3 = BitConverter.ToString(MD5.HashData(Encoding.ASCII.GetBytes($"{auth2}{_runtimeSetting.serialNum}"))).Replace("-", "").ToLower();
            keyboard1.LaneToken = auth3;
            var client = new RestClient(new RestClientOptions($"http://{keyboard1.IPAddress}:10000/") { FollowRedirects = false });
            client.AddDefaultHeader(KnownHeaders.Accept, "*/*");
            keyboard1.client = client;
            await LaneAuth(auth3, client);
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
            var vnc = await VNCUtils.Login(this.vncScreen, _lane.IPAddress, 5900, _plaza.vncpwd ?? "kissme");
            if (vnc != null)
            {
                vnc.Text = $"车道远程控制 {_plaza.station_name}({_lane.PlazaId}){_lane.LaneName}   {_lane.IPAddress} ";
                this.Text = vnc.Text;
            }
        }

        private async Task LaneAuth(string auth3, RestClient client)
        {
            await Task.Run(async () =>
            {

                try
                {
                    var request = new RestRequest("/api/auth", RestSharp.Method.Post);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", $"Bearer {auth3}");
                    var body = new { clientType = "tco", _runtimeSetting.serialNum };
                    request.AddJsonBody(body);
                    var response = await client.PostAsync<(int code, string msg)>(request);
                    if (response.code != 0)
                    {
                        this.Invoke(new Action(() =>
                        {
                            XtraMessageBox.Show($"车道认证失败{response.code}-{response.msg}");
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        XtraMessageBox.Show($"车道认证失败{ex.Message}");
                    }));
                }

            });
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