namespace Uixe.Watcher.WinForms
{
    partial class frmBillInfo
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
            btnOk = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            labelControl3 = new DevExpress.XtraEditors.LabelControl();
            libTitle = new DevExpress.XtraEditors.LabelControl();
            mpPorgress = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            txtBillCode = new DevExpress.XtraEditors.TextEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            txtBillNumber = new DevExpress.XtraEditors.TextEdit();
            labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)mpPorgress.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBillCode.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBillNumber.Properties).BeginInit();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Location = new System.Drawing.Point(117, 350);
            btnOk.Margin = new System.Windows.Forms.Padding(4);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(153, 47);
            btnOk.TabIndex = 3;
            btnOk.Text = "确认";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(372, 350);
            btnCancel.Margin = new System.Windows.Forms.Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(134, 47);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "取消";
            btnCancel.Click += btnCancel_Click;
            // 
            // labelControl3
            // 
            labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            labelControl3.Appearance.Options.UseFont = true;
            labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            labelControl3.Location = new System.Drawing.Point(27, 29);
            labelControl3.Margin = new System.Windows.Forms.Padding(4);
            labelControl3.Name = "labelControl3";
            labelControl3.Size = new System.Drawing.Size(636, 40);
            labelControl3.TabIndex = 7;
            labelControl3.Text = "请确认发票代码和发票号无误后确认，需要修改则直接修改。以最终确认时内容为准。 ";
            // 
            // libTitle
            // 
            libTitle.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            libTitle.Appearance.Options.UseFont = true;
            libTitle.Location = new System.Drawing.Point(126, 123);
            libTitle.Margin = new System.Windows.Forms.Padding(4);
            libTitle.Name = "libTitle";
            libTitle.Size = new System.Drawing.Size(80, 20);
            libTitle.TabIndex = 8;
            libTitle.Text = "工号姓名";
            // 
            // mpPorgress
            // 
            mpPorgress.EditValue = "";
            mpPorgress.Location = new System.Drawing.Point(12, 491);
            mpPorgress.Name = "mpPorgress";
            mpPorgress.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            mpPorgress.Properties.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            mpPorgress.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            mpPorgress.Properties.ShowTitle = true;
            mpPorgress.Properties.Stopped = true;
            mpPorgress.Size = new System.Drawing.Size(683, 37);
            mpPorgress.TabIndex = 12;
            mpPorgress.Visible = false;
            // 
            // txtBillCode
            // 
            txtBillCode.EditValue = "";
            txtBillCode.Location = new System.Drawing.Point(225, 193);
            txtBillCode.Name = "txtBillCode";
            txtBillCode.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            txtBillCode.Properties.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            txtBillCode.Properties.Appearance.Options.UseFont = true;
            txtBillCode.Properties.BeepOnError = true;
            txtBillCode.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtBillCode.Properties.MaxLength = 12;
            txtBillCode.Size = new System.Drawing.Size(279, 26);
            txtBillCode.TabIndex = 15;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new System.Drawing.Point(126, 199);
            labelControl1.Margin = new System.Windows.Forms.Padding(4);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(80, 20);
            labelControl1.TabIndex = 14;
            labelControl1.Text = "发票代码";
            // 
            // txtBillNumber
            // 
            txtBillNumber.EditValue = "";
            txtBillNumber.Location = new System.Drawing.Point(225, 286);
            txtBillNumber.Name = "txtBillNumber";
            txtBillNumber.Properties.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            txtBillNumber.Properties.Appearance.Options.UseFont = true;
            txtBillNumber.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtBillNumber.Properties.MaxLength = 8;
            txtBillNumber.Size = new System.Drawing.Size(279, 26);
            txtBillNumber.TabIndex = 17;
            // 
            // labelControl2
            // 
            labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            labelControl2.Appearance.Options.UseFont = true;
            labelControl2.Location = new System.Drawing.Point(126, 291);
            labelControl2.Margin = new System.Windows.Forms.Padding(4);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new System.Drawing.Size(80, 20);
            labelControl2.TabIndex = 16;
            labelControl2.Text = "发票号码";
            // 
            // frmBillInfo
            // 
            AcceptButton = btnOk;
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(707, 540);
            Controls.Add(txtBillNumber);
            Controls.Add(labelControl2);
            Controls.Add(txtBillCode);
            Controls.Add(labelControl1);
            Controls.Add(mpPorgress);
            Controls.Add(libTitle);
            Controls.Add(labelControl3);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmBillInfo";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "发票录入确认";
            TopMost = true;
            Load += frmBulktrans_Load;
            ((System.ComponentModel.ISupportInitialize)mpPorgress.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBillCode.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBillNumber.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl libTitle;
        private DevExpress.XtraEditors.MarqueeProgressBarControl mpPorgress;
        private DevExpress.XtraEditors.TextEdit txtBillCode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtBillNumber;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}