namespace Uixe.Watcher.V1
{
    partial class frmWeightTCOCall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWeightTCOCall));
            this.tsTabs = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.weightTCOConfirm1 = new Uixe.Watcher.V1.WeightTCOConfirm();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.tsTabs)).BeginInit();
            this.tsTabs.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsTabs
            // 
            this.tsTabs.AppearancePage.HeaderActive.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.tsTabs.AppearancePage.HeaderActive.Options.UseFont = true;
            this.tsTabs.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsTabs.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.tsTabs.Location = new System.Drawing.Point(2, 0);
            this.tsTabs.Name = "tsTabs";
            this.tsTabs.SelectedTabPage = this.xtraTabPage1;
            this.tsTabs.Size = new System.Drawing.Size(749, 591);
            this.tsTabs.TabIndex = 0;
            this.tsTabs.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.weightTCOConfirm1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(747, 559);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // weightTCOConfirm1
            // 
            this.weightTCOConfirm1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.weightTCOConfirm1.Appearance.Options.UseFont = true;
            this.weightTCOConfirm1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.weightTCOConfirm1.CanDo = false;
            this.weightTCOConfirm1.Location = new System.Drawing.Point(3, 0);
            this.weightTCOConfirm1.Name = "weightTCOConfirm1";
            this.weightTCOConfirm1.Size = new System.Drawing.Size(740, 551);
            this.weightTCOConfirm1.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(635, 471);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // frmWeightTCOCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 593);
            this.Controls.Add(this.tsTabs);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmWeightTCOCall.IconOptions.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWeightTCOCall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计重收费监控";
            ((System.ComponentModel.ISupportInitialize)(this.tsTabs)).EndInit();
            this.tsTabs.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tsTabs;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private WeightTCOConfirm weightTCOConfirm1;
    }
}