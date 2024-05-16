using DevExpress.XtraEditors;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;
using Uixe.Watcher.Uitls;

namespace Uixe.Watcher.TCO
{
    public static class TCOCallUtils
    {
        private delegate void DShowTCOQueryInfo(string mu);

        public static void Submit(bool ok, WeightTCOConfirm tms)
        {
            try
            {
                _ = tms.Lane.TCO_Confirm(tms.GetTCOConfirm(ok));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        public static void ShowTCOInfo(this frmPlaza form, MsgWeightTCOCALL msg)
        {
            form.Invoke(() =>
            {
                if (form.WeightTCOCall == null || form.WeightTCOCall.IsDisposed || !form.WeightTCOCall.IsHandleCreated)
                {
                    form.WeightTCOCall = new frmWeightTCOCall(form.Plaza, form._runtimeSetting, form.settings);
                    form.WeightTCOCall.LoadInfo();
                    form.WeightTCOCall.Hide();
                }
                form.WeightTCOCall.ShowTCOMsg(msg);

            });
            Task.Run(PlayUitls.PlayRing);
        }


        public static void ShowTCOInfo(this frmPlaza form, TCOCall call)
        {

            form.Invoke(() =>
            {

                if (form._tcocall == null || form._tcocall.IsDisposed || !form._tcocall.IsHandleCreated)
                {
                    form._tcocall = new frmShowTCOCall(form);
                }
                form._tcocall.Show(call);
                Task.Run(PlayUitls.PlayRing);
            });
        }


        public static void CloseTCOCall(this frmPlaza form)
        {
            try
            {

                try
                {
                    if (form.WeightTCOCall != null && !form.WeightTCOCall.IsDisposed && form.WeightTCOCall.IsHandleCreated)
                    {
                        if (form.WeightTCOCall.InvokeRequired)
                        {
                            form.WeightTCOCall.Invoke((MethodInvoker)delegate { form.CloseTCOCall(); });
                        }
                        else
                        {
                            form.WeightTCOCall.Hide();
                        }
                    }

                    if (form._tcocall != null && !form._tcocall.IsDisposed && form._tcocall.IsHandleCreated)
                    {
                        if (form._tcocall.InvokeRequired)
                        {
                            form._tcocall.Invoke((MethodInvoker)delegate { form.CloseTCOCall(); });
                        }
                        else
                        {
                            form._tcocall.Hide();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"tmNetworkTest_TickAsync{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CloseTCOCall {ex.Message}");
            }
        }
    }
}