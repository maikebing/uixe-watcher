using Uixe.Watcher.Properties;
namespace Uixe.Watcher.Controls
{
    partial class TCOPictureBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pic = new System.Windows.Forms.PictureBox();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.imageBox1 = new Cyotek.Windows.Forms.ImageBox();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.pcBPane = new DevExpress.XtraEditors.PanelControl();
            this.pcBar = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBPane)).BeginInit();
            this.pcBPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcBar)).BeginInit();
            this.pcBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.BackColor = System.Drawing.Color.White;
            this.pic.ImageLocation = "ftp://root:kissme@192.168.50.58/EMRCV4/IMAGE/TEMP/TTEMP.JPG";
            this.pic.Location = new System.Drawing.Point(89, 71);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(352, 288);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Visible = false;
            this.pic.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pic_LoadCompleted);
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Appearance.BackColor = System.Drawing.Color.White;
            this.panel1.Appearance.Options.UseBackColor = true;
            this.panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panel1.Controls.Add(this.imageBox1);
            this.panel1.Controls.Add(this.pic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 373);
            this.panel1.TabIndex = 2;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // imageBox1
            // 
            this.imageBox1.AllowDoubleClick = true;
            this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox1.Location = new System.Drawing.Point(2, 2);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(541, 369);
            this.imageBox1.TabIndex = 1;
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.Size = new System.Drawing.Size(400, 200);
            this.propertyGridControl1.TabIndex = 0;
            // 
            // pcBPane
            // 
            this.pcBPane.Controls.Add(this.pcBar);
            this.pcBPane.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcBPane.Location = new System.Drawing.Point(0, 367);
            this.pcBPane.Name = "pcBPane";
            this.pcBPane.Size = new System.Drawing.Size(545, 6);
            this.pcBPane.TabIndex = 3;
            this.pcBPane.Visible = false;
            // 
            // pcBar
            // 
            this.pcBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcBar.Controls.Add(this.simpleButton4);
            this.pcBar.Controls.Add(this.simpleButton3);
            this.pcBar.Controls.Add(this.simpleButton2);
            this.pcBar.Controls.Add(this.simpleButton1);
            this.pcBar.Location = new System.Drawing.Point(221, -10);
            this.pcBar.Name = "pcBar";
            this.pcBar.Size = new System.Drawing.Size(104, 28);
            this.pcBar.TabIndex = 0;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(80, 4);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(21, 21);
            this.simpleButton4.TabIndex = 3;
            this.simpleButton4.Text = "simpleButton4";
            this.simpleButton4.Click += new System.EventHandler(this.saveSToolStripButton_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(54, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(21, 21);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "simpleButton3";
            this.simpleButton3.Click += new System.EventHandler(this.BtnZoomOut30_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = global::Uixe.Watcher.Properties.Resources.BtnZoomIn30_Image;
            this.simpleButton2.Location = new System.Drawing.Point(3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(21, 21);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.BtnZoomIn30_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = global::Uixe.Watcher.Properties.Resources.BtnZoom30_Image;
            this.simpleButton1.Location = new System.Drawing.Point(28, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(21, 21);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.BtnZoom30_Click);
            // 
            // TCOPictureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcBPane);
            this.Controls.Add(this.panel1);
            this.Name = "TCOPictureBox";
            this.Size = new System.Drawing.Size(545, 373);
            this.Load += new System.EventHandler(this.TCOPictureBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBPane)).EndInit();
            this.pcBPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcBar)).EndInit();
            this.pcBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraEditors.PanelControl pcBPane;
        private DevExpress.XtraEditors.PanelControl pcBar;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private Cyotek.Windows.Forms.ImageBox imageBox1;
    }
}
