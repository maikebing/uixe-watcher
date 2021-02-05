using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

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

        private void KeyButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                int keystr;
                if (int.TryParse(btn.Tag as string, out keystr))
                {
                    SendKeyMessage(MSG_KEYDOWN, keystr, 0);
                    SendKeyMessage(MSG_KEYUP, keystr, 0);
                }
            }
        }

        private void KeyButton_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public string  IPAddress { get; set; }
        public int Port { get; set; }
        private UdpClient uc = new UdpClient();

        private void SendKeyMessage(int message, int wparam, long lparam)
        {
            //struct MSG_CMD
            //{
            //    char AppType[2];
            //    char NetNo[2];
            //    char PlazaNo[2];
            //    char LaneType;
            //    char LaneNo[3];
            //    char CMD[10];
            //    char Param[100];
            //};
            try
            {
                string msg = $"TS{string.Empty.PadRight(2 + 2 + 1 + 3, ' ')}LANEREMOTE{message:D10}{wparam:D10}{lparam:D10}";
                ShowInfo?.Invoke(msg);
                var buffer = Encoding.Default.GetBytes(msg);
                uc.Send(buffer, buffer.Length, IPAddress, Port);
                System.Threading.Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                ShowInfo?.Invoke(ex.Message);
            }
        }
         
         

        private DateTime lastplay = DateTime.Now;
        private decimal rept = 0;
         

      
        public delegate void DShowInfo (string text);
    
        public event DShowInfo ShowInfo;
   
 
    }
}