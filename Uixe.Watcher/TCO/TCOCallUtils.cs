using DevExpress.XtraEditors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;
using Uixe.Watcher.Uitls;
using WhoIamDtos;

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
                tms?._logger.LogError(ex, "提交TCO确认信息时遇到异常");
                XtraMessageBox.Show(ex.Message);
            }
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
                    form._logger.LogError(ex, "tmNetworkTest_TickAsync异常");
                }
            }
            catch (Exception ex)
            {
                form._logger.LogError(ex, "tmNetworkTest_TickAsync异常");
            }
        }
    }
}