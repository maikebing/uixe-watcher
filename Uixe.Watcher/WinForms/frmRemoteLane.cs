using DevExpress.CodeParser;
using DevExpress.DataAccess.Native.Web;
using DevExpress.XtraEditors;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Uitls;
using Method = RestSharp.Method;

namespace Uixe.Watcher
{
    public partial class frmRemoteLane : DevExpress.XtraEditors.XtraForm
    {
        private readonly Plaza _plaza;
        private readonly LaneInfo _lane;
        public frmRemoteLane(Plaza plaza, LaneInfo lane)
        {
            _plaza = plaza;
            _lane = lane;
            InitializeComponent();
        }

        private  void frmRemoteLane_Load(object sender, EventArgs e)
        {
            keyboard1.IPAddress = _lane.IPAddress;
            var vnc =  VNCUtils.Login(this.vncScreen, _lane.IPAddress, 5900, _plaza.vncpwd ?? "kissme");
            if (vnc != null)
            {
                vnc.Text = $"车道远程控制 {_plaza.station_name}({_lane.PlazaId}){_lane.LaneName}   {_lane.IPAddress} ";
                this.Text = vnc.Text;
            }
            _ = Task.Run(async () =>
            {
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
                    if (_settings.CanPlayVideo)
                    {
                        LibVLC libVLC = new LibVLC();
                        string rtspurl = null;
                        if (!string.IsNullOrEmpty(_lane.VideoRtsp))
                        {
                            rtspurl = _lane.VideoRtsp;
                        }
                        else
                        {
                            var request = new RestRequest("/api/laneinfo", Method.Get);
                            request.AddHeader("Content-Type", "application/json");
                            if (client != null)
                            {
                                var result = await client.GetAsync<ApiResult<LaneInfoDtos>>(request);
                                rtspurl = result?.data?.laneVideoRTSPUrl;
                            }
                        }
                        this.Invoke(() =>
                        {
                            videoView1.MediaPlayer = new MediaPlayer(libVLC);
                            videoView1.MediaPlayer.EndReached += vlc_EndReached;
                            var media = new Media(libVLC, rtspurl, FromType.FromLocation); //播放本地文件
                            videoView1.MediaPlayer.Media = media;
                            videoView1.MediaPlayer.Play();
                            videoView1.MediaPlayer.Mute = _settings.laneVideoMute;
                        });
                    }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"视频初始化播放{_lane.VideoRtsp}失败，{ex.Message}");
            }
            });
        }

        private void vlc_EndReached(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc);
        }
        private void ThreadProc(Object stateInfo)
        {
            videoView1.MediaPlayer.Stop();
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
                            libInfo.Text = $"车道认证失败{response.code}-{response.msg}";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        libInfo.Text = $"车道认证失败{ex.Message}";
                    }));
                }

            });
        }

        bool ismax = false;
        internal RuntimeSetting _runtimeSetting;
        internal AppSettings _settings;

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

      

        private void keyboard1_ShowInfo(string text)
        {
            this.Invoke(new Action(() =>
            {
                libInfo.Text = text;
            }));
        }
    }
}