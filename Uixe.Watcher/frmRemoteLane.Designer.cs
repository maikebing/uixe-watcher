
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
            this.components = new System.ComponentModel.Container();
            this.videoView1 = new LibVLCSharp.WinForms.VideoView();
            this.radialMenu1 = new DevExpress.XtraBars.Ribbon.RadialMenu(this.components);
            this.vncScreen = new System.Windows.Forms.Panel();
            this.keyboard1 = new Uixe.Watcher.Controls.Keyboard();
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // videoView1
            // 
            this.videoView1.BackColor = System.Drawing.Color.Black;
            this.videoView1.Location = new System.Drawing.Point(6, 0);
            this.videoView1.Margin = new System.Windows.Forms.Padding(4);
            this.videoView1.MediaPlayer = null;
            this.videoView1.Name = "videoView1";
            this.videoView1.Size = new System.Drawing.Size(451, 421);
            this.videoView1.TabIndex = 1;
            this.videoView1.Text = "videoView1";
            // 
            // radialMenu1
            // 
            this.radialMenu1.Name = "radialMenu1";
            // 
            // vncScreen
            // 
            this.vncScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vncScreen.BackColor = System.Drawing.Color.Blue;
            this.vncScreen.Location = new System.Drawing.Point(453, 0);
            this.vncScreen.Margin = new System.Windows.Forms.Padding(4);
            this.vncScreen.Name = "vncScreen";
            this.vncScreen.Size = new System.Drawing.Size(1280, 941);
            this.vncScreen.TabIndex = 2;
            // 
            // keyboard1
            // 
            this.keyboard1.BackColor = System.Drawing.Color.Transparent;
            this.keyboard1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.keyboard1.IPAddress = null;
            this.keyboard1.Location = new System.Drawing.Point(6, 420);
            this.keyboard1.Name = "keyboard1";
            this.keyboard1.Port = 0;
            this.keyboard1.Size = new System.Drawing.Size(863, 379);
            this.keyboard1.TabIndex = 0;
            // 
            // frmRemoteLane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1733, 800);
            this.Controls.Add(this.keyboard1);
            this.Controls.Add(this.videoView1);
            this.Controls.Add(this.vncScreen);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmRemoteLane";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRemoteLane";
            this.Load += new System.EventHandler(this.frmRemoteLane_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private LibVLCSharp.WinForms.VideoView videoView1;
        private DevExpress.XtraBars.Ribbon.RadialMenu radialMenu1;
        private System.Windows.Forms.Panel vncScreen;
        private Controls.Keyboard keyboard1;
    }
}