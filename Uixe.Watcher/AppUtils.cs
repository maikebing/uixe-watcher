using DevExpress.XtraEditors;
using Microsoft.Win32;
using System;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Uixe.Watcher
{
    public class AppUtils
    {
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);


        public static void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    XtraMessageBox.Show("应用程序新版本此时无法下载 \n\n请检查你的网络连接，或者稍候重试，错误: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    XtraMessageBox.Show("由于部署错误，应用程序新版本无法检查更新，请重新部署应用程序并重试,错误: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    XtraMessageBox.Show("应用程序无法更新，不是一个有效的应用程序部署: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = XtraMessageBox.Show("应用程序已经有了新版本，你想现在确定更新吗?", "发现新版本", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        XtraMessageBox.Show("应用程序检测到一个强制版本更新" + info.MinimumRequiredVersion.ToString() + ". 应用程序现在将开始更新并重启.",
                            "发现新版本", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.UpdateCompleted += Ad_UpdateCompleted;
                            ad.CheckForUpdateProgressChanged += Ad_CheckForUpdateProgressChanged;
                            ad.UpdateAsync();

                        }
                        catch (DeploymentDownloadException dde)
                        {
                            XtraMessageBox.Show("无法安装新版本 \n\n请检查你的网络连接或稍候重试,错误: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        private static void Ad_CheckForUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            Program.MainForm.ShowStatusInfo($"正在更新:{e.Group},{e.BytesCompleted / 1024}/{e.BytesTotal / 1024},{e.ProgressPercentage}%,{e.State.ToString()} 。");
        }

        private static void Ad_UpdateCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                XtraMessageBox.Show("程序已更新， 即将重启.");
                Application.Restart();
            }
            else
            {
                XtraMessageBox.Show($"更新过程中遇到异常{e.Error.Message}");

            }
        }

    }






}