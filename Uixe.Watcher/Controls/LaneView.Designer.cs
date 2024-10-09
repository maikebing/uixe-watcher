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
        /// <param Name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
            components = new System.ComponentModel.Container();
            gcExitLanes = new DevExpress.XtraGrid.GridControl();
            laneInfoBindingSource = new System.Windows.Forms.BindingSource(components);
            gvExitLanes = new DevExpress.XtraGrid.Views.Grid.GridView();
            colPlazaId = new DevExpress.XtraGrid.Columns.GridColumn();
            luePlaza = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            tPlazaBindingSource = new System.Windows.Forms.BindingSource(components);
            colLaneNo = new DevExpress.XtraGrid.Columns.GridColumn();
            colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            coluserNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            colWorkState = new DevExpress.XtraGrid.Columns.GridColumn();
            colCarKind1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colCharge1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colCarType = new DevExpress.XtraGrid.Columns.GridColumn();
            colWorkMode = new DevExpress.XtraGrid.Columns.GridColumn();
            gcCardBoxExit = new DevExpress.XtraGrid.Columns.GridColumn();
            cardBoxControlRepositoryItem2 = new CardBoxControlRepositoryItem();
            colStartWorkTime = new DevExpress.XtraGrid.Columns.GridColumn();
            colyupengdeng = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            colCoil1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colCoil2 = new DevExpress.XtraGrid.Columns.GridColumn();
            colRSU = new DevExpress.XtraGrid.Columns.GridColumn();
            colYellow = new DevExpress.XtraGrid.Columns.GridColumn();
            gcWeightStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            gcReaderStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            colQRPay = new DevExpress.XtraGrid.Columns.GridColumn();
            Coil3 = new DevExpress.XtraGrid.Columns.GridColumn();
            colJiaoTong = new DevExpress.XtraGrid.Columns.GridColumn();
            colPrint = new DevExpress.XtraGrid.Columns.GridColumn();
            Coil4 = new DevExpress.XtraGrid.Columns.GridColumn();
            colLanGan = new DevExpress.XtraGrid.Columns.GridColumn();
            colBaoJing = new DevExpress.XtraGrid.Columns.GridColumn();
            colVPR = new DevExpress.XtraGrid.Columns.GridColumn();
            colCamera = new DevExpress.XtraGrid.Columns.GridColumn();
            colNetWork = new DevExpress.XtraGrid.Columns.GridColumn();
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            btnVNC = new DevExpress.XtraBars.BarButtonItem();
            btnPing = new DevExpress.XtraBars.BarButtonItem();
            btnRemotLane = new DevExpress.XtraBars.BarButtonItem();
            btnLaneReboot = new DevExpress.XtraBars.BarButtonItem();
            btnLaneControl = new DevExpress.XtraBars.BarSubItem();
            rpbExit = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            cbxPlaza = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            radialMenu1 = new DevExpress.XtraBars.Ribbon.RadialMenu(components);
            ((System.ComponentModel.ISupportInitialize)gcExitLanes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)laneInfoBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gvExitLanes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)luePlaza).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tPlazaBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cardBoxControlRepositoryItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rpbExit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemTimeEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cbxPlaza).BeginInit();
            ((System.ComponentModel.ISupportInitialize)radialMenu1).BeginInit();
            SuspendLayout();
            // 
            // gcExitLanes
            // 
            gcExitLanes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gcExitLanes.DataSource = laneInfoBindingSource;
            gcExitLanes.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            gcExitLanes.Location = new System.Drawing.Point(0, 0);
            gcExitLanes.MainView = gvExitLanes;
            gcExitLanes.Margin = new System.Windows.Forms.Padding(4);
            gcExitLanes.MenuManager = barManager1;
            gcExitLanes.Name = "gcExitLanes";
            gcExitLanes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemPictureEdit1, rpbExit, cardBoxControlRepositoryItem2, repositoryItemTimeEdit1, cbxPlaza, luePlaza });
            gcExitLanes.Size = new System.Drawing.Size(1513, 570);
            gcExitLanes.TabIndex = 9;
            gcExitLanes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvExitLanes });
            gcExitLanes.MouseUp += gcExitLanes_MouseUp;
            // 
            // laneInfoBindingSource
            // 
            laneInfoBindingSource.DataSource = typeof(Msg.LaneInfo);
            // 
            // gvExitLanes
            // 
            gvExitLanes.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            gvExitLanes.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gvExitLanes.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            gvExitLanes.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            gvExitLanes.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            gvExitLanes.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
            gvExitLanes.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(154, 190, 243);
            gvExitLanes.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(247, 251, 255);
            gvExitLanes.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gvExitLanes.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            gvExitLanes.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            gvExitLanes.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            gvExitLanes.Appearance.Empty.BackColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.Empty.Options.UseBackColor = true;
            gvExitLanes.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(231, 242, 254);
            gvExitLanes.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 12F);
            gvExitLanes.Appearance.EvenRow.ForeColor = System.Drawing.Color.PaleVioletRed;
            gvExitLanes.Appearance.EvenRow.Options.UseBackColor = true;
            gvExitLanes.Appearance.EvenRow.Options.UseFont = true;
            gvExitLanes.Appearance.EvenRow.Options.UseForeColor = true;
            gvExitLanes.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            gvExitLanes.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gvExitLanes.Appearance.FilterCloseButton.Options.UseBackColor = true;
            gvExitLanes.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            gvExitLanes.Appearance.FilterCloseButton.Options.UseForeColor = true;
            gvExitLanes.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(62, 109, 185);
            gvExitLanes.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.FilterPanel.Options.UseBackColor = true;
            gvExitLanes.Appearance.FilterPanel.Options.UseForeColor = true;
            gvExitLanes.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(59, 97, 156);
            gvExitLanes.Appearance.FixedLine.Options.UseBackColor = true;
            gvExitLanes.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.FocusedCell.Options.UseBackColor = true;
            gvExitLanes.Appearance.FocusedCell.Options.UseForeColor = true;
            gvExitLanes.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(49, 106, 197);
            gvExitLanes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.FocusedRow.Options.UseBackColor = true;
            gvExitLanes.Appearance.FocusedRow.Options.UseForeColor = true;
            gvExitLanes.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            gvExitLanes.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gvExitLanes.Appearance.FooterPanel.Options.UseBackColor = true;
            gvExitLanes.Appearance.FooterPanel.Options.UseBorderColor = true;
            gvExitLanes.Appearance.FooterPanel.Options.UseForeColor = true;
            gvExitLanes.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gvExitLanes.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gvExitLanes.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.GroupButton.Options.UseBackColor = true;
            gvExitLanes.Appearance.GroupButton.Options.UseBorderColor = true;
            gvExitLanes.Appearance.GroupButton.Options.UseForeColor = true;
            gvExitLanes.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gvExitLanes.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gvExitLanes.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.GroupFooter.Options.UseBackColor = true;
            gvExitLanes.Appearance.GroupFooter.Options.UseBorderColor = true;
            gvExitLanes.Appearance.GroupFooter.Options.UseForeColor = true;
            gvExitLanes.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(62, 109, 185);
            gvExitLanes.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.GroupPanel.Options.UseBackColor = true;
            gvExitLanes.Appearance.GroupPanel.Options.UseForeColor = true;
            gvExitLanes.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gvExitLanes.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gvExitLanes.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            gvExitLanes.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.GroupRow.Options.UseBackColor = true;
            gvExitLanes.Appearance.GroupRow.Options.UseBorderColor = true;
            gvExitLanes.Appearance.GroupRow.Options.UseFont = true;
            gvExitLanes.Appearance.GroupRow.Options.UseForeColor = true;
            gvExitLanes.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            gvExitLanes.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gvExitLanes.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F);
            gvExitLanes.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gvExitLanes.Appearance.HeaderPanel.Options.UseBackColor = true;
            gvExitLanes.Appearance.HeaderPanel.Options.UseBorderColor = true;
            gvExitLanes.Appearance.HeaderPanel.Options.UseFont = true;
            gvExitLanes.Appearance.HeaderPanel.Options.UseForeColor = true;
            gvExitLanes.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(106, 153, 228);
            gvExitLanes.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(208, 224, 251);
            gvExitLanes.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gvExitLanes.Appearance.HideSelectionRow.Options.UseForeColor = true;
            gvExitLanes.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(99, 127, 196);
            gvExitLanes.Appearance.HorzLine.Options.UseBackColor = true;
            gvExitLanes.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 12F);
            gvExitLanes.Appearance.OddRow.ForeColor = System.Drawing.Color.Navy;
            gvExitLanes.Appearance.OddRow.Options.UseBackColor = true;
            gvExitLanes.Appearance.OddRow.Options.UseFont = true;
            gvExitLanes.Appearance.OddRow.Options.UseForeColor = true;
            gvExitLanes.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(249, 252, 255);
            gvExitLanes.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(88, 129, 185);
            gvExitLanes.Appearance.Preview.Options.UseBackColor = true;
            gvExitLanes.Appearance.Preview.Options.UseForeColor = true;
            gvExitLanes.Appearance.Row.BackColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            gvExitLanes.Appearance.Row.Options.UseBackColor = true;
            gvExitLanes.Appearance.Row.Options.UseForeColor = true;
            gvExitLanes.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.RowSeparator.Options.UseBackColor = true;
            gvExitLanes.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(69, 126, 217);
            gvExitLanes.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            gvExitLanes.Appearance.SelectedRow.Options.UseBackColor = true;
            gvExitLanes.Appearance.SelectedRow.Options.UseForeColor = true;
            gvExitLanes.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(99, 127, 196);
            gvExitLanes.Appearance.VertLine.Options.UseBackColor = true;
            gvExitLanes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colPlazaId, colLaneNo, colUserName, coluserNumber, colWorkState, colCarKind1, colCharge1, colCarType, colWorkMode, gcCardBoxExit, colStartWorkTime, colyupengdeng, colCoil1, colCoil2, colRSU, colYellow, gcWeightStatus, gcReaderStatus, colQRPay, Coil3, colJiaoTong, colPrint, Coil4, colLanGan, colBaoJing, colVPR, colCamera, colNetWork });
            gvExitLanes.DetailHeight = 375;
            gvExitLanes.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gvExitLanes.GridControl = gcExitLanes;
            gvExitLanes.GroupCount = 1;
            gvExitLanes.Name = "gvExitLanes";
            gvExitLanes.OptionsBehavior.AutoExpandAllGroups = true;
            gvExitLanes.OptionsBehavior.KeepGroupExpandedOnSorting = false;
            gvExitLanes.OptionsMenu.EnableColumnMenu = false;
            gvExitLanes.OptionsMenu.EnableFooterMenu = false;
            gvExitLanes.OptionsMenu.EnableGroupPanelMenu = false;
            gvExitLanes.OptionsSelection.EnableAppearanceFocusedCell = false;
            gvExitLanes.OptionsSelection.EnableAppearanceFocusedRow = false;
            gvExitLanes.OptionsView.EnableAppearanceEvenRow = true;
            gvExitLanes.OptionsView.EnableAppearanceOddRow = true;
            gvExitLanes.OptionsView.ShowFooter = true;
            gvExitLanes.OptionsView.ShowGroupPanel = false;
            gvExitLanes.RowHeight = 43;
            gvExitLanes.RowSeparatorHeight = 3;
            gvExitLanes.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] { new DevExpress.XtraGrid.Columns.GridColumnSortInfo(colPlazaId, DevExpress.Data.ColumnSortOrder.Ascending) });
            gvExitLanes.RowStyle += gv_RowStyle;
            // 
            // colPlazaId
            // 
            colPlazaId.Caption = "收费站";
            colPlazaId.ColumnEdit = luePlaza;
            colPlazaId.FieldName = "PlazaId";
            colPlazaId.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value;
            colPlazaId.Name = "colPlazaId";
            colPlazaId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            colPlazaId.Visible = true;
            colPlazaId.VisibleIndex = 27;
            // 
            // luePlaza
            // 
            luePlaza.AutoHeight = false;
            luePlaza.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            luePlaza.DataSource = tPlazaBindingSource;
            luePlaza.DisplayMember = "StationName";
            luePlaza.Name = "luePlaza";
            luePlaza.ValueMember = "Id";
            // 
            // tPlazaBindingSource
            // 
            tPlazaBindingSource.DataSource = typeof(Dtos.T_Plaza);
            // 
            // colLaneNo
            // 
            colLaneNo.Caption = "车道";
            colLaneNo.FieldName = "LaneName";
            colLaneNo.Name = "colLaneNo";
            colLaneNo.OptionsColumn.AllowEdit = false;
            colLaneNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colLaneNo.OptionsColumn.ReadOnly = true;
            colLaneNo.OptionsFilter.AllowFilter = false;
            colLaneNo.Visible = true;
            colLaneNo.VisibleIndex = 0;
            colLaneNo.Width = 56;
            // 
            // colUserName
            // 
            colUserName.Caption = "姓名";
            colUserName.FieldName = "CollName";
            colUserName.Name = "colUserName";
            colUserName.OptionsColumn.AllowEdit = false;
            colUserName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colUserName.OptionsColumn.ReadOnly = true;
            colUserName.OptionsFilter.AllowFilter = false;
            colUserName.Visible = true;
            colUserName.VisibleIndex = 1;
            colUserName.Width = 139;
            // 
            // coluserNumber
            // 
            coluserNumber.Caption = "工号";
            coluserNumber.FieldName = "CollNo";
            coluserNumber.Name = "coluserNumber";
            coluserNumber.OptionsColumn.AllowEdit = false;
            coluserNumber.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            coluserNumber.OptionsColumn.ReadOnly = true;
            coluserNumber.OptionsFilter.AllowFilter = false;
            coluserNumber.Visible = true;
            coluserNumber.VisibleIndex = 2;
            coluserNumber.Width = 101;
            // 
            // colWorkState
            // 
            colWorkState.Caption = "工作状态";
            colWorkState.FieldName = "ClientMsg";
            colWorkState.Name = "colWorkState";
            colWorkState.OptionsColumn.AllowEdit = false;
            colWorkState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colWorkState.OptionsColumn.ReadOnly = true;
            colWorkState.OptionsFilter.AllowFilter = false;
            colWorkState.Visible = true;
            colWorkState.VisibleIndex = 3;
            colWorkState.Width = 169;
            // 
            // colCarKind1
            // 
            colCarKind1.Caption = "车型";
            colCarKind1.FieldName = "CarType";
            colCarKind1.Name = "colCarKind1";
            colCarKind1.OptionsColumn.AllowEdit = false;
            colCarKind1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colCarKind1.OptionsColumn.ReadOnly = true;
            colCarKind1.OptionsFilter.AllowFilter = false;
            colCarKind1.Visible = true;
            colCarKind1.VisibleIndex = 4;
            colCarKind1.Width = 59;
            // 
            // colCharge1
            // 
            colCharge1.Caption = "金额";
            colCharge1.FieldName = "Money";
            colCharge1.Name = "colCharge1";
            colCharge1.OptionsColumn.AllowEdit = false;
            colCharge1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colCharge1.OptionsColumn.ReadOnly = true;
            colCharge1.OptionsFilter.AllowFilter = false;
            colCharge1.Visible = true;
            colCharge1.VisibleIndex = 5;
            colCharge1.Width = 63;
            // 
            // colCarType
            // 
            colCarType.Caption = "车种";
            colCarType.FieldName = "CarKind";
            colCarType.Name = "colCarType";
            colCarType.OptionsColumn.AllowEdit = false;
            colCarType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colCarType.OptionsColumn.ReadOnly = true;
            colCarType.OptionsFilter.AllowFilter = false;
            colCarType.Visible = true;
            colCarType.VisibleIndex = 6;
            colCarType.Width = 85;
            // 
            // colWorkMode
            // 
            colWorkMode.Caption = "工作模式";
            colWorkMode.FieldName = "WrokMode";
            colWorkMode.Name = "colWorkMode";
            colWorkMode.OptionsColumn.AllowEdit = false;
            colWorkMode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colWorkMode.OptionsColumn.ReadOnly = true;
            colWorkMode.OptionsFilter.AllowFilter = false;
            colWorkMode.Visible = true;
            colWorkMode.VisibleIndex = 7;
            colWorkMode.Width = 105;
            // 
            // gcCardBoxExit
            // 
            gcCardBoxExit.Caption = "卡箱";
            gcCardBoxExit.ColumnEdit = cardBoxControlRepositoryItem2;
            gcCardBoxExit.FieldName = "CardBox";
            gcCardBoxExit.MaxWidth = 80;
            gcCardBoxExit.MinWidth = 40;
            gcCardBoxExit.Name = "gcCardBoxExit";
            gcCardBoxExit.OptionsColumn.AllowEdit = false;
            gcCardBoxExit.Visible = true;
            gcCardBoxExit.VisibleIndex = 9;
            gcCardBoxExit.Width = 80;
            // 
            // cardBoxControlRepositoryItem2
            // 
            cardBoxControlRepositoryItem2.AutoHeight = false;
            cardBoxControlRepositoryItem2.Name = "cardBoxControlRepositoryItem2";
            cardBoxControlRepositoryItem2.ReadOnly = true;
            // 
            // colStartWorkTime
            // 
            colStartWorkTime.Caption = "上班时间";
            colStartWorkTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            colStartWorkTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            colStartWorkTime.FieldName = "JobBeginTime";
            colStartWorkTime.Name = "colStartWorkTime";
            colStartWorkTime.OptionsColumn.AllowEdit = false;
            colStartWorkTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colStartWorkTime.OptionsColumn.ReadOnly = true;
            colStartWorkTime.OptionsFilter.AllowFilter = false;
            colStartWorkTime.Visible = true;
            colStartWorkTime.VisibleIndex = 8;
            colStartWorkTime.Width = 199;
            // 
            // colyupengdeng
            // 
            colyupengdeng.Caption = "雨棚灯";
            colyupengdeng.ColumnEdit = repositoryItemPictureEdit1;
            colyupengdeng.FieldName = "YuPengDeng";
            colyupengdeng.MaxWidth = 55;
            colyupengdeng.MinWidth = 38;
            colyupengdeng.Name = "colyupengdeng";
            colyupengdeng.OptionsColumn.AllowEdit = false;
            colyupengdeng.OptionsColumn.AllowSize = false;
            colyupengdeng.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colyupengdeng.OptionsColumn.ReadOnly = true;
            colyupengdeng.OptionsFilter.AllowFilter = false;
            colyupengdeng.Visible = true;
            colyupengdeng.VisibleIndex = 10;
            colyupengdeng.Width = 38;
            // 
            // repositoryItemPictureEdit1
            // 
            repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            // 
            // colCoil1
            // 
            colCoil1.Caption = "触发";
            colCoil1.FieldName = "Coil1";
            colCoil1.MaxWidth = 55;
            colCoil1.MinWidth = 38;
            colCoil1.Name = "colCoil1";
            colCoil1.OptionsColumn.AllowEdit = false;
            colCoil1.OptionsColumn.ReadOnly = true;
            colCoil1.Visible = true;
            colCoil1.VisibleIndex = 20;
            colCoil1.Width = 38;
            // 
            // colCoil2
            // 
            colCoil2.Caption = "交易";
            colCoil2.FieldName = "Coil2";
            colCoil2.MaxWidth = 55;
            colCoil2.MinWidth = 38;
            colCoil2.Name = "colCoil2";
            colCoil2.OptionsColumn.AllowEdit = false;
            colCoil2.OptionsColumn.ReadOnly = true;
            colCoil2.Visible = true;
            colCoil2.VisibleIndex = 21;
            colCoil2.Width = 38;
            // 
            // colRSU
            // 
            colRSU.FieldName = "RSU";
            colRSU.MaxWidth = 55;
            colRSU.MinWidth = 38;
            colRSU.Name = "colRSU";
            colRSU.OptionsColumn.AllowEdit = false;
            colRSU.OptionsColumn.ReadOnly = true;
            colRSU.Visible = true;
            colRSU.VisibleIndex = 19;
            colRSU.Width = 38;
            // 
            // colYellow
            // 
            colYellow.Caption = "小黄人";
            colYellow.FieldName = "Yellow";
            colYellow.MaxWidth = 55;
            colYellow.MinWidth = 38;
            colYellow.Name = "colYellow";
            colYellow.OptionsColumn.AllowEdit = false;
            colYellow.OptionsColumn.ReadOnly = true;
            colYellow.Visible = true;
            colYellow.VisibleIndex = 26;
            colYellow.Width = 44;
            // 
            // gcWeightStatus
            // 
            gcWeightStatus.Caption = "称台";
            gcWeightStatus.FieldName = "Weight";
            gcWeightStatus.MaxWidth = 55;
            gcWeightStatus.MinWidth = 38;
            gcWeightStatus.Name = "gcWeightStatus";
            gcWeightStatus.OptionsColumn.AllowEdit = false;
            gcWeightStatus.OptionsColumn.ReadOnly = true;
            gcWeightStatus.Visible = true;
            gcWeightStatus.VisibleIndex = 11;
            gcWeightStatus.Width = 38;
            // 
            // gcReaderStatus
            // 
            gcReaderStatus.Caption = "读卡器";
            gcReaderStatus.ColumnEdit = repositoryItemPictureEdit1;
            gcReaderStatus.FieldName = "Reader";
            gcReaderStatus.MaxWidth = 55;
            gcReaderStatus.MinWidth = 38;
            gcReaderStatus.Name = "gcReaderStatus";
            gcReaderStatus.OptionsColumn.AllowEdit = false;
            gcReaderStatus.OptionsColumn.ReadOnly = true;
            gcReaderStatus.Visible = true;
            gcReaderStatus.VisibleIndex = 13;
            gcReaderStatus.Width = 38;
            // 
            // colQRPay
            // 
            colQRPay.Caption = "移动支付";
            colQRPay.FieldName = "QRPay";
            colQRPay.MaxWidth = 55;
            colQRPay.MinWidth = 38;
            colQRPay.Name = "colQRPay";
            colQRPay.OptionsColumn.AllowEdit = false;
            colQRPay.OptionsColumn.ReadOnly = true;
            colQRPay.Visible = true;
            colQRPay.VisibleIndex = 24;
            colQRPay.Width = 48;
            // 
            // Coil3
            // 
            Coil3.Caption = "抓拍";
            Coil3.ColumnEdit = repositoryItemPictureEdit1;
            Coil3.FieldName = "Coil3";
            Coil3.MaxWidth = 55;
            Coil3.MinWidth = 38;
            Coil3.Name = "Coil3";
            Coil3.OptionsColumn.AllowEdit = false;
            Coil3.OptionsColumn.AllowSize = false;
            Coil3.OptionsColumn.ReadOnly = true;
            Coil3.OptionsFilter.AllowFilter = false;
            Coil3.Visible = true;
            Coil3.VisibleIndex = 12;
            Coil3.Width = 38;
            // 
            // colJiaoTong
            // 
            colJiaoTong.Caption = "交通灯";
            colJiaoTong.ColumnEdit = repositoryItemPictureEdit1;
            colJiaoTong.FieldName = "JiaoTongDeng";
            colJiaoTong.MaxWidth = 55;
            colJiaoTong.MinWidth = 38;
            colJiaoTong.Name = "colJiaoTong";
            colJiaoTong.OptionsColumn.AllowEdit = false;
            colJiaoTong.OptionsColumn.AllowSize = false;
            colJiaoTong.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colJiaoTong.OptionsColumn.ReadOnly = true;
            colJiaoTong.OptionsFilter.AllowFilter = false;
            colJiaoTong.Visible = true;
            colJiaoTong.VisibleIndex = 15;
            colJiaoTong.Width = 38;
            // 
            // colPrint
            // 
            colPrint.Caption = "打印机";
            colPrint.ColumnEdit = repositoryItemPictureEdit1;
            colPrint.FieldName = "Printer";
            colPrint.MaxWidth = 55;
            colPrint.MinWidth = 38;
            colPrint.Name = "colPrint";
            colPrint.OptionsColumn.AllowEdit = false;
            colPrint.OptionsColumn.AllowSize = false;
            colPrint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colPrint.OptionsColumn.ReadOnly = true;
            colPrint.OptionsFilter.AllowFilter = false;
            colPrint.Visible = true;
            colPrint.VisibleIndex = 14;
            colPrint.Width = 38;
            // 
            // Coil4
            // 
            Coil4.Caption = "离开";
            Coil4.ColumnEdit = repositoryItemPictureEdit1;
            Coil4.FieldName = "Coil4";
            Coil4.MaxWidth = 55;
            Coil4.MinWidth = 38;
            Coil4.Name = "Coil4";
            Coil4.OptionsColumn.AllowEdit = false;
            Coil4.OptionsColumn.AllowSize = false;
            Coil4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            Coil4.OptionsColumn.ReadOnly = true;
            Coil4.OptionsFilter.AllowFilter = false;
            Coil4.Visible = true;
            Coil4.VisibleIndex = 17;
            Coil4.Width = 38;
            // 
            // colLanGan
            // 
            colLanGan.Caption = "栏杆";
            colLanGan.ColumnEdit = repositoryItemPictureEdit1;
            colLanGan.FieldName = "LanGan";
            colLanGan.MaxWidth = 55;
            colLanGan.MinWidth = 38;
            colLanGan.Name = "colLanGan";
            colLanGan.OptionsColumn.AllowEdit = false;
            colLanGan.OptionsColumn.AllowSize = false;
            colLanGan.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colLanGan.OptionsColumn.ReadOnly = true;
            colLanGan.OptionsFilter.AllowFilter = false;
            colLanGan.Visible = true;
            colLanGan.VisibleIndex = 16;
            colLanGan.Width = 38;
            // 
            // colBaoJing
            // 
            colBaoJing.Caption = "黄闪";
            colBaoJing.FieldName = "BaoJing";
            colBaoJing.MaxWidth = 55;
            colBaoJing.MinWidth = 38;
            colBaoJing.Name = "colBaoJing";
            colBaoJing.OptionsColumn.AllowEdit = false;
            colBaoJing.OptionsColumn.ReadOnly = true;
            colBaoJing.Visible = true;
            colBaoJing.VisibleIndex = 22;
            colBaoJing.Width = 38;
            // 
            // colVPR
            // 
            colVPR.Caption = "车牌识别";
            colVPR.FieldName = "VPR";
            colVPR.MaxWidth = 55;
            colVPR.MinWidth = 38;
            colVPR.Name = "colVPR";
            colVPR.OptionsColumn.AllowEdit = false;
            colVPR.OptionsColumn.ReadOnly = true;
            colVPR.Visible = true;
            colVPR.VisibleIndex = 25;
            colVPR.Width = 52;
            // 
            // colCamera
            // 
            colCamera.Caption = "车道摄像机";
            colCamera.FieldName = "Camera";
            colCamera.MaxWidth = 55;
            colCamera.MinWidth = 38;
            colCamera.Name = "colCamera";
            colCamera.OptionsColumn.AllowEdit = false;
            colCamera.OptionsColumn.ReadOnly = true;
            colCamera.Visible = true;
            colCamera.VisibleIndex = 23;
            colCamera.Width = 38;
            // 
            // colNetWork
            // 
            colNetWork.Caption = "网络";
            colNetWork.ColumnEdit = repositoryItemPictureEdit1;
            colNetWork.FieldName = "Network";
            colNetWork.MaxWidth = 55;
            colNetWork.MinWidth = 38;
            colNetWork.Name = "colNetWork";
            colNetWork.OptionsColumn.AllowEdit = false;
            colNetWork.OptionsColumn.AllowSize = false;
            colNetWork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            colNetWork.OptionsColumn.ReadOnly = true;
            colNetWork.OptionsFilter.AllowFilter = false;
            colNetWork.Visible = true;
            colNetWork.VisibleIndex = 18;
            colNetWork.Width = 38;
            // 
            // barManager1
            // 
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { btnVNC, btnPing, btnRemotLane, btnLaneReboot, btnLaneControl });
            barManager1.MaxItemId = 8;
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            barDockControlTop.Location = new System.Drawing.Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Margin = new System.Windows.Forms.Padding(4);
            barDockControlTop.Size = new System.Drawing.Size(1513, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            barDockControlBottom.Location = new System.Drawing.Point(0, 570);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Margin = new System.Windows.Forms.Padding(4);
            barDockControlBottom.Size = new System.Drawing.Size(1513, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Margin = new System.Windows.Forms.Padding(4);
            barDockControlLeft.Size = new System.Drawing.Size(0, 570);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            barDockControlRight.Location = new System.Drawing.Point(1513, 0);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Margin = new System.Windows.Forms.Padding(4);
            barDockControlRight.Size = new System.Drawing.Size(0, 570);
            // 
            // btnVNC
            // 
            btnVNC.Caption = "远程桌面";
            btnVNC.Id = 0;
            btnVNC.Name = "btnVNC";
            btnVNC.ItemClick += btnVNC_ItemClick;
            // 
            // btnPing
            // 
            btnPing.Caption = "测试网络";
            btnPing.Id = 1;
            btnPing.Name = "btnPing";
            btnPing.ItemClick += btnPing_ItemClick;
            // 
            // btnRemotLane
            // 
            btnRemotLane.Caption = "远程控制";
            btnRemotLane.Id = 3;
            btnRemotLane.Name = "btnRemotLane";
            btnRemotLane.ItemClick += btnRemotLane_ItemClick;
            // 
            // btnLaneReboot
            // 
            btnLaneReboot.Caption = "重启";
            btnLaneReboot.Id = 6;
            btnLaneReboot.Name = "btnLaneReboot";
            btnLaneReboot.ItemClick += btnLaneReboot_ItemClick;
            // 
            // btnLaneControl
            // 
            btnLaneControl.Caption = "工控机";
            btnLaneControl.Id = 5;
            btnLaneControl.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(btnLaneReboot) });
            btnLaneControl.Name = "btnLaneControl";
            // 
            // rpbExit
            // 
            rpbExit.Name = "rpbExit";
            // 
            // repositoryItemTimeEdit1
            // 
            repositoryItemTimeEdit1.AutoHeight = false;
            repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            // 
            // cbxPlaza
            // 
            cbxPlaza.AutoHeight = false;
            cbxPlaza.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cbxPlaza.Name = "cbxPlaza";
            // 
            // radialMenu1
            // 
            radialMenu1.Glyph = Properties.Resources.promotion;
            radialMenu1.ItemAutoSize = DevExpress.XtraBars.Ribbon.RadialMenuItemAutoSize.Spring;
            radialMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(btnVNC), new DevExpress.XtraBars.LinkPersistInfo(btnPing), new DevExpress.XtraBars.LinkPersistInfo(btnRemotLane), new DevExpress.XtraBars.LinkPersistInfo(btnLaneControl) });
            radialMenu1.Manager = barManager1;
            radialMenu1.Name = "radialMenu1";
            // 
            // LaneView
            // 
            Appearance.BackColor = System.Drawing.SystemColors.Control;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gcExitLanes);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "LaneView";
            Size = new System.Drawing.Size(1513, 570);
            ((System.ComponentModel.ISupportInitialize)gcExitLanes).EndInit();
            ((System.ComponentModel.ISupportInitialize)laneInfoBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)gvExitLanes).EndInit();
            ((System.ComponentModel.ISupportInitialize)luePlaza).EndInit();
            ((System.ComponentModel.ISupportInitialize)tPlazaBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)cardBoxControlRepositoryItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemPictureEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)rpbExit).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemTimeEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)cbxPlaza).EndInit();
            ((System.ComponentModel.ISupportInitialize)radialMenu1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnRemotLane;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraBars.BarButtonItem btnLaneReboot;
        private DevExpress.XtraBars.BarSubItem btnLaneControl;
        private DevExpress.XtraGrid.Columns.GridColumn colPlazaId;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cbxPlaza;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit luePlaza;
        private System.Windows.Forms.BindingSource tPlazaBindingSource;
    }
}
