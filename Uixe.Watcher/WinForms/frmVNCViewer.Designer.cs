namespace Uixe.Watcher.WinForms
{
    partial class frmVNCViewer
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVNCViewer));
            dmVnc = new DevExpress.XtraBars.Docking2010.DocumentManager(components);
            widgetView1 = new DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView(components);
            ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            biFlowMode = new DevExpress.XtraBars.BarButtonItem();
            biStackedMode = new DevExpress.XtraBars.BarButtonItem();
            biTableMode = new DevExpress.XtraBars.BarButtonItem();
            biLeftToRight = new DevExpress.XtraBars.BarButtonItem();
            biRightToLeft = new DevExpress.XtraBars.BarButtonItem();
            biBottomUp = new DevExpress.XtraBars.BarButtonItem();
            biTopDown = new DevExpress.XtraBars.BarButtonItem();
            barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            biFreeMode = new DevExpress.XtraBars.BarButtonItem();
            biDragMode = new DevExpress.XtraBars.BarButtonItem();
            biItemMixer = new DevExpress.XtraBars.BarButtonItem();
            rpSettings = new DevExpress.XtraBars.Ribbon.RibbonPage();
            pgLayoutMode = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            pgFlowDirection = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)dmVnc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)widgetView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            SuspendLayout();
            // 
            // dmVnc
            // 
            dmVnc.ContainerControl = this;
            dmVnc.View = widgetView1;
            dmVnc.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] { widgetView1 });
            // 
            // widgetView1
            // 
            widgetView1.LayoutMode = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.TableLayout;
            widgetView1.RootContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            widgetView1.WindowsDialogProperties.NameColumnWidth = 5;
            widgetView1.WindowsDialogProperties.PathColumnWidth = 5;
            widgetView1.WindowsDialogProperties.Size = new System.Drawing.Size(467, 376);
            widgetView1.QueryControl += widgetView1_QueryControl;
            // 
            // ribbonControl1
            // 
            ribbonControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Blue;
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem, biFlowMode, biStackedMode, biTableMode, biLeftToRight, biRightToLeft, biBottomUp, biTopDown, barCheckItem1, biFreeMode, biDragMode, biItemMixer });
            ribbonControl1.Location = new System.Drawing.Point(0, 0);
            ribbonControl1.MaxItemId = 15;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { rpSettings });
            ribbonControl1.Size = new System.Drawing.Size(1358, 167);
            // 
            // biFlowMode
            // 
            biFlowMode.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biFlowMode.Caption = "Flow Layout";
            biFlowMode.GroupIndex = 1;
            biFlowMode.Id = 1;
            biFlowMode.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biFlowMode.ImageOptions.SvgImage");
            biFlowMode.Name = "biFlowMode";
            biFlowMode.Tag = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.FlowLayout;
            biFlowMode.ItemClick += OnLayoutModeCheckedChanged;
            // 
            // biStackedMode
            // 
            biStackedMode.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biStackedMode.Caption = "Stacked Layout";
            biStackedMode.Down = true;
            biStackedMode.GroupIndex = 1;
            biStackedMode.Id = 2;
            biStackedMode.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biStackedMode.ImageOptions.SvgImage");
            biStackedMode.Name = "biStackedMode";
            biStackedMode.Tag = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.StackLayout;
            biStackedMode.ItemClick += OnLayoutModeCheckedChanged;
            // 
            // biTableMode
            // 
            biTableMode.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biTableMode.Caption = "Table Layout";
            biTableMode.GroupIndex = 1;
            biTableMode.Id = 3;
            biTableMode.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biTableMode.ImageOptions.SvgImage");
            biTableMode.Name = "biTableMode";
            biTableMode.Tag = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.TableLayout;
            biTableMode.ItemClick += OnLayoutModeCheckedChanged;
            // 
            // biLeftToRight
            // 
            biLeftToRight.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biLeftToRight.Caption = "Left to Right";
            biLeftToRight.Down = true;
            biLeftToRight.GroupIndex = 2;
            biLeftToRight.Id = 4;
            biLeftToRight.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biLeftToRight.ImageOptions.SvgImage");
            biLeftToRight.Name = "biLeftToRight";
            biLeftToRight.Tag = System.Windows.Forms.FlowDirection.LeftToRight;
            biLeftToRight.ItemClick += OnFlowDirectionCheckedChanged;
            // 
            // biRightToLeft
            // 
            biRightToLeft.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biRightToLeft.Caption = "Right to Left";
            biRightToLeft.GroupIndex = 2;
            biRightToLeft.Id = 5;
            biRightToLeft.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biRightToLeft.ImageOptions.SvgImage");
            biRightToLeft.Name = "biRightToLeft";
            biRightToLeft.Tag = System.Windows.Forms.FlowDirection.RightToLeft;
            biRightToLeft.ItemClick += OnFlowDirectionCheckedChanged;
            // 
            // biBottomUp
            // 
            biBottomUp.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biBottomUp.Caption = "Bottom Up";
            biBottomUp.Down = true;
            biBottomUp.GroupIndex = 2;
            biBottomUp.Id = 6;
            biBottomUp.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biBottomUp.ImageOptions.SvgImage");
            biBottomUp.Name = "biBottomUp";
            biBottomUp.Tag = System.Windows.Forms.FlowDirection.BottomUp;
            biBottomUp.ItemClick += OnFlowDirectionCheckedChanged;
            // 
            // biTopDown
            // 
            biTopDown.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biTopDown.Caption = "Top Down";
            biTopDown.GroupIndex = 2;
            biTopDown.Id = 7;
            biTopDown.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biTopDown.ImageOptions.SvgImage");
            biTopDown.Name = "biTopDown";
            biTopDown.Tag = System.Windows.Forms.FlowDirection.TopDown;
            biTopDown.ItemClick += OnFlowDirectionCheckedChanged;
            // 
            // barCheckItem1
            // 
            barCheckItem1.BindableChecked = true;
            barCheckItem1.Caption = "Colored Widgets";
            barCheckItem1.Checked = true;
            barCheckItem1.Id = 10;
            barCheckItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barCheckItem1.ImageOptions.SvgImage");
            barCheckItem1.Name = "barCheckItem1";
            barCheckItem1.CheckedChanged += OnCheckedChanged;
            // 
            // biFreeMode
            // 
            biFreeMode.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biFreeMode.Caption = "Free Layout";
            biFreeMode.GroupIndex = 1;
            biFreeMode.Id = 11;
            biFreeMode.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("biFreeMode.ImageOptions.SvgImage");
            biFreeMode.Name = "biFreeMode";
            biFreeMode.Tag = DevExpress.XtraBars.Docking2010.Views.Widget.LayoutMode.FreeLayout;
            biFreeMode.ItemClick += OnLayoutModeCheckedChanged;
            // 
            // biDragMode
            // 
            biDragMode.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            biDragMode.Caption = "Outer Dragging";
            biDragMode.Id = 12;
            biDragMode.Name = "biDragMode";
            biDragMode.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
            biDragMode.ItemClick += ToggleFreeLayoutDragMode;
            // 
            // biItemMixer
            // 
            biItemMixer.Id = 14;
            biItemMixer.Name = "biItemMixer";
            // 
            // rpSettings
            // 
            rpSettings.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { pgLayoutMode, pgFlowDirection, ribbonPageGroup1 });
            rpSettings.Name = "rpSettings";
            rpSettings.Text = "Widget view settings";
            // 
            // pgLayoutMode
            // 
            pgLayoutMode.ItemLinks.Add(biFlowMode);
            pgLayoutMode.ItemLinks.Add(biStackedMode);
            pgLayoutMode.ItemLinks.Add(biTableMode);
            pgLayoutMode.ItemLinks.Add(biFreeMode);
            pgLayoutMode.Name = "pgLayoutMode";
            pgLayoutMode.Text = "Layout Mode";
            // 
            // pgFlowDirection
            // 
            pgFlowDirection.ItemLinks.Add(biLeftToRight);
            pgFlowDirection.ItemLinks.Add(biRightToLeft);
            pgFlowDirection.ItemLinks.Add(biBottomUp);
            pgFlowDirection.ItemLinks.Add(biTopDown);
            pgFlowDirection.Name = "pgFlowDirection";
            pgFlowDirection.Text = "Flow Direction";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.AllowTextClipping = false;
            ribbonPageGroup1.ItemLinks.Add(barCheckItem1);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "Appearance";
            // 
            // frmVNCViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1358, 692);
            Controls.Add(ribbonControl1);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "frmVNCViewer";
            Ribbon = ribbonControl1;
            Text = "frmVNCViewer";
            Load += frmVNCViewer_Load;
            ((System.ComponentModel.ISupportInitialize)dmVnc).EndInit();
            ((System.ComponentModel.ISupportInitialize)widgetView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraBars.Docking2010.DocumentManager dmVnc;
        private DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView widgetView1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem biFlowMode;
        private DevExpress.XtraBars.BarButtonItem biStackedMode;
        private DevExpress.XtraBars.BarButtonItem biTableMode;
        private DevExpress.XtraBars.BarButtonItem biLeftToRight;
        private DevExpress.XtraBars.BarButtonItem biRightToLeft;
        private DevExpress.XtraBars.BarButtonItem biBottomUp;
        private DevExpress.XtraBars.BarButtonItem biTopDown;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarButtonItem biFreeMode;
        private DevExpress.XtraBars.BarButtonItem biDragMode;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpSettings;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup pgLayoutMode;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup pgFlowDirection;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem biItemMixer;
    }
}