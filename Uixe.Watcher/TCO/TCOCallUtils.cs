using DevExpress.XtraEditors;
using MQTTnet.Client;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;

namespace Uixe.Watcher.V1
{
    public static class TCOCallUtils
    {
 
        private delegate void DShowTCOQueryInfo(string mu);

       

       

        public static void Submit(bool ok, WeightTCOConfirm tms)
        {
            try
            {
                if (tms != null)
                {
                    tms.MqttClient.PublishAsync($"/tco/confirm/650{tms.TCE.Network}{tms.TCE.Plaza}{tms.TCE.LaneNo}",tms.GetTCOConfirm(ok)
                      ).Wait(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public static void ShowTCOInfo(this frmMain form, string topic,string message, MQTTnet.Client.IMqttClient client)
        {
            try
            {
                var tc= Newtonsoft.Json.JsonConvert.DeserializeObject<tco_confirm>(message);
                if (tc.DlgType== DlgType.Weight)
                {
                    lock (form)
                    {
                        try
                        {
                            if (form.WeightTCOCall == null || form.WeightTCOCall.IsDisposed || !form.WeightTCOCall.IsHandleCreated)
                            {
                                form.WeightTCOCall = new frmWeightTCOCall();
                                form.WeightTCOCall.Main = form;
                               
                                form.WeightTCOCall.LoadInfo(client);
                                form.WeightTCOCall.Hide();
                            }
                            form.WeightTCOCall.ShowTCOMsg(Newtonsoft.Json.JsonConvert.DeserializeObject<MsgWeightTCOCALL>(message));
                            Task.Run(PlayUitls.PlayRing);
                        }
                        catch (Exception ex)
                        {
                            //     TCS.Log.LogInfo("ERROR:<{0}>\r\n{1}\r\n", ex.Message, mu);
                            form.WeightTCOCall = null;
                        }
                    }
                }
                else
                {
                    lock (form)
                    {
                        try
                        {
                            if (form._tcocall == null || form._tcocall.IsDisposed || !form._tcocall.IsHandleCreated)
                            {

                                form._tcocall = new frmShowTCOCall
                                {
                                    MQTTClient = client,
                                    Owner = form,
                                    Main = form
                                   
                                };
                            }
                            form._tcocall.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<TCOCall>(message));
                            Task.Run(PlayUitls.PlayRing);
                        }
                        catch (Exception ex)
                        {
                            //  TCS.Log.LogInfo("ERROR:<{0}>\r\n{1}\r\n", ex.Message, mu);
                            form._tcocall = null;
                        }
                    }
                }
                Application.DoEvents();
            }
            catch (Exception ex1)
            {
            }
        }

        public static void CloseTCOCall(this frmMain form)
        {
            try
            {
                lock (form)
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
                     //   Log.LogException("RemoveNowTab 2", "", ex);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}