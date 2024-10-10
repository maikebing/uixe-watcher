using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using DevExpress.XtraBars.Docking2010.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Controls;
using Uixe.Watcher.Dtos;
using DevExpress;
using Vnc.Viewer;

namespace Uixe.Watcher.WinForms
{
    public partial class frmVNCViewer : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Color[] WidgetColors = { Color.FromArgb(255, 173, 180), Color.FromArgb(255, 243, 182), Color.FromArgb(230, 189, 228), Color.FromArgb(255, 219, 189), Color.FromArgb(189, 230, 255), Color.FromArgb(204, 245, 194) };
        public frmVNCViewer(T_Boss tb)
        {
            InitializeComponent();
            SuspendLayout();


            foreach (var p in tb.Plazas)
            {
                foreach (var l in p.Lanes)
                {
                    var ctl = new VNCViewer() { IPAddress = l.Ip, Port = 5900, Password = p.VncPwd, Caption = $"{p.StationName}{l.LaneNo}" };
                    var doc = widgetView1.AddDocument(ctl, ctl.Caption);
                    widgetView1.Documents.Add(doc);

                }
            }

            widgetView1.AllowDocumentStateChangeAnimation = DevExpress.Utils.DefaultBoolean.True;
            widgetView1.DocumentSpacing = 3;
            widgetView1.StackGroups.Clear();
            int _x = (int)Math.Sqrt(widgetView1.Documents.Count);
            int _y = _x + 1;

            for (int i = 0; i < _y; i++)
            {
                var g = new StackGroup();
                widgetView1.StackGroups.Add(g);
            }
            SetWidgetsAppearances();
            int cw = Width / _y - 6;
            foreach (Document item in widgetView1.Documents)
            {
                item.Width = (int)Math.Round(cw * (720F / 1280F) * DevExpress.Skins.DpiProvider.Default.DpiScaleFactor);
                item.Height = (int)Math.Round(cw * DevExpress.Skins.DpiProvider.Default.DpiScaleFactor);
            }
            //设置布局显示


            //tableLayout的行列定义
            //构建每个文档所属的ColumnIndex和RowIndex
            widgetView1.Rows.Clear();
            widgetView1.Columns.Clear();
            for (int i = 0; i < _x; i++)
            {
                widgetView1.Rows.Add(new RowDefinition() { });
                for (int j = 0; j < _y; j++)
                {
                    widgetView1.Columns.Add(new ColumnDefinition());
                }
            }

          
            ResumeLayout(false);
            PerformLayout();

        }


        void OnQueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Document.ControlTypeName))
                e.Control = Activator.CreateInstance(Type.GetType(e.Document.ControlTypeName)) as Control;
            else
                e.Control = new Control();
        }
        void OnLayoutModeCheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LayoutMode layoutMode = (LayoutMode)e.Item.Tag;
            ApplyLayoutMode(layoutMode);
        }
        void ApplyLayoutMode(LayoutMode layoutMode)
        {
            widgetView1.BeginUpdateAnimation();
            widgetView1.LayoutMode = layoutMode;
            switch (layoutMode)
            {
                case LayoutMode.FlowLayout:
                    InitFlowLayout();
                    break;
                case LayoutMode.FreeLayout:
                    InitFreeLayout();
                    break;
                default:
                    biItemMixer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    biDragMode.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
                    pgFlowDirection.Visible = false;
                    break;
            }
            int _x = (int)Math.Sqrt(widgetView1.Documents.Count);
            int _y = _x + 1;

            for (int i = 0; i < _x; i++)
            {
                var st = widgetView1.StackGroups[i];
                for (int j = 0; j < _y; j++)
                {
                    widgetView1.Controller.Dock(widgetView1.Documents[i * j] as Document, st);
                }
            }
            widgetView1.EndUpdateAnimation();
        }
        void InitFlowLayout()
        {
            biItemMixer.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            biDragMode.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
            pgFlowDirection.Visible = true;
        }
        void InitFreeLayout()
        {
            pgFlowDirection.Visible = false;
            biItemMixer.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
            biDragMode.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }
        void OnFlowDirectionCheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            widgetView1.BeginUpdateAnimation();
            FlowDirection flowDirection = (FlowDirection)e.Item.Tag;
            widgetView1.FlowLayoutProperties.FlowDirection = flowDirection;
            widgetView1.EndUpdateAnimation();
        }
    
        void OnCheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((e.Item as DevExpress.XtraBars.BarCheckItem).Checked) SetWidgetsAppearances();
            else ResetWidgetAppearances();
        }
        void SetWidgetsAppearances()
        {
            List<BaseDocument> documents = new List<BaseDocument>();
            documents.AddRange(widgetView1.Documents.ToArray());
            documents.AddRange(widgetView1.FloatDocuments.ToArray());
            for (int i = 0; i < documents.Count; i++)
            {
                Document document = documents[i] as Document;
                document.AppearanceActiveCaption.BackColor = WidgetColors[i % WidgetColors.Length];
                document.AppearanceCaption.BackColor = WidgetColors[i % WidgetColors.Length];
            }
        }
        void ResetWidgetAppearances()
        {
            List<BaseDocument> documents = new List<BaseDocument>();
            documents.AddRange(widgetView1.FloatDocuments.ToArray());
            documents.AddRange(widgetView1.Documents.ToArray());
            foreach (Document document in documents)
            {
                document.AppearanceActiveCaption.Reset();
                document.AppearanceCaption.Reset();
            }
        }
        void ToggleFreeLayoutDragMode(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (biDragMode.Down)
                widgetView1.FreeLayoutProperties.ItemDragStyle = ItemDragStyle.DockingHints;
            else
                widgetView1.FreeLayoutProperties.ItemDragStyle = ItemDragStyle.Default;
        }

        private void widgetView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
            if (e.Control == null) e.Control = new Button();
        }

        private void frmVNCViewer_Load(object sender, EventArgs e)
        {
            biTableMode.PerformClick();
        }
    }
}