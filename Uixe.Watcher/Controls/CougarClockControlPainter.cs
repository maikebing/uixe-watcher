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

using DevExpress.XtraEditors.Drawing;
using System.Drawing;

namespace Uixe.Watcher.Controls
{
    internal class CougarClockControlPainter : BaseEditPainter
    {
        public CougarClockControlPainter() : base()
        {
        }

        public override void Draw(ControlGraphicsInfoArgs info)
        {
            base.Draw(info);
            if (info.ViewInfo == null) return;
            CougarClockControlViewInfo vi = info.ViewInfo as CougarClockControlViewInfo;
            if (vi.Item == null) return;
            CougarClockRepositoryItem cri = vi.Item as CougarClockRepositoryItem;
            if (cri.ControlType == null)
                return;
            (cri.DrawControl as IEditValue).EditValue = vi.EditValue;
            cri.DrawControl.Bounds = info.Bounds;
            Bitmap bm = new Bitmap(info.Bounds.Width, info.Bounds.Height);
            cri.DrawControl.DrawToBitmap(bm, new Rectangle(0, 0, bm.Width, bm.Height));
            info.Graphics.DrawImage(bm, info.Bounds.Location);
        }
    }
}