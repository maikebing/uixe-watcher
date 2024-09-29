using System;
using System.Collections.Generic;
using Uixe.Watcher.Msg;

namespace Uixe.Watcher.Controls
{
    /// <summary>
    /// Summary description for MessageView.
    /// </summary>
    public class MessageView : DevExpress.XtraEditors.XtraUserControl
    {
        private DevExpress.XtraGrid.GridControl Grd;
        private DevExpress.XtraGrid.Views.Grid.GridView GrdV;
        private DevExpress.XtraGrid.Columns.GridColumn colmessageNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn colcarKind;
        private DevExpress.XtraGrid.Columns.GridColumn colcarTeam;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn colbillNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn cardnumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private System.Windows.Forms.BindingSource msgInfoBindingSource;
        private System.ComponentModel.IContainer components;

        public MessageView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private int _MsgViewRowsCount;

        private List<MsgInfo> infos = new List<MsgInfo>();

        /// <summary>
        /// 这个方法目前初始化对应的站
        /// </summary>
        /// <param Name="plazano"></param>
        internal void initMessageView(string plazano, int MsgViewRowsCount)
        {
            _MsgViewRowsCount = MsgViewRowsCount;
            msgInfoBindingSource.DataSource = infos;
        }

        /// <summary>
        ///  这个方法用来设置当前这个试图中显示那个站的消息数据 。
        /// </summary>
        /// <param Name="plaza"></param>
        public void SetPlaza(string plaza)
        {
        }

        private delegate void HShowMessageView(MsgInfo msg);

        public void ShowMessageView(MsgInfo msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new HShowMessageView(ShowMessageView), msg);
            }
            else
            {
                lock (Grd)
                {
                    infos.Insert(0, msg);
                    if (infos.Count > _MsgViewRowsCount)
                    {
                        infos.Remove(infos[infos.Count - 1]);
                    }
                    Grd.RefreshDataSource();
                    try
                    {
                        GrdV.FocusedRowHandle = 0;
                        GrdV.TopRowIndex = 0;
                        GrdV.TopRowIndex = 0;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(string.Format("错误:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
                    }
                }
            }
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Grd = new DevExpress.XtraGrid.GridControl();
            this.msgInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GrdV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colmessageNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcarKind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcarTeam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbillNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cardnumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdV)).BeginInit();
            this.SuspendLayout();
            //
            // Grd
            //
            this.Grd.DataSource = this.msgInfoBindingSource;
            this.Grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grd.Location = new System.Drawing.Point(0, 0);
            this.Grd.MainView = this.GrdV;
            this.Grd.Name = "Grd";
            this.Grd.Size = new System.Drawing.Size(1627, 352);
            this.Grd.TabIndex = 10;
            this.Grd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GrdV});
            //
            // msgInfoBindingSource
            //
            this.msgInfoBindingSource.DataSource = typeof(Uixe.Watcher.Msg.MsgInfo);
            //
            // GrdV
            //
            this.GrdV.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.GrdV.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.GrdV.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.GrdV.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.GrdV.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.GrdV.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.GrdV.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.GrdV.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.GrdV.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.GrdV.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.GrdV.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.GrdV.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.GrdV.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.GrdV.Appearance.Empty.Options.UseBackColor = true;
            this.GrdV.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.GrdV.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.EvenRow.Options.UseBackColor = true;
            this.GrdV.Appearance.EvenRow.Options.UseFont = true;
            this.GrdV.Appearance.EvenRow.Options.UseForeColor = true;
            this.GrdV.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.GrdV.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.GrdV.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.GrdV.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.GrdV.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.GrdV.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.GrdV.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.GrdV.Appearance.FilterPanel.Options.UseBackColor = true;
            this.GrdV.Appearance.FilterPanel.Options.UseForeColor = true;
            this.GrdV.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.GrdV.Appearance.FixedLine.Options.UseBackColor = true;
            this.GrdV.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.GrdV.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.FocusedCell.Options.UseBackColor = true;
            this.GrdV.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GrdV.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.GrdV.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.GrdV.Appearance.FocusedRow.Options.UseBackColor = true;
            this.GrdV.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GrdV.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.GrdV.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.GrdV.Appearance.FooterPanel.Options.UseBackColor = true;
            this.GrdV.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.GrdV.Appearance.FooterPanel.Options.UseForeColor = true;
            this.GrdV.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.GrdV.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.GrdV.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.GroupButton.Options.UseBackColor = true;
            this.GrdV.Appearance.GroupButton.Options.UseBorderColor = true;
            this.GrdV.Appearance.GroupButton.Options.UseForeColor = true;
            this.GrdV.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.GrdV.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.GrdV.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.GroupFooter.Options.UseBackColor = true;
            this.GrdV.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.GrdV.Appearance.GroupFooter.Options.UseForeColor = true;
            this.GrdV.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.GrdV.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.GroupPanel.Options.UseBackColor = true;
            this.GrdV.Appearance.GroupPanel.Options.UseForeColor = true;
            this.GrdV.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.GrdV.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.GrdV.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.GrdV.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.GroupRow.Options.UseBackColor = true;
            this.GrdV.Appearance.GroupRow.Options.UseBorderColor = true;
            this.GrdV.Appearance.GroupRow.Options.UseFont = true;
            this.GrdV.Appearance.GroupRow.Options.UseForeColor = true;
            this.GrdV.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.GrdV.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.GrdV.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.GrdV.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.GrdV.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.GrdV.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.GrdV.Appearance.HeaderPanel.Options.UseFont = true;
            this.GrdV.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GrdV.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.GrdV.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.GrdV.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.GrdV.Appearance.HorzLine.Options.UseBackColor = true;
            this.GrdV.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.GrdV.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 12F);
            this.GrdV.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.OddRow.Options.UseBackColor = true;
            this.GrdV.Appearance.OddRow.Options.UseFont = true;
            this.GrdV.Appearance.OddRow.Options.UseForeColor = true;
            this.GrdV.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.GrdV.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.GrdV.Appearance.Preview.Options.UseBackColor = true;
            this.GrdV.Appearance.Preview.Options.UseForeColor = true;
            this.GrdV.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.GrdV.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F);
            this.GrdV.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.GrdV.Appearance.Row.Options.UseBackColor = true;
            this.GrdV.Appearance.Row.Options.UseFont = true;
            this.GrdV.Appearance.Row.Options.UseForeColor = true;
            this.GrdV.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.GrdV.Appearance.RowSeparator.Options.UseBackColor = true;
            this.GrdV.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.GrdV.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.GrdV.Appearance.SelectedRow.Options.UseBackColor = true;
            this.GrdV.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GrdV.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.GrdV.Appearance.VertLine.Options.UseBackColor = true;
            this.GrdV.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.GrdV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colmessageNumber,
            this.gridColumn1,
            this.gridColumn2,
            this.colcarKind,
            this.colcarTeam,
            this.gridColumn5,
            this.colbillNumber,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn7,
            this.cardnumber,
            this.gridColumn9});
            this.GrdV.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.GrdV.GridControl = this.Grd;
            this.GrdV.HorzScrollStep = 1;
            this.GrdV.Name = "GrdV";
            this.GrdV.OptionsCustomization.AllowColumnMoving = false;
            this.GrdV.OptionsCustomization.AllowFilter = false;
            this.GrdV.OptionsCustomization.AllowGroup = false;
            this.GrdV.OptionsCustomization.AllowSort = false;
            this.GrdV.OptionsDetail.EnableMasterViewMode = false;
            this.GrdV.OptionsDetail.ShowDetailTabs = false;
            this.GrdV.OptionsDetail.SmartDetailExpand = false;
            this.GrdV.OptionsDetail.SmartDetailExpandButtonMode = DevExpress.XtraGrid.Views.Grid.DetailExpandButtonMode.CheckDefaultDetail;
            this.GrdV.OptionsLayout.StoreDataSettings = false;
            this.GrdV.OptionsLayout.StoreVisualOptions = false;
            this.GrdV.OptionsNavigation.AutoMoveRowFocus = false;
            this.GrdV.OptionsNavigation.UseTabKey = false;
            this.GrdV.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GrdV.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.GrdV.OptionsSelection.EnableAppearanceHideSelection = false;
            this.GrdV.OptionsSelection.UseIndicatorForSelection = false;
            this.GrdV.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            this.GrdV.OptionsView.ShowDetailButtons = false;
            this.GrdV.OptionsView.ShowGroupPanel = false;
            this.GrdV.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            this.GrdV.SynchronizeClones = false;
            //
            // colmessageNumber
            //
            this.colmessageNumber.Caption = "车道";
            this.colmessageNumber.FieldName = "LaneNo";
            this.colmessageNumber.Name = "colmessageNumber";
            this.colmessageNumber.OptionsColumn.AllowMove = false;
            this.colmessageNumber.OptionsColumn.AllowSize = false;
            this.colmessageNumber.OptionsColumn.ReadOnly = true;
            this.colmessageNumber.OptionsFilter.AllowAutoFilter = false;
            this.colmessageNumber.OptionsFilter.AllowFilter = false;
            this.colmessageNumber.Visible = true;
            this.colmessageNumber.VisibleIndex = 0;
            this.colmessageNumber.Width = 30;
            //
            // gridColumn1
            //
            this.gridColumn1.Caption = "通过日期";
            this.gridColumn1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "OccDateTime";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 182;
            //
            // gridColumn2
            //
            this.gridColumn2.Caption = "工号";
            this.gridColumn2.FieldName = "CollNo";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 45;
            //
            // colcarKind
            //
            this.colcarKind.Caption = "车种";
            this.colcarKind.FieldName = "CarKind";
            this.colcarKind.Name = "colcarKind";
            this.colcarKind.OptionsColumn.AllowEdit = false;
            this.colcarKind.OptionsFilter.AllowFilter = false;
            this.colcarKind.Visible = true;
            this.colcarKind.VisibleIndex = 4;
            this.colcarKind.Width = 39;
            //
            // colcarTeam
            //
            this.colcarTeam.Caption = "车型";
            this.colcarTeam.FieldName = "CarType";
            this.colcarTeam.Name = "colcarTeam";
            this.colcarTeam.OptionsColumn.AllowEdit = false;
            this.colcarTeam.OptionsFilter.AllowFilter = false;
            this.colcarTeam.Visible = true;
            this.colcarTeam.VisibleIndex = 5;
            this.colcarTeam.Width = 30;
            //
            // gridColumn5
            //
            this.gridColumn5.Caption = "消息类型";
            this.gridColumn5.FieldName = "MsgType";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            this.gridColumn5.Width = 53;
            //
            // colbillNumber
            //
            this.colbillNumber.Caption = "支付方式";
            this.colbillNumber.FieldName = "PayType";
            this.colbillNumber.Name = "colbillNumber";
            this.colbillNumber.OptionsColumn.AllowEdit = false;
            this.colbillNumber.OptionsFilter.AllowFilter = false;
            this.colbillNumber.Visible = true;
            this.colbillNumber.VisibleIndex = 6;
            this.colbillNumber.Width = 72;
            //
            // gridColumn3
            //
            this.gridColumn3.Caption = "金额";
            this.gridColumn3.FieldName = "Cash";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 44;
            //
            // gridColumn6
            //
            this.gridColumn6.Caption = "异常情况";
            this.gridColumn6.FieldName = "Exception";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 8;
            this.gridColumn6.Width = 73;
            //
            // gridColumn7
            //
            this.gridColumn7.Caption = "违章情况";
            this.gridColumn7.FieldName = "DevStatus";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 9;
            this.gridColumn7.Width = 100;
            //
            // cardnumber
            //
            this.cardnumber.Caption = "发票代码";
            this.cardnumber.FieldName = "Receipt";
            this.cardnumber.Name = "cardnumber";
            this.cardnumber.OptionsColumn.AllowEdit = false;
            this.cardnumber.Visible = true;
            this.cardnumber.VisibleIndex = 10;
            this.cardnumber.Width = 61;
            //
            // gridColumn9
            //
            this.gridColumn9.Caption = "提示信息";
            this.gridColumn9.FieldName = "PromptMsg";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 11;
            this.gridColumn9.Width = 100;
            //
            // MessageView
            //
            this.Controls.Add(this.Grd);
            this.Name = "MessageView";
            this.Size = new System.Drawing.Size(1627, 352);
            ((System.ComponentModel.ISupportInitialize)(this.Grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.msgInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrdV)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}