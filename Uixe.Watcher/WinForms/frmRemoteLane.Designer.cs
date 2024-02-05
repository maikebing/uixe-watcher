
namespace Uixe.Watcher
{
    partial class frmRemoteLane
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            radialMenu1 = new DevExpress.XtraBars.Ribbon.RadialMenu(components);
            vncScreen = new System.Windows.Forms.Panel();
            keyboard1 = new Controls.Keyboard();
            videoView1 = new WinFormsRtspPlayer.VideoControl();
            libInfo = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)radialMenu1).BeginInit();
            SuspendLayout();
            // 
            // radialMenu1
            // 
            radialMenu1.Name = "radialMenu1";
            // 
            // vncScreen
            // 
            vncScreen.BackColor = System.Drawing.Color.Blue;
            vncScreen.Location = new System.Drawing.Point(438, 2);
            vncScreen.Name = "vncScreen";
            vncScreen.Size = new System.Drawing.Size(1280, 720);
            vncScreen.TabIndex = 2;
            // 
            // keyboard1
            // 
            keyboard1.BackColor = System.Drawing.Color.Transparent;
            keyboard1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            keyboard1.client = null;
            keyboard1.IPAddress = null;
            keyboard1.LaneToken = null;
            keyboard1.Location = new System.Drawing.Point(3, 377);
            keyboard1.Margin = new System.Windows.Forms.Padding(2);
            keyboard1.Name = "keyboard1";
            keyboard1.Size = new System.Drawing.Size(853, 345);
            keyboard1.TabIndex = 0;
            keyboard1.ShowInfo += keyboard1_ShowInfo;
            // 
            // videoView1
            // 
            videoView1.BackColor = System.Drawing.Color.Black;
            videoView1.Location = new System.Drawing.Point(13, 2);
            videoView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            videoView1.Name = "videoView1";
            videoView1.Size = new System.Drawing.Size(418, 369);
            videoView1.TabIndex = 3;
            // 
            // libInfo
            // 
            libInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            libInfo.LineLocation = DevExpress.XtraEditors.LineLocation.Top;
            libInfo.Location = new System.Drawing.Point(134, 377);
            libInfo.Name = "libInfo";
            libInfo.Size = new System.Drawing.Size(298, 0);
            libInfo.TabIndex = 4;
            // 
            // frmRemoteLane
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(1723, 726);
            Controls.Add(libInfo);
            Controls.Add(videoView1);
            Controls.Add(keyboard1);
            Controls.Add(vncScreen);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "frmRemoteLane";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "frmRemoteLane";
            Load += frmRemoteLane_Load;
            ((System.ComponentModel.ISupportInitialize)radialMenu1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RadialMenu radialMenu1;
        private System.Windows.Forms.Panel vncScreen;
        private Controls.Keyboard keyboard1;
        private WinFormsRtspPlayer.VideoControl videoView1;
        private DevExpress.XtraEditors.LabelControl libInfo;
    }
}