namespace Uixe.Watcher
{
    partial class frmTimeSync
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
            this.components = new System.ComponentModel.Container();
            this.btnHelp = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetAllClientTime = new DevExpress.XtraEditors.SimpleButton();
            this.btnSync = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtOut = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tmSync = new System.Windows.Forms.Timer(this.components);
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.laneTimeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvLane = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLaneName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLaneDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSetTime = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laneTimeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLane)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(26, 411);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(93, 35);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Text = "帮助";
            // 
            // btnGetAllClientTime
            // 
            this.btnGetAllClientTime.Location = new System.Drawing.Point(139, 411);
            this.btnGetAllClientTime.Name = "btnGetAllClientTime";
            this.btnGetAllClientTime.Size = new System.Drawing.Size(93, 35);
            this.btnGetAllClientTime.TabIndex = 13;
            this.btnGetAllClientTime.Text = "客户端时间(&G)";
            this.btnGetAllClientTime.Click += new System.EventHandler(this.btnGetAllClientTime_Click);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(365, 411);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(93, 35);
            this.btnSync.TabIndex = 14;
            this.btnSync.Text = "同步时间(&S)";
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(478, 411);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 35);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtOut
            // 
            this.txtOut.Location = new System.Drawing.Point(17, 270);
            this.txtOut.Name = "txtOut";
            this.txtOut.Properties.ReadOnly = true;
            this.txtOut.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtOut.Size = new System.Drawing.Size(567, 119);
            this.txtOut.TabIndex = 16;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 14);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "车道时间查看:";
            // 
            // tmSync
            // 
            this.tmSync.Tick += new System.EventHandler(this.tmSync_Tick);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.laneTimeBindingSource;
            this.gcMain.Location = new System.Drawing.Point(17, 39);
            this.gcMain.MainView = this.gvLane;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(567, 214);
            this.gcMain.TabIndex = 18;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLane});
            // 
            // gvLane
            // 
            this.gvLane.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLaneName,
            this.colLaneDateTime});
            this.gvLane.GridControl = this.gcMain;
            this.gvLane.Name = "gvLane";
            this.gvLane.OptionsCustomization.AllowGroup = false;
            this.gvLane.OptionsView.ShowGroupPanel = false;
            // 
            // colLaneName
            // 
            this.colLaneName.Caption = "车道名";
            this.colLaneName.FieldName = "LaneName";
            this.colLaneName.Name = "colLaneName";
            this.colLaneName.OptionsColumn.AllowEdit = false;
            this.colLaneName.OptionsColumn.ReadOnly = true;
            this.colLaneName.Visible = true;
            this.colLaneName.VisibleIndex = 0;
            this.colLaneName.Width = 78;
            // 
            // colLaneDateTime
            // 
            this.colLaneDateTime.Caption = "车道时间";
            this.colLaneDateTime.FieldName = "LaneDateTime";
            this.colLaneDateTime.Name = "colLaneDateTime";
            this.colLaneDateTime.OptionsColumn.AllowEdit = false;
            this.colLaneDateTime.OptionsColumn.ReadOnly = true;
            this.colLaneDateTime.Visible = true;
            this.colLaneDateTime.VisibleIndex = 1;
            this.colLaneDateTime.Width = 441;
            // 
            // btnSetTime
            // 
            this.btnSetTime.Location = new System.Drawing.Point(252, 411);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(93, 35);
            this.btnSetTime.TabIndex = 19;
            this.btnSetTime.Text = "强制设置时间";
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // frmTimeSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 458);
            this.Controls.Add(this.btnSetTime);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnGetAllClientTime);
            this.Controls.Add(this.btnHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTimeSync";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时间同步";
            this.Load += new System.EventHandler(this.frmTimeSync_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laneTimeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLane)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmSync;
        private DevExpress.XtraEditors.SimpleButton btnHelp;
        private DevExpress.XtraEditors.SimpleButton btnGetAllClientTime;
        private DevExpress.XtraEditors.SimpleButton btnSync;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.MemoEdit txtOut;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLane;
        private System.Windows.Forms.BindingSource laneTimeBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colLaneName;
        private DevExpress.XtraGrid.Columns.GridColumn colLaneDateTime;
        private DevExpress.XtraEditors.SimpleButton btnSetTime;
    }
}