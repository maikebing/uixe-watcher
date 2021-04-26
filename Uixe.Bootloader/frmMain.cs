using System;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using Uixe.Bootloader.Properties;

namespace Uixe.Bootloader
{
    public partial class frmMain : Form
    {
        private svnloder svn;
        private Thread ts;

        public frmMain()
        {
            InitializeComponent();
        }
        private void CreateLnk(App app, string arg)
        {
            string lnkPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + app.ShortcutName + ".lnk";
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortCut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(lnkPath);
            shortCut.TargetPath = Application.ExecutablePath;
            shortCut.Arguments = arg;
            shortCut.WindowStyle = 1;
            shortCut.Description = Application.ProductName + Application.ProductVersion;
            shortCut.WorkingDirectory = Application.StartupPath;
            shortCut.Save();
        }
        private NameValueCollection GetQueryStringParameters()
        {
            NameValueCollection col = new NameValueCollection();
            try
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                    col = HttpUtility.ParseQueryString(queryString);

                }
                else if (System.Environment.CommandLine.IndexOf("?") > 0)
                {
                    col = HttpUtility.ParseQueryString(System.Environment.CommandLine.Substring(System.Environment.CommandLine.IndexOf("?")));
                }
            }
            catch (Exception)
            {

            }
            return col;
        }
        NameValueCollection nvc = new NameValueCollection();
        public App App { get; set; }
        public string PlazaNo { get; set; }
        public string AppName { get; set; }
        public string RootSvr { get; set; }
        public string SVNurl { get; set; }
        string defaultquery = "";

        private void frmMain_Load(object sender, EventArgs e)
        {
            nvc = GetQueryStringParameters();
            if (nvc != null && nvc.Count > 0)
            {
                PlazaNo = nvc["PlazaNo"];
                AppName = nvc["AppName"];
                RootSvr = nvc["RootSvr"];
            }
            else
            {
                //http://des.zsjgsc.com/ClickOnce/Uixe.Bootloader.application?RootSvr=des.zsjgsc.com&PlazaNo=9999&AppName=DES
                PlazaNo = Properties.Settings.Default.DefaultPlaza;
                AppName = Properties.Settings.Default.DefaultAppName;// nvc["AppName"];
                RootSvr = Settings.Default.DefaultRootSvr;
                defaultquery = string.Format($"?RootSvr={RootSvr}&PlazaNo={PlazaNo}&AppName={AppName}");

            }
            try
            {
                var fs = System.Drawing.FontFamily.Families;
                if (!fs.Any(f => f.Name == "微软雅黑"))
                {
                    InstallFont("msyh.ttf", "微软雅黑");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("安装字体时出现异常" + ex.Message);
            }
            try
            {
                svnloder.locroot = nvc["locroot"];
                SVNurl = string.Format("{0}", RootSvr);
                InstallItem it = InstallItem.LoadRes();
                var s = from i in it.AppList where i.AppName == AppName select i;
                if (string.IsNullOrEmpty(PlazaNo) || !s.Any())
                {
                    ForEnd();
                }
                else
                {
                    App = s.FirstOrDefault();
                    if (App == null)
                    {
                        MessageBox.Show("应用无效,请联系服务器管理员！");
                        Application.Exit();
                    }
                    else
                    {
                        libTitle.Text = App.ShortcutName;
                        label1.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                        try
                        {

                            ts = new Thread(new ThreadStart(Do));
                            ts.Start();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("进程 启动失败" + ex.Message + ex.StackTrace + ex.Source);
                            Application.Exit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("进程 启动失败" + ex.Message + ex.StackTrace + ex.Source);
            }
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd,
        uint Msg,
        int wParam,
        int lParam
        );
        [DllImport("gdi32")]
        public static extern int AddFontResource(string lpFileName);
        const int WM_FONTCHANGE = 0x001D;
        const int HWND_BROADCAST = 0xffff;
        public static bool InstallFont(string sFontFileName, string sFontName)
        {
            string _sTargetFontPath = string.Format(@"{0}\fonts\{1}", Environment.GetEnvironmentVariable("WINDIR"), sFontFileName);//系统FONT目录
            string _sResourceFontPath = string.Format(@"{0}\{1}", Application.StartupPath, sFontFileName);//需要安装的FONT目录
            try
            {
                if (!File.Exists(_sTargetFontPath) && File.Exists(_sResourceFontPath))
                {
                    int _nRet;
                    File.Copy(_sResourceFontPath, _sTargetFontPath);
                    _nRet = AddFontResource(_sTargetFontPath);
                    _nRet = WriteProfileString("fonts", sFontName + "(TrueType)", sFontFileName);
                    SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static void ForEnd()
        {
            if (MessageBox.Show(
                "请前往 des.zsjgsc.com 选择收费站和软件类型开始安装。\r\n【是】打开网站\r\n【否】自行打开页面！"
                , "安装完毕"
                , MessageBoxButtons.YesNo
                 ) == System.Windows.Forms.DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("http://des.zsjgsc.com");
            }
            Application.Exit();
        }
        private void Do()
        {


            if (!string.IsNullOrEmpty(PlazaNo) && App != null)
            {
                try
                {

                    svn = new svnloder(lblInfo, SVNurl + App.SvnPath);
                    try
                    {
                        string exe = svn.GetAppLoc(App.SvnPath);
                        var pros = from p in System.Diagnostics.Process.GetProcesses() where p.MainModule.FileName.ToLower().StartsWith(exe) select p;
                        if (pros.Any())
                        {
                            foreach (var item in pros.ToArray())
                            {
                                item.Kill();
                                svn.ShowInfo("正在结束进程" + item.ProcessName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        svn.ShowInfo("结束进程失败" + ex.Message);
                    }

                    if (svn.Init(SVNurl + App.SvnPath, App.SvnPath))
                    {
                        svn.ShowInfo("正在检查新版本....");
                        //if (svn.CheckVer(SVNurl + App.SvnPath, App.SvnPath))
                        {
                            svn.ShowInfo("准备更新....");
                            svn.Update(App.SvnPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    svn.ShowInfo("更新失败" + ex.Message);
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    svn.ShowInfo("正在启动应用程序....");

                    var psi = new ProcessStartInfo(svn.GetAppLoc(App.SvnPath + "\\" + App.ExecName)) { UseShellExecute = true };
                    psi.WorkingDirectory = svn.GetAppLoc(App.SvnPath + "\\");
                    Process.Start(psi);

                    Thread.Sleep(new TimeSpan(0, 0, 1));
                }
                catch (Exception ex1)
                {
                    svn.ShowInfo("应用程序启动失败" + ex1.Message);
                    MessageBox.Show(ex1.Message + Environment.NewLine + ex1.StackTrace+Environment.NewLine+ svn.GetAppLoc(App.SvnPath + "\\" + App.ExecName)+Environment.NewLine + svn.GetAppLoc(App.SvnPath + "\\"));
                }
            }
            else
            {
                svn.ShowInfo("没有参数，无法启动....");
                MessageBox.Show("缺少站编码或APP，因此无法做任何事。随后自动打开网站发行网站");
                System.Diagnostics.Process.Start(string.Format("http://{0}/", RootSvr));
            }
            Application.Exit();
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Control &&
                e.KeyCode == Keys.Down)
            {
                TopMost = false;
                btnCancel.Visible = true;
                btnUpdate.Visible = true;
                txtver.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            svnloder.Cancel = true;
            if (ts != null)
            {
                ts.Abort();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int s;
            if (int.TryParse(txtver.Text, out s))
            {
                if (MessageBox.Show(string.Format("是否更新至{0}", s.ToString()), "更新至版本", MessageBoxButtons.OKCancel) ==
                    DialogResult.OK)
                {
                    svn.Update(s);
                }
            }
        }
        private bool _isMouseDown;
        private Point _formLocation; //form的location
        private Point _mouseOffset;

        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            //鼠标的按下位置
            if (_isMouseDown)
            {
                Point pt = MousePosition;
                int x = _mouseOffset.X - pt.X;
                int y = _mouseOffset.Y - pt.Y;

                Location = new Point(_formLocation.X - x, _formLocation.Y - y);
            }
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                _formLocation = Location;
                _mouseOffset = MousePosition;
            }
        }
    }
}