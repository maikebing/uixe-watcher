using RestSharp;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uixe.Watcher.Controls
{
    [DefaultEvent("ShowInfo")]
    public partial class Keyboard : UserControl
    {

        public Keyboard()
        {
            InitializeComponent();

        }

        private void Keyboard_Load(object sender, EventArgs e)
        {
        }

        public const int MSG_KEYDOWN = 0x0010;
        public const int MSG_KEYUP = 0x0012;
        public const int MSG_USER = 0x0800;

        private async void KeyButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                int keystr;
                if (int.TryParse(btn.Tag as string, out keystr))
                {
                    await SendKeyMessageAsync(MSG_KEYDOWN, keystr, 0);
                    await SendKeyMessageAsync(MSG_KEYUP, keystr, 0);
                }
            }
        }

        private void KeyButton_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public string IPAddress { get; set; }
   
        public string  LaneToken { get;  set; }
        public RestClient client { get; set; }
        private async Task SendKeyMessageAsync(int message, int wParam, long lParam)
        {
            try
            {
              
                var request = new RestRequest("/api/control", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization",$"Bearer {LaneToken}" );
                var body = new { message, wParam, lParam };
                request.AddJsonBody(body);
                var response = await client.PostAsync<(int code, string msg)>(request);
                ShowInfo?.Invoke($"{response.code}-{response.msg}");
            }
            catch (Exception ex)
            {
                ShowInfo?.Invoke(ex.Message);
            }
        }

        public delegate void DShowInfo(string text);

        public event DShowInfo ShowInfo;
    }
}