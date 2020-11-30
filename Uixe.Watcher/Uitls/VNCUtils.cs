using DevExpress.XtraEditors;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vnc.Viewer;

namespace TCS.BOSS.VNC
{
    public static class VNCUtils
    {
        public static async Task<Vnc.Viewer.View> Login(Form parentForm, string IP, int port, string password)
        {
            return await Login(parentForm, IP, port, password, false);
        }

        public static async Task<Vnc.Viewer.View> Login(Form parentForm, string IP, int port, string password, bool ispc)
        {
            Conn conn = new Conn();
            ViewOpts vopts = new ViewOpts();
            //vopts.IsFullScrn = true;
            if (ispc)
            {
                vopts.CliScaling = CliScaling.Auto;
                vopts.PixelSize = PixelSize.Force8Bit;
                vopts.CliScalingWidth = (ushort)(Screen.PrimaryScreen.WorkingArea.Width * 0.5);
            }
            else
            {
                vopts.ViewOnly = true;
                vopts.CliScaling = CliScaling.Custom;
                vopts.PixelSize = PixelSize.Force8Bit;
              
                    vopts.CliScalingWidth = 1280;
                    vopts.CliScalingHeight = 800;
            }

            //  Tamir.SharpSsh.SshExec ssh =  new Tamir.SharpSsh.SshExec (IP ,"root",password);
            //  ssh.Connect();
            //、、  ssh.RunCommand("killall vncserver ;cd /EMRCV4/BIN/;./vncserver &");
            // ssh.Close();
            if (parentForm != null)
            {
                conn.parentForm = parentForm;
            }

            Vnc.Viewer.ConnOpts copts = new Vnc.Viewer.ConnOpts(IP, port, password, vopts);

            await Task.Factory.StartNew((Action)delegate
                {
                    try
                    {
                        conn.Run(copts);
                        if (parentForm != null)
                        {
                            parentForm.Invoke((Action)delegate
                            {
                                if (conn != null && conn.myView != null)
                                {
                                    conn.myView.Refresh();
                                }
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        if (parentForm != null)
                        {
                            parentForm.Invoke((Action)delegate
                            {
                                XtraMessageBox.Show(ex.Message);
                            });
                        }
                    }
                });
            if (conn != null && conn.myView != null)
            {
                conn.myView.Menu = null;
                conn.myView.Text = "远程VNC:" + IP;
            }
            //   conn.myView.Size = new Size(640, 480);
            return conn.myView;
            // conn.myView.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
    }
}