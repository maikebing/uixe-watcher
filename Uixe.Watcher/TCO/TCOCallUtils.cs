using DevExpress.XtraEditors;
using MQTTnet.Client;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.Ring;

namespace Uixe.Watcher.V1
{
    public static class TCOCallUtils
    {
        public static frmShowTCOCall _tcocall;
        public static frmWeightTCOCall WeightTCOCall;

        private delegate void DShowTCOQueryInfo(string mu);

        public static object thisLock = new object();
        public static object thisLock1 = new object();

       

        public static void Submit(bool ok, WeightTCOConfirm tms)
        {
            try
            {
                if (tms != null)
                {
                    var lstd = tms.dbxLSTD.GetSelectedDataRow() as FreshAgriProducts;
                    tms.MqttClient.PublishAsync($"/tco/confirm/650{tms.TCE.Network}{tms.TCE.Plaza}{tms.TCE.LaneNo}",
                        new
                        {
                            message = (ok ? "TCO_OK" : "TCO_NO"),
                            anwser = ok,
                            transNO= tms.TCE.MsgTcoTran.TransNO,
                            userid = RuntimeSetting.NowCollect?.UserId,
                            datetime = DateTime.UtcNow,
                            fresh_agri= lstd,
                            plate= tms.CarPlateTextEdit.Text
                        }).Wait(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public static void ShowTCOInfo(Form form, string topic,string message, MQTTnet.Client.IMqttClient client)
        {
            try
            {
                var tc= Newtonsoft.Json.JsonConvert.DeserializeObject<tco_confirm>(message);
                if (tc.DlgType== DlgType.Weight)
                {
                    lock (thisLock1)
                    {
                        try
                        {
                            if (WeightTCOCall == null || WeightTCOCall.IsDisposed || !WeightTCOCall.IsHandleCreated)
                            {
                                WeightTCOCall = new frmWeightTCOCall();
                                WeightTCOCall.LoadInfo(client);
                                WeightTCOCall.Hide();
                            }
                            WeightTCOCall.ShowTCOMsg(Newtonsoft.Json.JsonConvert.DeserializeObject<MsgWeightTCOCALL>(message));
                            System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(PlayUitls.PlayRing), null);
                        }
                        catch (Exception ex)
                        {
                       //     TCS.Log.LogInfo("ERROR:<{0}>\r\n{1}\r\n", ex.Message, mu);
                            WeightTCOCall = null;
                        }
                    }
                }
                else
                {
                    lock (thisLock)
                    {
                        try
                        {
                            if (_tcocall == null || _tcocall.IsDisposed || !_tcocall.IsHandleCreated)
                            {

                                _tcocall = new frmShowTCOCall
                                {
                                    Owner = Program.MainForm
                                };
                            }
                            _tcocall.Show(Newtonsoft.Json.JsonConvert.DeserializeObject<TCOCall>(message));
                            System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(PlayUitls.PlayRing), null);
                        }
                        catch (Exception ex)
                        {
                          //  TCS.Log.LogInfo("ERROR:<{0}>\r\n{1}\r\n", ex.Message, mu);
                            _tcocall = null;
                        }
                    }
                }
                Application.DoEvents();
            }
            catch (Exception ex1)
            {
            }
        }

        public static void CloseTCOCall()
        {
            try
            {
                lock (thisLock)
                {
                    try
                    {
                        if (WeightTCOCall != null && !WeightTCOCall.IsDisposed && WeightTCOCall.IsHandleCreated)
                        {
                            if (WeightTCOCall.InvokeRequired)
                            {
                                WeightTCOCall.Invoke((MethodInvoker)delegate { CloseTCOCall(); });
                            }
                            else
                            {
                                WeightTCOCall.Hide();
                            }
                        }

                        if (_tcocall != null && !_tcocall.IsDisposed && _tcocall.IsHandleCreated)
                        {
                            if (_tcocall.InvokeRequired)
                            {
                                _tcocall.Invoke((MethodInvoker)delegate { CloseTCOCall(); });
                            }
                            else
                            {
                                _tcocall.Hide();
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