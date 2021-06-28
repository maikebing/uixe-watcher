using DevExpress.XtraEditors;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;

namespace Uixe.Watcher.TCO
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
                    tms.MqttClient.PublishAsync($"/tco/confirm/650{tms.TCE.Network}{tms.TCE.Plaza}{tms.TCE.LaneNo}", tms.GetTCOConfirm(ok)
                      ).Wait(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public static void ShowTCOInfo(this frmPlaza form, string topic, string message, MQTTnet.Client.IMqttClient client)
        {
            try
            {
                var tc = Newtonsoft.Json.JsonConvert.DeserializeObject<tco_confirm>(message);
                if (tc.DlgType == DlgType.Weight)
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
                            Console.WriteLine($"ShowTCOInfo{ex.Message}");
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
                                form._tcocall = new frmShowTCOCall(form)
                                {
                                    MQTTClient = client,
                                    Owner = form
                                };
                            }
                            form._tcocall.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<TCOCall>(message));
                            Task.Run(PlayUitls.PlayRing);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"ShowTCOInfo{ex.Message}");
                            form._tcocall = null;
                        }
                    }
                }
                Application.DoEvents();
            }
            catch (Exception ex1)
            {
                Console.WriteLine($"ShowTCOInfo{ex1.Message}");
            }
        }

        public static void CloseTCOCall(this frmPlaza form)
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
                        Console.WriteLine($"tmNetworkTest_TickAsync{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CloseTCOCall {ex.Message}");
            }
        }
    }
}