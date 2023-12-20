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
            pic = new System.Windows.Forms.PictureBox();
            panel1 = new DevExpress.XtraEditors.PanelControl();
            imageBox1 = new Cyotek.Windows.Forms.ImageBox();
            propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            pcBPane = new DevExpress.XtraEditors.PanelControl();
            pcBar = new DevExpress.XtraEditors.PanelControl();
            simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panel1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)propertyGridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pcBPane).BeginInit();
            pcBPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcBar).BeginInit();
            pcBar.SuspendLayout();
            SuspendLayout();
            // 
            // pic
            // 
            pic.BackColor = System.Drawing.Color.White;
            pic.ImageLocation = "ftp://root:kissme@192.168.50.58/EMRCV4/IMAGE/TEMP/TTEMP.JPG";
            pic.Location = new System.Drawing.Point(104, 89);
            pic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            pic.Name = "pic";
            pic.Size = new System.Drawing.Size(352, 288);
            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.Visible = false;
            pic.LoadCompleted += pic_LoadCompleted;
            pic.MouseDown += pic_MouseDown;
            pic.MouseMove += pic_MouseMove;
            pic.MouseUp += pic_MouseUp;
            // 
            // panel1
            // 
            panel1.Appearance.BackColor = System.Drawing.Color.White;
            panel1.Appearance.Options.UseBackColor = true;
            panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel1.Controls.Add(imageBox1);
            panel1.Controls.Add(pic);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(636, 466);
            panel1.TabIndex = 2;
            panel1.Resize += panel1_Resize;
            // 
            // imageBox1
            // 
            imageBox1.AllowDoubleClick = true;
            imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            imageBox1.Location = new System.Drawing.Point(2, 2);
            imageBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            imageBox1.Name = "imageBox1";
            imageBox1.Size = new System.Drawing.Size(632, 462);
            imageBox1.TabIndex = 1;
            // 
            // propertyGridControl1
            // 
            propertyGridControl1.Location = new System.Drawing.Point(0, 0);
            propertyGridControl1.Name = "propertyGridControl1";
            propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            propertyGridControl1.Size = new System.Drawing.Size(400, 200);
            propertyGridControl1.TabIndex = 0;
            // 
            // pcBPane
            // 
            pcBPane.Controls.Add(pcBar);
            pcBPane.Dock = System.Windows.Forms.DockStyle.Bottom;
            pcBPane.Location = new System.Drawing.Point(0, 458);
            pcBPane.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            pcBPane.Name = "pcBPane";
            pcBPane.Size = new System.Drawing.Size(636, 8);
            pcBPane.TabIndex = 3;
            pcBPane.Visible = false;
            // 
            // pcBar
            // 
            pcBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            pcBar.Controls.Add(simpleButton4);
            pcBar.Controls.Add(simpleButton3);
            pcBar.Controls.Add(simpleButton2);
            pcBar.Controls.Add(simpleButton1);
            pcBar.Location = new System.Drawing.Point(258, -13);
            pcBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            pcBar.Name = "pcBar";
            pcBar.Size = new System.Drawing.Size(121, 35);
            pcBar.TabIndex = 0;
            // 
            // simpleButton4
            // 
            simpleButton4.Location = new System.Drawing.Point(93, 5);
            simpleButton4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            simpleButton4.Name = "simpleButton4";
            simpleButton4.Size = new System.Drawing.Size(24, 26);
            simpleButton4.TabIndex = 3;
            simpleButton4.Text = "simpleButton4";
            simpleButton4.Click += saveSToolStripButton_Click;
            // 
            // simpleButton3
            // 
            simpleButton3.Location = new System.Drawing.Point(63, 5);
            simpleButton3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            simpleButton3.Name = "simpleButton3";
            simpleButton3.Size = new System.Drawing.Size(24, 26);
            simpleButton3.TabIndex = 2;
            simpleButton3.Text = "simpleButton3";
            simpleButton3.Click += BtnZoomOut30_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.ImageOptions.Image = Resources.BtnZoomIn30_Image;
            simpleButton2.Location = new System.Drawing.Point(4, 5);
            simpleButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new System.Drawing.Size(24, 26);
            simpleButton2.TabIndex = 1;
            simpleButton2.Text = "simpleButton2";
            simpleButton2.Click += BtnZoomIn30_Click;
            // 
            // simpleButton1
            // 
            simpleButton1.ImageOptions.Image = Resources.BtnZoom30_Image;
            simpleButton1.Location = new System.Drawing.Point(33, 5);
            simpleButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(24, 26);
            simpleButton1.TabIndex = 0;
            simpleButton1.Text = "simpleButton1";
            simpleButton1.Click += BtnZoom30_Click;
            // 
            // TCOPictureBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(pcBPane);
            Controls.Add(panel1);
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "TCOPictureBox";
            Size = new System.Drawing.Size(636, 466);
            Load += TCOPictureBox_Load;
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ((System.ComponentModel.ISupportInitialize)panel1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)propertyGridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pcBPane).EndInit();
            pcBPane.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pcBar).EndInit();
            pcBar.ResumeLayout(false);
            ResumeLayout(false);
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
