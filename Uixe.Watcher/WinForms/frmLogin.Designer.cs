namespace Uixe.Watcher
{
    partial class frmLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param Name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            txtUser = new DevExpress.XtraEditors.TextEdit();
            txtPassword = new DevExpress.XtraEditors.TextEdit();
            lblInfo = new DevExpress.XtraEditors.LabelControl();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            btnLogin = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            lblPlaza = new DevExpress.XtraEditors.LabelControl();
            lblserver = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            SuspendLayout();
            // 
            // txtUser
            // 
            txtUser.EditValue = "tcoadmin";
            txtUser.Location = new System.Drawing.Point(216, 109);
            txtUser.Name = "txtUser";
            txtUser.Properties.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            txtUser.Properties.Appearance.Options.UseFont = true;
            txtUser.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtUser.Size = new System.Drawing.Size(305, 28);
            txtUser.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(217, 149);
            txtPassword.Name = "txtPassword";
            txtPassword.Properties.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            txtPassword.Properties.Appearance.Options.UseFont = true;
            txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            txtPassword.Properties.UseSystemPasswordChar = true;
            txtPassword.Size = new System.Drawing.Size(304, 28);
            txtPassword.TabIndex = 1;
            txtPassword.KeyPress += txtPassword_KeyPress;
            // 
            // lblInfo
            // 
            lblInfo.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            lblInfo.Appearance.ForeColor = System.Drawing.Color.White;
            lblInfo.Appearance.Options.UseFont = true;
            lblInfo.Appearance.Options.UseForeColor = true;
            lblInfo.Location = new System.Drawing.Point(216, 189);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new System.Drawing.Size(0, 16);
            lblInfo.TabIndex = 26;
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Appearance.Options.UseForeColor = true;
            labelControl2.Location = new System.Drawing.Point(135, 111);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(43, 16);
            labelControl2.TabIndex = 27;
            labelControl2.Text = "工号:";
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.Appearance.Options.UseForeColor = true;
            labelControl3.Location = new System.Drawing.Point(136, 156);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(43, 16);
            labelControl3.TabIndex = 28;
            labelControl3.Text = "密码:";
            // 
            // btnLogin
            // 
            btnLogin.BackgroundImage = Properties.Resources.login;
            btnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            btnLogin.ImageOptions.Image = Properties.Resources.login;
            btnLogin.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnLogin.Location = new System.Drawing.Point(207, 214);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(118, 51);
            btnLogin.TabIndex = 2;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.ImageOptions.Image = Properties.Resources.off;
            btnCancel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnCancel.Location = new System.Drawing.Point(403, 214);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(118, 51);
            btnCancel.TabIndex = 3;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblPlaza
            // 
            lblPlaza.Appearance.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold);
            lblPlaza.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            lblPlaza.Appearance.Options.UseFont = true;
            lblPlaza.Appearance.Options.UseForeColor = true;
            lblPlaza.Location = new System.Drawing.Point(207, 12);
            lblPlaza.Name = "lblPlaza";
            lblPlaza.Size = new System.Drawing.Size(132, 33);
            lblPlaza.TabIndex = 29;
            lblPlaza.Text = "车道监控";
            // 
            // lblserver
            // 
            lblserver.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            lblserver.Appearance.ForeColor = System.Drawing.Color.White;
            lblserver.Appearance.Options.UseFont = true;
            lblserver.Appearance.Options.UseForeColor = true;
            lblserver.Location = new System.Drawing.Point(156, 287);
            lblserver.Name = "lblserver";
            lblserver.Size = new System.Drawing.Size(0, 16);
            lblserver.TabIndex = 32;
            // 
            // frmLogin
            // 
            AcceptButton = btnLogin;
            Appearance.BackColor = System.Drawing.Color.Fuchsia;
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            BackgroundImageStore = Properties.Resources.login_bg;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(660, 319);
            Controls.Add(lblserver);
            Controls.Add(lblPlaza);
            Controls.Add(btnCancel);
            Controls.Add(btnLogin);
            Controls.Add(labelControl3);
            Controls.Add(labelControl2);
            Controls.Add(lblInfo);
            Controls.Add(txtPassword);
            Controls.Add(txtUser);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Segoe UI", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            IconOptions.Icon = (System.Drawing.Icon)resources.GetObject("frmLogin.IconOptions.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmLogin";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "登录";
            TransparencyKey = System.Drawing.Color.Fuchsia;
            Load += frmLogin_Load;
            MouseDown += frmLogin_MouseDown;
            MouseMove += frmLogin_MouseMove;
            MouseUp += frmLogin_MouseUp;
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblPlaza;
        private DevExpress.XtraEditors.LabelControl lblserver;
    }
}