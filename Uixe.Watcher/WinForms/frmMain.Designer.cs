
namespace Uixe.Watcher
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.barMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.btnAbout = new DevExpress.XtraBars.BarButtonItem();
            this.btnLogin = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgPlazas = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabbedMdiManagerex1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManagerEX(this.components);
            this.rpgTime = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.tmDateTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManagerex1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Blue;
            this.ribbon.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(35);
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.skinRibbonGalleryBarItem1,
            this.barMdiChildrenListItem1,
            this.btnAbout,
            this.btnLogin});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(4);
            this.ribbon.MaxItemId = 7;
            this.ribbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbon.Name = "ribbon";
            this.ribbon.OptionsMenuMinWidth = 385;
            this.ribbon.PageHeaderItemLinks.Add(this.btnAbout);
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1432, 160);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // skinRibbonGalleryBarItem1
            // 
            this.skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
            this.skinRibbonGalleryBarItem1.Id = 3;
            this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
            // 
            // barMdiChildrenListItem1
            // 
            this.barMdiChildrenListItem1.Caption = "收费站";
            this.barMdiChildrenListItem1.Id = 4;
            this.barMdiChildrenListItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barMdiChildrenListItem1.ImageOptions.SvgImage")));
            this.barMdiChildrenListItem1.Name = "barMdiChildrenListItem1";
            // 
            // btnAbout
            // 
            this.btnAbout.Caption = "barButtonItem1";
            this.btnAbout.Id = 5;
            this.btnAbout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAbout.ImageOptions.SvgImage")));
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAbout_ItemClick);
            // 
            // btnLogin
            // 
            this.btnLogin.Caption = "登录";
            this.btnLogin.Id = 6;
            this.btnLogin.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnLogin.ImageOptions.SvgImage")));
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLogin_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgPlazas,
            this.rpgTime});
            this.ribbonPage1.MergeOrder = 1;
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "常规业务";
            // 
            // rpgPlazas
            // 
            this.rpgPlazas.ItemLinks.Add(this.btnLogin);
            this.rpgPlazas.ItemLinks.Add(this.barMdiChildrenListItem1);
            this.rpgPlazas.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
            this.rpgPlazas.Name = "rpgPlazas";
            this.rpgPlazas.Text = "收费站";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 795);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1432, 24);
            // 
            // xtraTabbedMdiManagerex1
            // 
            this.xtraTabbedMdiManagerex1.BackImage = global::Uixe.Watcher.Properties.Resources.backgroupimage;
            this.xtraTabbedMdiManagerex1.BigLogo = global::Uixe.Watcher.Properties.Resources.logo3;
            this.xtraTabbedMdiManagerex1.MdiParent = this;
            // 
            // rpgTime
            // 
            this.rpgTime.Name = "rpgTime";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 819);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "新疆交投云坐席";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManagerex1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgPlazas;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;
        private DevExpress.XtraBars.BarMdiChildrenListItem barMdiChildrenListItem1;
        private DevExpress.XtraBars.BarButtonItem btnAbout;
        private DevExpress.XtraBars.BarButtonItem btnLogin;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManagerEX xtraTabbedMdiManagerex1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup rpgTime;
        private System.Windows.Forms.Timer tmDateTime;
    }
}