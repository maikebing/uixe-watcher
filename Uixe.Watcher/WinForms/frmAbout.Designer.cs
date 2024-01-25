namespace Uixe.Watcher
{
    partial class frmAbout
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            logoPictureBox = new System.Windows.Forms.PictureBox();
            labelProductName = new System.Windows.Forms.Label();
            labelVersion = new System.Windows.Forms.Label();
            labelCopyright = new System.Windows.Forms.Label();
            labelCompanyName = new System.Windows.Forms.Label();
            textBoxDescription = new DevExpress.XtraEditors.MemoEdit();
            okButton = new DevExpress.XtraEditors.SimpleButton();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textBoxDescription.Properties).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(labelCompanyName, 1, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
            tableLayoutPanel.Controls.Add(okButton, 1, 5);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(10, 10);
            tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.91837F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.62585F));
            tableLayoutPanel.Size = new System.Drawing.Size(516, 306);
            tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            logoPictureBox.Image = (System.Drawing.Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new System.Drawing.Point(4, 4);
            logoPictureBox.Margin = new System.Windows.Forms.Padding(4);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 6);
            logoPictureBox.Size = new System.Drawing.Size(162, 298);
            logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            logoPictureBox.Click += logoPictureBox_Click;
            // 
            // labelProductName
            // 
            labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelProductName.Location = new System.Drawing.Point(176, 0);
            labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            labelProductName.MaximumSize = new System.Drawing.Size(0, 16);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new System.Drawing.Size(336, 16);
            labelProductName.TabIndex = 19;
            labelProductName.Text = "产品名称";
            labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVersion.Location = new System.Drawing.Point(176, 30);
            labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            labelVersion.MaximumSize = new System.Drawing.Size(0, 16);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new System.Drawing.Size(336, 16);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "版本";
            labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCopyright.Location = new System.Drawing.Point(176, 60);
            labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            labelCopyright.MaximumSize = new System.Drawing.Size(0, 16);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new System.Drawing.Size(336, 16);
            labelCopyright.TabIndex = 21;
            labelCopyright.Text = "Copyright";
            labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCompanyName.Location = new System.Drawing.Point(176, 90);
            labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 4, 0);
            labelCompanyName.MaximumSize = new System.Drawing.Size(0, 16);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new System.Drawing.Size(336, 16);
            labelCompanyName.TabIndex = 22;
            labelCompanyName.Text = "公司名称";
            labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxDescription.Location = new System.Drawing.Point(174, 124);
            textBoxDescription.Margin = new System.Windows.Forms.Padding(4);
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Properties.ReadOnly = true;
            textBoxDescription.Size = new System.Drawing.Size(338, 131);
            textBoxDescription.TabIndex = 25;
            textBoxDescription.MouseDoubleClick += textBoxDescription_MouseDoubleClick;
            // 
            // okButton
            // 
            okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Location = new System.Drawing.Point(418, 266);
            okButton.Margin = new System.Windows.Forms.Padding(4);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(94, 36);
            okButton.TabIndex = 26;
            okButton.Text = "确定(&O)";
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(47, 329);
            labelControl1.Margin = new System.Windows.Forms.Padding(4);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(71, 15);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "labelControl1";
            // 
            // frmAbout
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(536, 326);
            Controls.Add(labelControl1);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            IconOptions.ShowIcon = false;
            Margin = new System.Windows.Forms.Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAbout";
            Padding = new System.Windows.Forms.Padding(10);
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "frmAbout";
            tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)textBoxDescription.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCompanyName;
        private DevExpress.XtraEditors.MemoEdit textBoxDescription;
        private DevExpress.XtraEditors.SimpleButton okButton;
        private System.Windows.Forms.Label labelCopyright;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
