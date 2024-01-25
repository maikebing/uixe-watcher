namespace Uixe.Watcher.Controls
{
    partial class CougarClockContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
            digitalGauge1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge();
            digitalBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent();
            ((System.ComponentModel.ISupportInitialize)digitalGauge1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)digitalBackgroundLayerComponent1).BeginInit();
            SuspendLayout();
            // 
            // gaugeControl1
            // 
            gaugeControl1.AutoLayout = false;
            gaugeControl1.BackColor = System.Drawing.Color.Transparent;
            gaugeControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gaugeControl1.ColorScheme.Color = System.Drawing.Color.Transparent;
            gaugeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] { digitalGauge1 });
            gaugeControl1.LayoutInterval = 7;
            gaugeControl1.LayoutPadding = new DevExpress.XtraGauges.Core.Base.Thickness(7);
            gaugeControl1.Location = new System.Drawing.Point(0, 0);
            gaugeControl1.Margin = new System.Windows.Forms.Padding(4);
            gaugeControl1.Name = "gaugeControl1";
            gaugeControl1.Size = new System.Drawing.Size(513, 81);
            gaugeControl1.TabIndex = 0;
            // 
            // digitalGauge1
            // 
            digitalGauge1.AppearanceOff.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#EAECF1");
            digitalGauge1.AppearanceOn.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#7184BA");
            digitalGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent[] { digitalBackgroundLayerComponent1 });
            digitalGauge1.Bounds = new System.Drawing.Rectangle(-1, -1, 524, 94);
            digitalGauge1.DigitCount = 17;
            digitalGauge1.DisplayMode = DevExpress.XtraGauges.Core.Model.DigitalGaugeDisplayMode.SevenSegment;
            digitalGauge1.Name = "digitalGauge1";
            digitalGauge1.Padding = new DevExpress.XtraGauges.Core.Base.TextSpacing(26, 20, 26, 20);
            digitalGauge1.ProportionalStretch = false;
            digitalGauge1.Text = "2021-01-18 14:42:54";
            // 
            // digitalBackgroundLayerComponent1
            // 
            digitalBackgroundLayerComponent1.BottomRight = new DevExpress.XtraGauges.Core.Base.PointF2D(812.6751F, 106.075F);
            digitalBackgroundLayerComponent1.Name = "digitalBackgroundLayerComponent1";
            digitalBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.DigitalBackgroundShapeSetType.Style11;
            digitalBackgroundLayerComponent1.TopLeft = new DevExpress.XtraGauges.Core.Base.PointF2D(26F, 0F);
            digitalBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // CougarClockContainer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gaugeControl1);
            DoubleBuffered = true;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "CougarClockContainer";
            Size = new System.Drawing.Size(513, 81);
            ((System.ComponentModel.ISupportInitialize)digitalGauge1).EndInit();
            ((System.ComponentModel.ISupportInitialize)digitalBackgroundLayerComponent1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraGauges.Win.GaugeControl gaugeControl1;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalGauge digitalGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Digital.DigitalBackgroundLayerComponent digitalBackgroundLayerComponent1;
    }
}
