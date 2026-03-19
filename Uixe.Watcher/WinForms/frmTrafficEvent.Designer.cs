namespace Uixe.Watcher.WinForms
{
    partial class frmTrafficEvent
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
            lblTitle = new DevExpress.XtraEditors.LabelControl();
            lblStationLane = new DevExpress.XtraEditors.LabelControl();
            groupBasic = new DevExpress.XtraEditors.GroupControl();
            txtQueueLength = new DevExpress.XtraEditors.TextEdit();
            lblQueueLength = new DevExpress.XtraEditors.LabelControl();
            txtDuration = new DevExpress.XtraEditors.TextEdit();
            lblDuration = new DevExpress.XtraEditors.LabelControl();
            txtStartTime = new DevExpress.XtraEditors.TextEdit();
            lblStartTime = new DevExpress.XtraEditors.LabelControl();
            txtCapTime = new DevExpress.XtraEditors.TextEdit();
            lblCapTime = new DevExpress.XtraEditors.LabelControl();
            txtLaneNo = new DevExpress.XtraEditors.TextEdit();
            lblLaneNo = new DevExpress.XtraEditors.LabelControl();
            txtEventType = new DevExpress.XtraEditors.TextEdit();
            lblEventType = new DevExpress.XtraEditors.LabelControl();
            txtRecordId = new DevExpress.XtraEditors.TextEdit();
            lblRecordId = new DevExpress.XtraEditors.LabelControl();
            groupMedia = new DevExpress.XtraEditors.GroupControl();
            tabMedia = new System.Windows.Forms.TabControl();
            lblMediaHint = new DevExpress.XtraEditors.LabelControl();
            groupSummary = new DevExpress.XtraEditors.GroupControl();
            memoSummary = new DevExpress.XtraEditors.MemoEdit();
            btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)groupBasic).BeginInit();
            groupBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtQueueLength.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDuration.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtStartTime.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtCapTime.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtLaneNo.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtEventType.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtRecordId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupMedia).BeginInit();
            groupMedia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupSummary).BeginInit();
            groupSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoSummary.Properties).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Appearance.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold);
            lblTitle.Appearance.ForeColor = System.Drawing.Color.Firebrick;
            lblTitle.Appearance.Options.UseFont = true;
            lblTitle.Appearance.Options.UseForeColor = true;
            lblTitle.Location = new System.Drawing.Point(24, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(163, 33);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "交通事件提醒";
            // 
            // lblStationLane
            // 
            lblStationLane.Appearance.Font = new System.Drawing.Font("宋体", 13.5F);
            lblStationLane.Appearance.Options.UseFont = true;
            lblStationLane.Location = new System.Drawing.Point(28, 62);
            lblStationLane.Name = "lblStationLane";
            lblStationLane.Size = new System.Drawing.Size(207, 18);
            lblStationLane.TabIndex = 1;
            lblStationLane.Text = "收费站：    车道：";
            // 
            // groupBasic
            // 
            groupBasic.Controls.Add(txtQueueLength);
            groupBasic.Controls.Add(lblQueueLength);
            groupBasic.Controls.Add(txtDuration);
            groupBasic.Controls.Add(lblDuration);
            groupBasic.Controls.Add(txtStartTime);
            groupBasic.Controls.Add(lblStartTime);
            groupBasic.Controls.Add(txtCapTime);
            groupBasic.Controls.Add(lblCapTime);
            groupBasic.Controls.Add(txtLaneNo);
            groupBasic.Controls.Add(lblLaneNo);
            groupBasic.Controls.Add(txtEventType);
            groupBasic.Controls.Add(lblEventType);
            groupBasic.Controls.Add(txtRecordId);
            groupBasic.Controls.Add(lblRecordId);
            groupBasic.Location = new System.Drawing.Point(24, 95);
            groupBasic.Name = "groupBasic";
            groupBasic.Size = new System.Drawing.Size(852, 205);
            groupBasic.TabIndex = 2;
            groupBasic.Text = "事件信息";
            // 
            // txtQueueLength
            // 
            txtQueueLength.Location = new System.Drawing.Point(132, 166);
            txtQueueLength.Name = "txtQueueLength";
            txtQueueLength.Properties.ReadOnly = true;
            txtQueueLength.Size = new System.Drawing.Size(696, 20);
            txtQueueLength.TabIndex = 13;
            // 
            // lblQueueLength
            // 
            lblQueueLength.Location = new System.Drawing.Point(30, 169);
            lblQueueLength.Name = "lblQueueLength";
            lblQueueLength.Size = new System.Drawing.Size(84, 14);
            lblQueueLength.TabIndex = 12;
            lblQueueLength.Text = "最大排队长度：";
            // 
            // txtDuration
            // 
            txtDuration.Location = new System.Drawing.Point(584, 123);
            txtDuration.Name = "txtDuration";
            txtDuration.Properties.ReadOnly = true;
            txtDuration.Size = new System.Drawing.Size(244, 20);
            txtDuration.TabIndex = 11;
            // 
            // lblDuration
            // 
            lblDuration.Location = new System.Drawing.Point(470, 126);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new System.Drawing.Size(60, 14);
            lblDuration.TabIndex = 10;
            lblDuration.Text = "统计时长：";
            // 
            // txtStartTime
            // 
            txtStartTime.Location = new System.Drawing.Point(132, 123);
            txtStartTime.Name = "txtStartTime";
            txtStartTime.Properties.ReadOnly = true;
            txtStartTime.Size = new System.Drawing.Size(244, 20);
            txtStartTime.TabIndex = 9;
            // 
            // lblStartTime
            // 
            lblStartTime.Location = new System.Drawing.Point(30, 126);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new System.Drawing.Size(60, 14);
            lblStartTime.TabIndex = 8;
            lblStartTime.Text = "开始时间：";
            // 
            // txtCapTime
            // 
            txtCapTime.Location = new System.Drawing.Point(584, 85);
            txtCapTime.Name = "txtCapTime";
            txtCapTime.Properties.ReadOnly = true;
            txtCapTime.Size = new System.Drawing.Size(244, 20);
            txtCapTime.TabIndex = 7;
            // 
            // lblCapTime
            // 
            lblCapTime.Location = new System.Drawing.Point(470, 88);
            lblCapTime.Name = "lblCapTime";
            lblCapTime.Size = new System.Drawing.Size(60, 14);
            lblCapTime.TabIndex = 6;
            lblCapTime.Text = "抓拍时间：";
            // 
            // txtLaneNo
            // 
            txtLaneNo.Location = new System.Drawing.Point(132, 85);
            txtLaneNo.Name = "txtLaneNo";
            txtLaneNo.Properties.ReadOnly = true;
            txtLaneNo.Size = new System.Drawing.Size(244, 20);
            txtLaneNo.TabIndex = 5;
            // 
            // lblLaneNo
            // 
            lblLaneNo.Location = new System.Drawing.Point(30, 88);
            lblLaneNo.Name = "lblLaneNo";
            lblLaneNo.Size = new System.Drawing.Size(48, 14);
            lblLaneNo.TabIndex = 4;
            lblLaneNo.Text = "车道号：";
            // 
            // txtEventType
            // 
            txtEventType.Location = new System.Drawing.Point(584, 47);
            txtEventType.Name = "txtEventType";
            txtEventType.Properties.ReadOnly = true;
            txtEventType.Size = new System.Drawing.Size(244, 20);
            txtEventType.TabIndex = 3;
            // 
            // lblEventType
            // 
            lblEventType.Location = new System.Drawing.Point(470, 50);
            lblEventType.Name = "lblEventType";
            lblEventType.Size = new System.Drawing.Size(60, 14);
            lblEventType.TabIndex = 2;
            lblEventType.Text = "事件类型：";
            // 
            // txtRecordId
            // 
            txtRecordId.Location = new System.Drawing.Point(132, 47);
            txtRecordId.Name = "txtRecordId";
            txtRecordId.Properties.ReadOnly = true;
            txtRecordId.Size = new System.Drawing.Size(244, 20);
            txtRecordId.TabIndex = 1;
            // 
            // lblRecordId
            // 
            lblRecordId.Location = new System.Drawing.Point(30, 50);
            lblRecordId.Name = "lblRecordId";
            lblRecordId.Size = new System.Drawing.Size(48, 14);
            lblRecordId.TabIndex = 0;
            lblRecordId.Text = "记录号：";
            // 
            // groupMedia
            // 
            groupMedia.Controls.Add(tabMedia);
            groupMedia.Controls.Add(lblMediaHint);
            groupMedia.Location = new System.Drawing.Point(24, 313);
            groupMedia.Name = "groupMedia";
            groupMedia.Size = new System.Drawing.Size(852, 378);
            groupMedia.TabIndex = 3;
            groupMedia.Text = "媒体预览";
            // 
            // tabMedia
            // 
            tabMedia.Location = new System.Drawing.Point(30, 55);
            tabMedia.Name = "tabMedia";
            tabMedia.SelectedIndex = 0;
            tabMedia.ShowToolTips = true;
            tabMedia.Size = new System.Drawing.Size(798, 303);
            tabMedia.TabIndex = 1;
            // 
            // lblMediaHint
            // 
            lblMediaHint.Location = new System.Drawing.Point(30, 32);
            lblMediaHint.Name = "lblMediaHint";
            lblMediaHint.Size = new System.Drawing.Size(156, 14);
            lblMediaHint.TabIndex = 0;
            lblMediaHint.Text = "切换选项卡可查看图片或视频";
            // 
            // groupSummary
            // 
            groupSummary.Controls.Add(memoSummary);
            groupSummary.Location = new System.Drawing.Point(24, 708);
            groupSummary.Name = "groupSummary";
            groupSummary.Size = new System.Drawing.Size(852, 101);
            groupSummary.TabIndex = 4;
            groupSummary.Text = "播报摘要";
            // 
            // memoSummary
            // 
            memoSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            memoSummary.Location = new System.Drawing.Point(2, 23);
            memoSummary.Name = "memoSummary";
            memoSummary.Properties.ReadOnly = true;
            memoSummary.Size = new System.Drawing.Size(700, 76);
            memoSummary.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.AccessibleDescription = "关闭交通事件提醒窗口";
            btnClose.AccessibleName = "关闭提醒";
            btnClose.Location = new System.Drawing.Point(752, 821);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(124, 46);
            btnClose.TabIndex = 5;
            btnClose.Text = "关闭";
            btnClose.Click += btnClose_Click;
            // 
            // frmTrafficEvent
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(900, 879);
            Controls.Add(btnClose);
            Controls.Add(groupSummary);
            Controls.Add(groupMedia);
            Controls.Add(groupBasic);
            Controls.Add(lblStationLane);
            Controls.Add(lblTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmTrafficEvent";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "交通事件提醒";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)groupBasic).EndInit();
            groupBasic.ResumeLayout(false);
            groupBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtQueueLength.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDuration.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtStartTime.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtCapTime.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtLaneNo.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtEventType.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtRecordId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupMedia).EndInit();
            groupMedia.ResumeLayout(false);
            groupMedia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupSummary).EndInit();
            groupSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)memoSummary.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblStationLane;
        private DevExpress.XtraEditors.GroupControl groupBasic;
        private DevExpress.XtraEditors.TextEdit txtQueueLength;
        private DevExpress.XtraEditors.LabelControl lblQueueLength;
        private DevExpress.XtraEditors.TextEdit txtDuration;
        private DevExpress.XtraEditors.LabelControl lblDuration;
        private DevExpress.XtraEditors.TextEdit txtStartTime;
        private DevExpress.XtraEditors.LabelControl lblStartTime;
        private DevExpress.XtraEditors.TextEdit txtCapTime;
        private DevExpress.XtraEditors.LabelControl lblCapTime;
        private DevExpress.XtraEditors.TextEdit txtLaneNo;
        private DevExpress.XtraEditors.LabelControl lblLaneNo;
        private DevExpress.XtraEditors.TextEdit txtEventType;
        private DevExpress.XtraEditors.LabelControl lblEventType;
        private DevExpress.XtraEditors.TextEdit txtRecordId;
        private DevExpress.XtraEditors.LabelControl lblRecordId;
        private DevExpress.XtraEditors.GroupControl groupMedia;
        private System.Windows.Forms.TabControl tabMedia;
        private DevExpress.XtraEditors.LabelControl lblMediaHint;
        private DevExpress.XtraEditors.GroupControl groupSummary;
        private DevExpress.XtraEditors.MemoEdit memoSummary;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}
