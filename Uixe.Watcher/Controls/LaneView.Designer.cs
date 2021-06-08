namespace Uixe.Watcher.Controls
{
    partial class LaneView
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
            this.components = new System.ComponentModel.Container();
            this.gcExitLanes = new DevExpress.XtraGrid.GridControl();
            this.laneInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvExitLanes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLaneNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coluserNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarKind1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCharge1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCarType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCardBoxExit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cardBoxControlRepositoryItem2 = new Uixe.Watcher.Controls.CardBoxControlRepositoryItem();
            this.colStartWorkTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colyupengdeng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colCoil1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCoil2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRSU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYellow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWeightStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcReaderStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQRPay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Coil3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJiaoTong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrint = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Coil4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLanGan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaoJing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVPR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCamera = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNetWork = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnVNC = new DevExpress.XtraBars.BarButtonItem();
            this.btnPing = new DevExpress.XtraBars.BarButtonItem();
            this.btnReboot = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemotLane = new DevExpress.XtraBars.BarButtonItem();
            this.rpbExit = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.radialMenu1 = new DevExpress.XtraBars.Ribbon.RadialMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcExitLanes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.laneInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExitLanes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBoxControlRepositoryItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpbExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcExitLanes
            // 
            this.gcExitLanes.DataSource = this.laneInfoBindingSource;
            this.gcExitLanes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcExitLanes.Location = new System.Drawing.Point(0, 0);
            this.gcExitLanes.MainView = this.gvExitLanes;
            this.gcExitLanes.MenuManager = this.barManager1;
            this.gcExitLanes.Name = "gcExitLanes";
            this.gcExitLanes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.rpbExit,
            this.cardBoxControlRepositoryItem2,
            this.repositoryItemTimeEdit1});
            this.gcExitLanes.Size = new System.Drawing.Size(1518, 576);
            this.gcExitLanes.TabIndex = 9;
            this.gcExitLanes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExitLanes});
            this.gcExitLanes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gcExitLanes_MouseUp);
            // 
            // laneInfoBindingSource
            // 
            this.laneInfoBindingSource.DataSource = typeof(Uixe.Watcher.Msg.LaneInfo);
            // 
            // gvExitLanes
            // 
            this.gvExitLanes.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExitLanes.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExitLanes.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.Empty.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gvExitLanes.Appearance.EvenRow.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.gvExitLanes.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.EvenRow.Options.UseFont = true;
            this.gvExitLanes.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExitLanes.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExitLanes.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvExitLanes.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gvExitLanes.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gvExitLanes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExitLanes.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExitLanes.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExitLanes.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExitLanes.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.GroupButton.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExitLanes.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExitLanes.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvExitLanes.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExitLanes.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvExitLanes.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gvExitLanes.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.GroupRow.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.GroupRow.Options.UseFont = true;
            this.gvExitLanes.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvExitLanes.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvExitLanes.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gvExitLanes.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvExitLanes.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvExitLanes.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvExitLanes.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gvExitLanes.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gvExitLanes.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gvExitLanes.Appearance.HorzLine.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gvExitLanes.Appearance.OddRow.ForeColor = System.Drawing.Color.Navy;
            this.gvExitLanes.Appearance.OddRow.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.OddRow.Options.UseFont = true;
            this.gvExitLanes.Appearance.OddRow.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gvExitLanes.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gvExitLanes.Appearance.Preview.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.Preview.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gvExitLanes.Appearance.Row.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.Row.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gvExitLanes.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvExitLanes.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvExitLanes.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvExitLanes.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gvExitLanes.Appearance.VertLine.Options.UseBackColor = true;
            this.gvExitLanes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLaneNo,
            this.colUserName,
            this.coluserNumber,
            this.colWorkState,
            this.colCarKind1,
            this.colCharge1,
            this.colCarType,
            this.colWorkMode,
            this.gcCardBoxExit,
            this.colStartWorkTime,
            this.colyupengdeng,
            this.colCoil1,
            this.colCoil2,
            this.colRSU,
            this.colYellow,
            this.gcWeightStatus,
            this.gcReaderStatus,
            this.colQRPay,
            this.Coil3,
            this.colJiaoTong,
            this.colPrint,
            this.Coil4,
            this.colLanGan,
            this.colBaoJing,
            this.colVPR,
            this.colCamera,
            this.colNetWork});
            this.gvExitLanes.DetailHeight = 300;
            this.gvExitLanes.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvExitLanes.GridControl = this.gcExitLanes;
            this.gvExitLanes.Name = "gvExitLanes";
            this.gvExitLanes.OptionsMenu.EnableColumnMenu = false;
            this.gvExitLanes.OptionsMenu.EnableFooterMenu = false;
            this.gvExitLanes.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvExitLanes.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvExitLanes.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvExitLanes.OptionsView.EnableAppearanceEvenRow = true;
            this.gvExitLanes.OptionsView.EnableAppearanceOddRow = true;
            this.gvExitLanes.OptionsView.ShowGroupPanel = false;
            this.gvExitLanes.RowHeight = 34;
            this.gvExitLanes.RowSeparatorHeight = 3;
            this.gvExitLanes.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gv_RowStyle);
            // 
            // colLaneNo
            // 
            this.colLaneNo.Caption = "车道";
            this.colLaneNo.FieldName = "LaneName";
            this.colLaneNo.MinWidth = 17;
            this.colLaneNo.Name = "colLaneNo";
            this.colLaneNo.OptionsColumn.AllowEdit = false;
            this.colLaneNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLaneNo.OptionsColumn.ReadOnly = true;
            this.colLaneNo.OptionsFilter.AllowFilter = false;
            this.colLaneNo.Visible = true;
            this.colLaneNo.VisibleIndex = 0;
            this.colLaneNo.Width = 48;
            // 
            // colUserName
            // 
            this.colUserName.Caption = "姓名";
            this.colUserName.FieldName = "CollName";
            this.colUserName.MinWidth = 17;
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colUserName.OptionsColumn.ReadOnly = true;
            this.colUserName.OptionsFilter.AllowFilter = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 1;
            this.colUserName.Width = 119;
            // 
            // coluserNumber
            // 
            this.coluserNumber.Caption = "工号";
            this.coluserNumber.FieldName = "CollNo";
            this.coluserNumber.MinWidth = 17;
            this.coluserNumber.Name = "coluserNumber";
            this.coluserNumber.OptionsColumn.AllowEdit = false;
            this.coluserNumber.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.coluserNumber.OptionsColumn.ReadOnly = true;
            this.coluserNumber.OptionsFilter.AllowFilter = false;
            this.coluserNumber.Visible = true;
            this.coluserNumber.VisibleIndex = 2;
            this.coluserNumber.Width = 87;
            // 
            // colWorkState
            // 
            this.colWorkState.Caption = "工作状态";
            this.colWorkState.FieldName = "ClientMsg";
            this.colWorkState.MinWidth = 17;
            this.colWorkState.Name = "colWorkState";
            this.colWorkState.OptionsColumn.AllowEdit = false;
            this.colWorkState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colWorkState.OptionsColumn.ReadOnly = true;
            this.colWorkState.OptionsFilter.AllowFilter = false;
            this.colWorkState.Visible = true;
            this.colWorkState.VisibleIndex = 3;
            this.colWorkState.Width = 145;
            // 
            // colCarKind1
            // 
            this.colCarKind1.Caption = "车型";
            this.colCarKind1.FieldName = "CarType";
            this.colCarKind1.MinWidth = 17;
            this.colCarKind1.Name = "colCarKind1";
            this.colCarKind1.OptionsColumn.AllowEdit = false;
            this.colCarKind1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCarKind1.OptionsColumn.ReadOnly = true;
            this.colCarKind1.OptionsFilter.AllowFilter = false;
            this.colCarKind1.Visible = true;
            this.colCarKind1.VisibleIndex = 4;
            this.colCarKind1.Width = 51;
            // 
            // colCharge1
            // 
            this.colCharge1.Caption = "金额";
            this.colCharge1.FieldName = "Money";
            this.colCharge1.MinWidth = 17;
            this.colCharge1.Name = "colCharge1";
            this.colCharge1.OptionsColumn.AllowEdit = false;
            this.colCharge1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCharge1.OptionsColumn.ReadOnly = true;
            this.colCharge1.OptionsFilter.AllowFilter = false;
            this.colCharge1.Visible = true;
            this.colCharge1.VisibleIndex = 5;
            this.colCharge1.Width = 54;
            // 
            // colCarType
            // 
            this.colCarType.Caption = "车种";
            this.colCarType.FieldName = "CarKind";
            this.colCarType.MinWidth = 17;
            this.colCarType.Name = "colCarType";
            this.colCarType.OptionsColumn.AllowEdit = false;
            this.colCarType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCarType.OptionsColumn.ReadOnly = true;
            this.colCarType.OptionsFilter.AllowFilter = false;
            this.colCarType.Visible = true;
            this.colCarType.VisibleIndex = 6;
            this.colCarType.Width = 73;
            // 
            // colWorkMode
            // 
            this.colWorkMode.Caption = "工作模式";
            this.colWorkMode.FieldName = "WrokMode";
            this.colWorkMode.MinWidth = 17;
            this.colWorkMode.Name = "colWorkMode";
            this.colWorkMode.OptionsColumn.AllowEdit = false;
            this.colWorkMode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colWorkMode.OptionsColumn.ReadOnly = true;
            this.colWorkMode.OptionsFilter.AllowFilter = false;
            this.colWorkMode.Visible = true;
            this.colWorkMode.VisibleIndex = 7;
            this.colWorkMode.Width = 90;
            // 
            // gcCardBoxExit
            // 
            this.gcCardBoxExit.Caption = "卡箱";
            this.gcCardBoxExit.ColumnEdit = this.cardBoxControlRepositoryItem2;
            this.gcCardBoxExit.FieldName = "CardBox";
            this.gcCardBoxExit.MaxWidth = 69;
            this.gcCardBoxExit.MinWidth = 34;
            this.gcCardBoxExit.Name = "gcCardBoxExit";
            this.gcCardBoxExit.OptionsColumn.AllowEdit = false;
            this.gcCardBoxExit.Visible = true;
            this.gcCardBoxExit.VisibleIndex = 9;
            this.gcCardBoxExit.Width = 69;
            // 
            // cardBoxControlRepositoryItem2
            // 
            this.cardBoxControlRepositoryItem2.AutoHeight = false;
            this.cardBoxControlRepositoryItem2.Name = "cardBoxControlRepositoryItem2";
            this.cardBoxControlRepositoryItem2.ReadOnly = true;
            // 
            // colStartWorkTime
            // 
            this.colStartWorkTime.Caption = "上班时间";
            this.colStartWorkTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colStartWorkTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colStartWorkTime.FieldName = "JobBeginTime";
            this.colStartWorkTime.MinWidth = 17;
            this.colStartWorkTime.Name = "colStartWorkTime";
            this.colStartWorkTime.OptionsColumn.AllowEdit = false;
            this.colStartWorkTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colStartWorkTime.OptionsColumn.ReadOnly = true;
            this.colStartWorkTime.OptionsFilter.AllowFilter = false;
            this.colStartWorkTime.Visible = true;
            this.colStartWorkTime.VisibleIndex = 8;
            this.colStartWorkTime.Width = 171;
            // 
            // colyupengdeng
            // 
            this.colyupengdeng.Caption = "雨棚灯";
            this.colyupengdeng.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colyupengdeng.FieldName = "YuPengDeng";
            this.colyupengdeng.MaxWidth = 47;
            this.colyupengdeng.MinWidth = 33;
            this.colyupengdeng.Name = "colyupengdeng";
            this.colyupengdeng.OptionsColumn.AllowEdit = false;
            this.colyupengdeng.OptionsColumn.AllowSize = false;
            this.colyupengdeng.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colyupengdeng.OptionsColumn.ReadOnly = true;
            this.colyupengdeng.OptionsFilter.AllowFilter = false;
            this.colyupengdeng.Visible = true;
            this.colyupengdeng.VisibleIndex = 10;
            this.colyupengdeng.Width = 33;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // colCoil1
            // 
            this.colCoil1.Caption = "触发";
            this.colCoil1.FieldName = "Coil1";
            this.colCoil1.MaxWidth = 47;
            this.colCoil1.MinWidth = 33;
            this.colCoil1.Name = "colCoil1";
            this.colCoil1.OptionsColumn.AllowEdit = false;
            this.colCoil1.OptionsColumn.ReadOnly = true;
            this.colCoil1.Visible = true;
            this.colCoil1.VisibleIndex = 20;
            this.colCoil1.Width = 33;
            // 
            // colCoil2
            // 
            this.colCoil2.Caption = "交易";
            this.colCoil2.FieldName = "Coil2";
            this.colCoil2.MaxWidth = 47;
            this.colCoil2.MinWidth = 33;
            this.colCoil2.Name = "colCoil2";
            this.colCoil2.OptionsColumn.AllowEdit = false;
            this.colCoil2.OptionsColumn.ReadOnly = true;
            this.colCoil2.Visible = true;
            this.colCoil2.VisibleIndex = 21;
            this.colCoil2.Width = 33;
            // 
            // colRSU
            // 
            this.colRSU.FieldName = "RSU";
            this.colRSU.MaxWidth = 47;
            this.colRSU.MinWidth = 33;
            this.colRSU.Name = "colRSU";
            this.colRSU.OptionsColumn.AllowEdit = false;
            this.colRSU.OptionsColumn.ReadOnly = true;
            this.colRSU.Visible = true;
            this.colRSU.VisibleIndex = 19;
            this.colRSU.Width = 33;
            // 
            // colYellow
            // 
            this.colYellow.Caption = "小黄人";
            this.colYellow.FieldName = "Yellow";
            this.colYellow.MaxWidth = 47;
            this.colYellow.MinWidth = 33;
            this.colYellow.Name = "colYellow";
            this.colYellow.OptionsColumn.AllowEdit = false;
            this.colYellow.OptionsColumn.ReadOnly = true;
            this.colYellow.Visible = true;
            this.colYellow.VisibleIndex = 26;
            this.colYellow.Width = 38;
            // 
            // gcWeightStatus
            // 
            this.gcWeightStatus.Caption = "称台";
            this.gcWeightStatus.FieldName = "Weight";
            this.gcWeightStatus.MaxWidth = 47;
            this.gcWeightStatus.MinWidth = 33;
            this.gcWeightStatus.Name = "gcWeightStatus";
            this.gcWeightStatus.OptionsColumn.AllowEdit = false;
            this.gcWeightStatus.OptionsColumn.ReadOnly = true;
            this.gcWeightStatus.Visible = true;
            this.gcWeightStatus.VisibleIndex = 11;
            this.gcWeightStatus.Width = 33;
            // 
            // gcReaderStatus
            // 
            this.gcReaderStatus.Caption = "读卡器";
            this.gcReaderStatus.ColumnEdit = this.repositoryItemPictureEdit1;
            this.gcReaderStatus.FieldName = "Reader";
            this.gcReaderStatus.MaxWidth = 47;
            this.gcReaderStatus.MinWidth = 33;
            this.gcReaderStatus.Name = "gcReaderStatus";
            this.gcReaderStatus.OptionsColumn.AllowEdit = false;
            this.gcReaderStatus.OptionsColumn.ReadOnly = true;
            this.gcReaderStatus.Visible = true;
            this.gcReaderStatus.VisibleIndex = 13;
            this.gcReaderStatus.Width = 33;
            // 
            // colQRPay
            // 
            this.colQRPay.Caption = "移动支付";
            this.colQRPay.FieldName = "QRPay";
            this.colQRPay.MaxWidth = 47;
            this.colQRPay.MinWidth = 33;
            this.colQRPay.Name = "colQRPay";
            this.colQRPay.OptionsColumn.AllowEdit = false;
            this.colQRPay.OptionsColumn.ReadOnly = true;
            this.colQRPay.Visible = true;
            this.colQRPay.VisibleIndex = 24;
            this.colQRPay.Width = 41;
            // 
            // Coil3
            // 
            this.Coil3.Caption = "抓拍";
            this.Coil3.ColumnEdit = this.repositoryItemPictureEdit1;
            this.Coil3.FieldName = "Coil3";
            this.Coil3.MaxWidth = 47;
            this.Coil3.MinWidth = 33;
            this.Coil3.Name = "Coil3";
            this.Coil3.OptionsColumn.AllowEdit = false;
            this.Coil3.OptionsColumn.AllowSize = false;
            this.Coil3.OptionsColumn.ReadOnly = true;
            this.Coil3.OptionsFilter.AllowFilter = false;
            this.Coil3.Visible = true;
            this.Coil3.VisibleIndex = 12;
            this.Coil3.Width = 33;
            // 
            // colJiaoTong
            // 
            this.colJiaoTong.Caption = "交通灯";
            this.colJiaoTong.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colJiaoTong.FieldName = "JiaoTongDeng";
            this.colJiaoTong.MaxWidth = 47;
            this.colJiaoTong.MinWidth = 33;
            this.colJiaoTong.Name = "colJiaoTong";
            this.colJiaoTong.OptionsColumn.AllowEdit = false;
            this.colJiaoTong.OptionsColumn.AllowSize = false;
            this.colJiaoTong.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colJiaoTong.OptionsColumn.ReadOnly = true;
            this.colJiaoTong.OptionsFilter.AllowFilter = false;
            this.colJiaoTong.Visible = true;
            this.colJiaoTong.VisibleIndex = 15;
            this.colJiaoTong.Width = 33;
            // 
            // colPrint
            // 
            this.colPrint.Caption = "打印机";
            this.colPrint.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colPrint.FieldName = "Printer";
            this.colPrint.MaxWidth = 47;
            this.colPrint.MinWidth = 33;
            this.colPrint.Name = "colPrint";
            this.colPrint.OptionsColumn.AllowEdit = false;
            this.colPrint.OptionsColumn.AllowSize = false;
            this.colPrint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPrint.OptionsColumn.ReadOnly = true;
            this.colPrint.OptionsFilter.AllowFilter = false;
            this.colPrint.Visible = true;
            this.colPrint.VisibleIndex = 14;
            this.colPrint.Width = 33;
            // 
            // Coil4
            // 
            this.Coil4.Caption = "离开";
            this.Coil4.ColumnEdit = this.repositoryItemPictureEdit1;
            this.Coil4.FieldName = "Coil4";
            this.Coil4.MaxWidth = 47;
            this.Coil4.MinWidth = 33;
            this.Coil4.Name = "Coil4";
            this.Coil4.OptionsColumn.AllowEdit = false;
            this.Coil4.OptionsColumn.AllowSize = false;
            this.Coil4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.Coil4.OptionsColumn.ReadOnly = true;
            this.Coil4.OptionsFilter.AllowFilter = false;
            this.Coil4.Visible = true;
            this.Coil4.VisibleIndex = 17;
            this.Coil4.Width = 33;
            // 
            // colLanGan
            // 
            this.colLanGan.Caption = "栏杆";
            this.colLanGan.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colLanGan.FieldName = "LanGan";
            this.colLanGan.MaxWidth = 47;
            this.colLanGan.MinWidth = 33;
            this.colLanGan.Name = "colLanGan";
            this.colLanGan.OptionsColumn.AllowEdit = false;
            this.colLanGan.OptionsColumn.AllowSize = false;
            this.colLanGan.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLanGan.OptionsColumn.ReadOnly = true;
            this.colLanGan.OptionsFilter.AllowFilter = false;
            this.colLanGan.Visible = true;
            this.colLanGan.VisibleIndex = 16;
            this.colLanGan.Width = 33;
            // 
            // colBaoJing
            // 
            this.colBaoJing.Caption = "黄闪";
            this.colBaoJing.FieldName = "BaoJing";
            this.colBaoJing.MaxWidth = 47;
            this.colBaoJing.MinWidth = 33;
            this.colBaoJing.Name = "colBaoJing";
            this.colBaoJing.OptionsColumn.AllowEdit = false;
            this.colBaoJing.OptionsColumn.ReadOnly = true;
            this.colBaoJing.Visible = true;
            this.colBaoJing.VisibleIndex = 22;
            this.colBaoJing.Width = 33;
            // 
            // colVPR
            // 
            this.colVPR.Caption = "车牌识别";
            this.colVPR.FieldName = "VPR";
            this.colVPR.MaxWidth = 47;
            this.colVPR.MinWidth = 33;
            this.colVPR.Name = "colVPR";
            this.colVPR.OptionsColumn.AllowEdit = false;
            this.colVPR.OptionsColumn.ReadOnly = true;
            this.colVPR.Visible = true;
            this.colVPR.VisibleIndex = 25;
            this.colVPR.Width = 45;
            // 
            // colCamera
            // 
            this.colCamera.Caption = "车道摄像机";
            this.colCamera.FieldName = "Camera";
            this.colCamera.MaxWidth = 47;
            this.colCamera.MinWidth = 33;
            this.colCamera.Name = "colCamera";
            this.colCamera.OptionsColumn.AllowEdit = false;
            this.colCamera.OptionsColumn.ReadOnly = true;
            this.colCamera.Visible = true;
            this.colCamera.VisibleIndex = 23;
            this.colCamera.Width = 33;
            // 
            // colNetWork
            // 
            this.colNetWork.Caption = "网络";
            this.colNetWork.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colNetWork.FieldName = "Network";
            this.colNetWork.MaxWidth = 47;
            this.colNetWork.MinWidth = 33;
            this.colNetWork.Name = "colNetWork";
            this.colNetWork.OptionsColumn.AllowEdit = false;
            this.colNetWork.OptionsColumn.AllowSize = false;
            this.colNetWork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colNetWork.OptionsColumn.ReadOnly = true;
            this.colNetWork.OptionsFilter.AllowFilter = false;
            this.colNetWork.Visible = true;
            this.colNetWork.VisibleIndex = 18;
            this.colNetWork.Width = 33;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnVNC,
            this.btnPing,
            this.btnReboot,
            this.btnRemotLane});
            this.barManager1.MaxItemId = 4;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1518, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 576);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1518, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 576);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1518, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 576);
            // 
            // btnVNC
            // 
            this.btnVNC.Caption = "远程桌面";
            this.btnVNC.Id = 0;
            this.btnVNC.Name = "btnVNC";
            this.btnVNC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnVNC_ItemClick);
            // 
            // btnPing
            // 
            this.btnPing.Caption = "测试网络";
            this.btnPing.Id = 1;
            this.btnPing.Name = "btnPing";
            // 
            // btnReboot
            // 
            this.btnReboot.Caption = "重启车道";
            this.btnReboot.Id = 2;
            this.btnReboot.Name = "btnReboot";
            // 
            // btnRemotLane
            // 
            this.btnRemotLane.Caption = "远程控制";
            this.btnRemotLane.Id = 3;
            this.btnRemotLane.Name = "btnRemotLane";
            this.btnRemotLane.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRemotLane_ItemClick);
            // 
            // rpbExit
            // 
            this.rpbExit.Name = "rpbExit";
            // 
            // repositoryItemTimeEdit1
            // 
            this.repositoryItemTimeEdit1.AutoHeight = false;
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemTimeEdit1.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            // 
            // radialMenu1
            // 
            this.radialMenu1.Glyph = global::Uixe.Watcher.Properties.Resources.promotion;
            this.radialMenu1.ItemAutoSize = DevExpress.XtraBars.Ribbon.RadialMenuItemAutoSize.Spring;
            this.radialMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnVNC),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPing),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnReboot),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRemotLane)});
            this.radialMenu1.Manager = this.barManager1;
            this.radialMenu1.Name = "radialMenu1";
            // 
            // LaneView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcExitLanes);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Name = "LaneView";
            this.Size = new System.Drawing.Size(1518, 576);
            ((System.ComponentModel.ISupportInitialize)(this.gcExitLanes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.laneInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExitLanes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardBoxControlRepositoryItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpbExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radialMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcExitLanes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvExitLanes;
        private DevExpress.XtraGrid.Columns.GridColumn colLaneNo;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn coluserNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkState;
        private DevExpress.XtraGrid.Columns.GridColumn colCarKind1;
        private DevExpress.XtraGrid.Columns.GridColumn colCharge1;
        private DevExpress.XtraGrid.Columns.GridColumn colCarType;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkMode;
        private DevExpress.XtraGrid.Columns.GridColumn colStartWorkTime;
        private DevExpress.XtraGrid.Columns.GridColumn colyupengdeng;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colJiaoTong;
        private DevExpress.XtraGrid.Columns.GridColumn colLanGan;
        private DevExpress.XtraGrid.Columns.GridColumn colPrint;
        private DevExpress.XtraGrid.Columns.GridColumn colNetWork;
        private DevExpress.XtraGrid.Columns.GridColumn Coil3;
        private DevExpress.XtraGrid.Columns.GridColumn Coil4;
        private DevExpress.XtraGrid.Columns.GridColumn gcReaderStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gcWeightStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gcCardBoxExit;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar rpbExit;
        private CardBoxControlRepositoryItem cardBoxControlRepositoryItem2;
        private System.Windows.Forms.BindingSource laneInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCoil1;
        private DevExpress.XtraGrid.Columns.GridColumn colCoil2;
        private DevExpress.XtraGrid.Columns.GridColumn colRSU;
        private DevExpress.XtraGrid.Columns.GridColumn colYellow;
        private DevExpress.XtraGrid.Columns.GridColumn colQRPay;
        private DevExpress.XtraGrid.Columns.GridColumn colBaoJing;
        private DevExpress.XtraGrid.Columns.GridColumn colVPR;
        private DevExpress.XtraGrid.Columns.GridColumn colCamera;
        private DevExpress.XtraBars.Ribbon.RadialMenu radialMenu1;
        private DevExpress.XtraBars.BarButtonItem btnVNC;
        private DevExpress.XtraBars.BarButtonItem btnPing;
        private DevExpress.XtraBars.BarButtonItem btnReboot;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnRemotLane;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
    }
}
