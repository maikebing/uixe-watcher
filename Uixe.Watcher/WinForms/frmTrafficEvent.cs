using Cyotek.Windows.Forms;
using DevExpress.XtraEditors;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.WinForms
{
    public partial class frmTrafficEvent : XtraForm
    {
        private readonly List<PictureBox> _imageLoaders = new List<PictureBox>();
        private readonly List<VideoMediaHost> _videoHosts = new List<VideoMediaHost>();
        private LibVLC _libVlc;

        public frmTrafficEvent()
        {
            InitializeComponent();
            tabMedia.SelectedIndexChanged += tabMedia_SelectedIndexChanged;
        }

        /// <summary>
        /// 显示交通事件内容。
        /// </summary>
        public void ShowTrafficEvent(T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var stationName = plaza?.StationName ?? string.Empty;
            var laneNo = string.IsNullOrWhiteSpace(lane?.LaneNo) ? request.LaneNo : lane.LaneNo;
            var eventTypeText = request.GetEventTypeText();
            var summary = request.GetSummary(stationName, laneNo);

            Text = $"交通事件提醒 - {eventTypeText}";
            lblTitle.Text = eventTypeText;
            lblStationLane.Text = string.IsNullOrWhiteSpace(stationName) ? $"车道：{laneNo}" : $"收费站：{stationName}    车道：{laneNo}";
            txtRecordId.Text = string.IsNullOrWhiteSpace(request.RecordId) ? "无" : request.RecordId;
            txtEventType.Text = string.IsNullOrWhiteSpace(request.EventType) ? eventTypeText : $"{eventTypeText} ({request.EventType})";
            txtLaneNo.Text = string.IsNullOrWhiteSpace(laneNo) ? "无" : laneNo;
            txtCapTime.Text = request.CapTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "无";
            txtStartTime.Text = request.StartTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "无";
            txtDuration.Text = string.IsNullOrWhiteSpace(request.GetDurationText()) ? "无" : request.GetDurationText();
            txtQueueLength.Text = string.IsNullOrWhiteSpace(request.GetQueueLengthText()) ? "无" : request.GetQueueLengthText();
            memoSummary.Text = summary;
            BuildMediaTabs(request);
            Show();
            Activate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ReleaseMediaResources();
            base.OnFormClosed(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BuildMediaTabs(TrafficEventPushRequest request)
        {
            ResetMediaTabs();

            var imageItems = ParseMediaItems(request.ImageList, TrafficMediaKind.Image);
            var videoItems = ParseMediaItems(request.VideoList, TrafficMediaKind.Video);
            lblMediaHint.Text = $"图片 {imageItems.Count} 个，视频 {videoItems.Count} 个。切换选项卡可查看预览。";

            var imageIndex = 1;
            foreach (var item in imageItems)
            {
                tabMedia.TabPages.Add(CreateImageTabPage(item, imageIndex));
                imageIndex++;
            }

            var videoIndex = 1;
            foreach (var item in videoItems)
            {
                tabMedia.TabPages.Add(CreateVideoTabPage(item, videoIndex));
                videoIndex++;
            }

            if (tabMedia.TabPages.Count == 0)
            {
                var emptyPage = new TabPage("暂无媒体");
                emptyPage.Controls.Add(CreateMessageLabel("当前事件未提供图片或视频地址。"));
                tabMedia.TabPages.Add(emptyPage);
                lblMediaHint.Text = "当前事件未提供媒体地址。";
                return;
            }

            tabMedia.SelectedIndex = 0;
            PlaySelectedVideo();
        }

        private void ResetMediaTabs()
        {
            StopAllVideos();

            foreach (var videoHost in _videoHosts)
            {
                videoHost.Dispose();
            }

            _videoHosts.Clear();

            foreach (var loader in _imageLoaders)
            {
                loader.Dispose();
            }

            _imageLoaders.Clear();

            foreach (TabPage tabPage in tabMedia.TabPages)
            {
                tabPage.Dispose();
            }

            tabMedia.TabPages.Clear();
        }

        private void ReleaseMediaResources()
        {
            ResetMediaTabs();
            if (_libVlc != null)
            {
                _libVlc.Dispose();
                _libVlc = null;
            }
        }

        private TabPage CreateImageTabPage(TrafficMediaItem item, int index)
        {
            var tabPage = new TabPage($"图片{index}")
            {
                ToolTipText = item.Url
            };

            var imageBox = new ImageBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black,
                SizeMode = ImageBoxSizeMode.Fit
            };

            var loader = new PictureBox
            {
                Visible = false
            };

            loader.LoadCompleted += delegate (object sender, AsyncCompletedEventArgs e)
            {
                if (e.Error != null || loader.Image == null)
                {
                    ShowPreviewMessage(tabPage, $"图片加载失败：{item.Url}");
                    return;
                }

                imageBox.Image = (Image)loader.Image.Clone();
            };

            tabPage.Controls.Add(imageBox);
            tabPage.Controls.Add(loader);
            _imageLoaders.Add(loader);

            try
            {
                loader.ImageLocation = item.Url;
                loader.LoadAsync();
            }
            catch (Exception)
            {
                ShowPreviewMessage(tabPage, $"图片加载失败：{item.Url}");
            }

            return tabPage;
        }

        private TabPage CreateVideoTabPage(TrafficMediaItem item, int index)
        {
            var tabPage = new TabPage($"视频{index}")
            {
                ToolTipText = item.Url
            };

            try
            {
                EnsureLibVlc();

                var videoView = new VideoView
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Black
                };

                tabPage.Controls.Add(videoView);

                var videoHost = new VideoMediaHost(videoView, _libVlc, item.Url);
                tabPage.Tag = videoHost;
                _videoHosts.Add(videoHost);
            }
            catch (Exception)
            {
                ShowPreviewMessage(tabPage, $"视频初始化失败：{item.Url}");
            }

            return tabPage;
        }

        private void EnsureLibVlc()
        {
            if (_libVlc != null)
            {
                return;
            }

            Core.Initialize();
            _libVlc = new LibVLC();
        }

        private void tabMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlaySelectedVideo();
        }

        private void PlaySelectedVideo()
        {
            StopAllVideos();
            if (tabMedia.SelectedTab == null)
            {
                return;
            }

            if (tabMedia.SelectedTab.Tag is VideoMediaHost videoHost)
            {
                try
                {
                    videoHost.Play();
                    lblMediaHint.Text = $"正在播放：{videoHost.Url}";
                }
                catch (Exception)
                {
                    ShowPreviewMessage(tabMedia.SelectedTab, $"视频播放失败：{videoHost.Url}");
                }

                return;
            }

            if (!string.IsNullOrWhiteSpace(tabMedia.SelectedTab.ToolTipText))
            {
                lblMediaHint.Text = $"当前预览：{tabMedia.SelectedTab.ToolTipText}";
            }
        }

        private void StopAllVideos()
        {
            foreach (var videoHost in _videoHosts)
            {
                videoHost.Stop();
            }
        }

        private static List<TrafficMediaItem> ParseMediaItems(string rawValue, TrafficMediaKind kind)
        {
            return ParseMediaUrls(rawValue)
                .Select(url => new TrafficMediaItem(kind, url))
                .ToList();
        }

        private static IEnumerable<string> ParseMediaUrls(string rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return Enumerable.Empty<string>();
            }

            var trimmed = rawValue.Trim();
            if ((trimmed.StartsWith("[") && trimmed.EndsWith("]")) || (trimmed.StartsWith("{") && trimmed.EndsWith("}")))
            {
                try
                {
                    using var document = JsonDocument.Parse(trimmed);
                    var values = new List<string>();
                    CollectUrls(document.RootElement, values);
                    return values.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
                }
                catch (JsonException)
                {
                }
            }

            return trimmed
                .Split(new[] { '\r', '\n', ',', ';', '|', '，', '；' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Trim().Trim('"'))
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static void CollectUrls(JsonElement element, List<string> values)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.String:
                    var value = element.GetString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        values.Add(value.Trim());
                    }
                    break;
                case JsonValueKind.Array:
                    foreach (var item in element.EnumerateArray())
                    {
                        CollectUrls(item, values);
                    }
                    break;
                case JsonValueKind.Object:
                    foreach (var property in element.EnumerateObject())
                    {
                        if (property.Value.ValueKind == JsonValueKind.String)
                        {
                            var propertyName = property.Name.ToLowerInvariant();
                            if (propertyName.Contains("url") || propertyName.Contains("uri") || propertyName.Contains("path") || propertyName.Contains("src"))
                            {
                                var propertyValue = property.Value.GetString();
                                if (!string.IsNullOrWhiteSpace(propertyValue))
                                {
                                    values.Add(propertyValue.Trim());
                                }
                            }
                        }

                        CollectUrls(property.Value, values);
                    }
                    break;
            }
        }

        private static Label CreateMessageLabel(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private static void ShowPreviewMessage(TabPage tabPage, string text)
        {
            if (tabPage.Controls.OfType<Label>().Any())
            {
                return;
            }

            var label = CreateMessageLabel(text);
            tabPage.Controls.Add(label);
            label.BringToFront();
        }

        private enum TrafficMediaKind
        {
            Image,
            Video
        }

        private sealed record TrafficMediaItem(TrafficMediaKind Kind, string Url);

        private sealed class VideoMediaHost : IDisposable
        {
            private readonly LibVLC _libVlc;
            private Media _media;

            public VideoMediaHost(VideoView videoView, LibVLC libVlc, string url)
            {
                VideoView = videoView;
                _libVlc = libVlc;
                Url = url;
                MediaPlayer = new MediaPlayer(libVlc);
                VideoView.MediaPlayer = MediaPlayer;
            }

            public VideoView VideoView { get; }

            public MediaPlayer MediaPlayer { get; }

            public string Url { get; }

            public void Play()
            {
                Stop();
                _media?.Dispose();
                _media = null;

                if (!Uri.TryCreate(Url, UriKind.Absolute, out var uri))
                {
                    throw new InvalidOperationException($"无效的视频地址：{Url}");
                }

                _media = new Media(_libVlc, uri);
                MediaPlayer.Play(_media);
            }

            public void Stop()
            {
                if (MediaPlayer.IsPlaying)
                {
                    MediaPlayer.Stop();
                }
            }

            public void Dispose()
            {
                Stop();
                _media?.Dispose();
                VideoView.MediaPlayer = null;
                MediaPlayer.Dispose();
                VideoView.Dispose();
            }
        }
    }
}
