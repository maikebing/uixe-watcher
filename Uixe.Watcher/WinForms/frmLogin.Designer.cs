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
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
            Uixe.Watcher.Properties.Settings settings1 = new Uixe.Watcher.Properties.Settings();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lblPlaza = new DevExpress.XtraEditors.LabelControl();
            this.lblserver = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUser
            // 
            this.txtUser.EditValue = "tcoadmin";
            this.txtUser.Location = new System.Drawing.Point(216, 109);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.Appearance.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtUser.Properties.Appearance.Options.UseFont = true;
            this.txtUser.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtUser.Size = new System.Drawing.Size(305, 28);
            this.txtUser.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(217, 149);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(304, 28);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Appearance.Options.UseFont = true;
            this.lblInfo.Appearance.Options.UseForeColor = true;
            this.lblInfo.Location = new System.Drawing.Point(216, 189);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 16);
            this.lblInfo.TabIndex = 26;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(135, 111);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(43, 16);
            this.labelControl2.TabIndex = 27;
            this.labelControl2.Text = "工号:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(136, 156);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(43, 16);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "密码:";
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = global::Uixe.Watcher.Properties.Resources.login;
            this.btnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnLogin.ImageOptions.Image = global::Uixe.Watcher.Properties.Resources.login;
            this.btnLogin.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLogin.Location = new System.Drawing.Point(207, 214);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(118, 51);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::Uixe.Watcher.Properties.Resources.off;
            this.btnCancel.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(403, 214);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 51);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPlaza
            // 
            this.lblPlaza.Appearance.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPlaza.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblPlaza.Appearance.Options.UseFont = true;
            this.lblPlaza.Appearance.Options.UseForeColor = true;
            this.lblPlaza.Location = new System.Drawing.Point(207, 12);
            this.lblPlaza.Name = "lblPlaza";
            this.lblPlaza.Size = new System.Drawing.Size(132, 33);
            this.lblPlaza.TabIndex = 29;
            this.lblPlaza.Text = "车道监控";
            // 
            // lblserver
            // 
            this.lblserver.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblserver.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblserver.Appearance.Options.UseFont = true;
            this.lblserver.Appearance.Options.UseForeColor = true;
            this.lblserver.Location = new System.Drawing.Point(156, 287);
            this.lblserver.Name = "lblserver";
            this.lblserver.Size = new System.Drawing.Size(0, 16);
            this.lblserver.TabIndex = 32;
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.Appearance.BackColor = System.Drawing.Color.Fuchsia;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::Uixe.Watcher.Properties.Resources.login_bg;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(660, 319);
            this.Controls.Add(this.lblserver);
            this.Controls.Add(this.lblPlaza);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            settings1.SettingsKey = "";
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", settings1, "frmLogin_Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmLogin.IconOptions.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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