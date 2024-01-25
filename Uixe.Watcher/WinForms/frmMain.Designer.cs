
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            barMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            btnAbout = new DevExpress.XtraBars.BarButtonItem();
            btnLogin = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            rpgPlazas = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            rpgTime = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            xtraTabbedMdiManagerex1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManagerEX(components);
            tmDateTime = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManagerex1).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Blue;
            ribbon.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(35, 37, 35, 37);
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbon.ExpandCollapseItem, skinRibbonGalleryBarItem1, barMdiChildrenListItem1, btnAbout, btnLogin });
            ribbon.Location = new System.Drawing.Point(0, 0);
            ribbon.Margin = new System.Windows.Forms.Padding(4);
            ribbon.MaxItemId = 7;
            ribbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            ribbon.Name = "ribbon";
            ribbon.OptionsMenuMinWidth = 385;
            ribbon.PageHeaderItemLinks.Add(btnAbout);
            ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbon.Size = new System.Drawing.Size(1432, 167);
            ribbon.StatusBar = ribbonStatusBar;
            // 
            // skinRibbonGalleryBarItem1
            // 
            skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
            skinRibbonGalleryBarItem1.Id = 3;
            skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
            // 
            // barMdiChildrenListItem1
            // 
            barMdiChildrenListItem1.Caption = "收费站";
            barMdiChildrenListItem1.Id = 4;
            barMdiChildrenListItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barMdiChildrenListItem1.ImageOptions.SvgImage");
            barMdiChildrenListItem1.Name = "barMdiChildrenListItem1";
            // 
            // btnAbout
            // 
            btnAbout.Caption = "barButtonItem1";
            btnAbout.Id = 5;
            btnAbout.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnAbout.ImageOptions.SvgImage");
            btnAbout.Name = "btnAbout";
            btnAbout.ItemClick += btnAbout_ItemClick;
            // 
            // btnLogin
            // 
            btnLogin.Caption = "登录";
            btnLogin.Id = 6;
            btnLogin.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnLogin.ImageOptions.SvgImage");
            btnLogin.Name = "btnLogin";
            btnLogin.ItemClick += btnLogin_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { rpgPlazas, rpgTime });
            ribbonPage1.MergeOrder = 1;
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "常规业务";
            // 
            // rpgPlazas
            // 
            rpgPlazas.ItemLinks.Add(btnLogin);
            rpgPlazas.ItemLinks.Add(barMdiChildrenListItem1);
            rpgPlazas.ItemLinks.Add(skinRibbonGalleryBarItem1);
            rpgPlazas.Name = "rpgPlazas";
            rpgPlazas.Text = "收费站";
            // 
            // rpgTime
            // 
            rpgTime.Name = "rpgTime";
            // 
            // ribbonStatusBar
            // 
            ribbonStatusBar.Location = new System.Drawing.Point(0, 856);
            ribbonStatusBar.Margin = new System.Windows.Forms.Padding(4);
            ribbonStatusBar.Name = "ribbonStatusBar";
            ribbonStatusBar.Ribbon = ribbon;
            ribbonStatusBar.Size = new System.Drawing.Size(1432, 22);
            // 
            // xtraTabbedMdiManagerex1
            // 
            xtraTabbedMdiManagerex1.BackImage = Properties.Resources.backgroupimage;
            xtraTabbedMdiManagerex1.BigLogo = Properties.Resources.logo3;
            xtraTabbedMdiManagerex1.MdiParent = this;
            // 
            // tmDateTime
            // 
            tmDateTime.Enabled = true;
            tmDateTime.Tick += timer1_Tick;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1432, 878);
            Controls.Add(ribbonStatusBar);
            Controls.Add(ribbon);
            IsMdiContainer = true;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "frmMain";
            Ribbon = ribbon;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            StatusBar = ribbonStatusBar;
            Text = "新疆交投云坐席";
            FormClosed += frmMain_FormClosed;
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
            ((System.ComponentModel.ISupportInitialize)xtraTabbedMdiManagerex1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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