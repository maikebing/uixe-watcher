// Developer Express Code Central Example:
// How to put a custom UserControl in a GridView cell
//
// This example demonstrates how a custom UserControl can be used as an in-place
// editor in GridView. As described in the http://www.devexpress.com/scid=A128
// Knowledge Base, it is not possible to just place a control within a cell,
// because cells are not controls. When a cell's editor is not activated, its
// content is drawn via a painter. So, in our example, we have created a painter to
// draw the entire UserControl's content. All cells in GridView will be drawn using
// this painter until an end-user clicks a cell. In this case, an actual instance
// of the UserControl class will be created. Controls inherited from the BaseEdit
// class are drawn via their painters, other controls are drawn via the
// DrawToBitmap function. In case of 3rd-party controls, you need to draw them
// manually. If you want to use your custom control in GridView or other controls,
// you need to implement the IEditValue interface in it.
//
// See also:
// http://www.devexpress.com/scid=E2198
//
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3051

using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Uixe.Watcher.Controls
{
    public partial class CardBoxContainer : XtraUserControl, IEditValue
    {
        private bool EditValueEventBlock;

        public CardBoxContainer()
        {
            InitializeComponent();
        }

        public CardBoxInfo CardBoxInfo { get; set; } = null;

        public object EditValue
        {
            get { return (object)(CardBoxInfo); }
            set
            {
                CardBoxInfo = value as string;

                if (value == null || CardBoxInfo == null)
                {
                    progressBarControl1.Enabled = false;
                    progressBarControl1.Position = 0;
                    progressBarControl1.Properties.Maximum = 100;
                    progressBarControl1.ToolTip = "无卡箱";
                    this.Refresh();
                    return;
                }
                EditValueEventBlock = true;
                try
                {
                    progressBarControl1.Enabled = true;
                    if (CardBoxInfo.CardBoxMax == 0 && CardBoxInfo.CardBoxNow == 0)
                    {
                        progressBarControl1.Properties.Maximum = 100;
                        progressBarControl1.Position = 0;
                    }
                    else
                    {
                        progressBarControl1.Properties.Maximum = CardBoxInfo.CardBoxMax;
                        progressBarControl1.Position = CardBoxInfo.CardBoxNow;
                    }
                    if (CardBoxInfo.IsExit)
                    {
                        if (CardBoxInfo.CardBoxNow > CardBoxInfo.CardBoxMax - 10)
                        {
                            progressBarControl1.ForeColor = Color.Red;
                        }
                        else
                        {
                            progressBarControl1.ForeColor = Color.DarkGreen;
                        }
                    }
                    else
                    {
                        if (CardBoxInfo.CardBoxNow < 10)
                        {
                            progressBarControl1.ForeColor = Color.Red;
                        }
                        else
                        {
                            progressBarControl1.ForeColor = Color.DarkGreen;
                        }
                    }
                    UpdateToolTip();
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    EditValueEventBlock = false;
                }
            }
        }

        public event EventHandler EditValueChanged;

        public void OnEditValueChanged(EventArgs e)
        {
            EventHandler handler = EditValueChanged;
            if (handler != null) handler(this, e);
        }

        private void RaiseEditValueChanged()
        {
            if ((EditValueChanged == null) || (EditValueEventBlock))
                return;
            EditValueChanged(this, EventArgs.Empty);
        }

        private void progressBarControl1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            if (CardBoxInfo != null && CardBoxInfo.CardBoxMax > 0 && CardBoxInfo.CardBoxID > 0)
            {
                e.DisplayText = CardBoxInfo.CardBoxID.ToString().PadLeft(6, '0');
                UpdateToolTip();
            }
            else
            {
                e.DisplayText = "无卡箱";
            }
        }

        private void UpdateToolTip()
        {
            if (CardBoxInfo.IsExit)
            {
                progressBarControl1.ToolTip = $"卡箱{CardBoxInfo.CardBoxID.ToString().PadLeft(6, '0')}已收卡{CardBoxInfo.CardBoxNow}";
            }
            else
            {
                progressBarControl1.ToolTip = $"卡箱{CardBoxInfo.CardBoxID.ToString().PadLeft(6, '0')}已发卡{CardBoxInfo.CardBoxNow}";
            }
        }
    }

    public class CardBoxInfo
    {
        public int CardBoxID { get; set; }
        public int CardBoxMax { get; set; }
        public bool IsExit { get; set; }
        public int CardBoxNow { get; set; }

        public static implicit operator CardBoxInfo(string s)
        {
            var sp = !string.IsNullOrEmpty(s) ? s.Split(';') : new string[] { };
            CardBoxInfo cbi = null;
            if (sp.Length == 4)
            {
                try
                {
                    cbi = new CardBoxInfo { CardBoxID = int.Parse(sp[0]), CardBoxMax = int.Parse(sp[1]), CardBoxNow = int.Parse(sp[2]), IsExit = sp[3] == "X" };
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return cbi;
        }

        //由一个LaneInfo显式返回一个string
        public static explicit operator string(CardBoxInfo ret)
        {
            return ret == null ? string.Empty : $"{ret.CardBoxID};{ret.CardBoxMax};{ret.CardBoxNow}";
        }
    }
}