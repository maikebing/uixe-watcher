using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Uixe.Watcher.Controls
{
    public partial class CougarClockContainer : XtraUserControl, IEditValue
    {
        public CougarClockContainer()
        {
            InitializeComponent();
        }

        #region IEditValue Members

        private bool EditValueEventBlock = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object EditValue
        {
            get { return (object)digitalGauge1.Text; }
            set
            {
                if (value == null)
                {
                    this.Refresh();
                }
                else
                {
                    digitalGauge1.Text = value.ToString();
                }
            }
        }

        public event EventHandler EditValueChanged;

        public void OnEditValueChanged(EventArgs e)
        {
            EventHandler handler = EditValueChanged;
            if (handler != null) handler(this, e);
        }

        #endregion IEditValue Members

        private void RaiseEditValueChanged()
        {
            if ((EditValueChanged == null) || (EditValueEventBlock))
                return;
            EditValueChanged(this, EventArgs.Empty);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return base.IsInputKey(keyData);
        }

        protected override bool IsInputChar(char charCode)
        {
            return base.IsInputChar(charCode);
        }
    
    }
}