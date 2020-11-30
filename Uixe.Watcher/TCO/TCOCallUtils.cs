using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
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

        public static void ShowTCOQueryInfo(object obj)
        {
            try
            {
                
                if (obj == null)
                {
                   Debug.WriteLine ("ShowTCOQueryInfo  TCS.Msg.MQL.MsgUnion mu =  obj as TCS.Msg.MQL.MsgUnion; mu为空 ");
                }
                else
                {
                    if (Program.MainForm != null && !Program.MainForm.IsDisposed && Program.MainForm.IsHandleCreated)
                    {
                        if (Program.MainForm.InvokeRequired)
                        {
                            try
                            {
                                Program.MainForm.Invoke((DShowTCOQueryInfo)ShowTCOQueryInfo, obj);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("ShowTCOQueryInfo Program.MainForm.Invoke((DShowTCOQueryInfo)ShowTCOQueryInfo, mu);", obj.ToString(), ex);
                            }
                        }
                        else
                        {
                            try
                            {
                               
                               //    ShowTCOInfo(obj);
                            }
                            catch (Exception ex)
                            {
                                CloseTCOCall();
                                Debug.WriteLine ("ShowTCOQueryInfo", obj.ToString(), ex);
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine("ShowTCOQueryInfo 主窗体未创建或者已经释放");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void Submit(bool ok, WeightTCOConfirm tms)
        {
            try
            {
                if (tms != null)
                {
                    var lstd = tms.dbxLSTD.GetSelectedDataRow() as FreshAgriProducts;
                    string cmd = "TCOCONFIRM";
                    string param = (ok ? "TCO_OK" : "TCO_NO") + (RuntimeSetting.NowCollect?.UserId ?? string.Empty.PadLeft(6, ' '));
                    param += ((char)(int.Parse(lstd?.Id ?? "0") + 'A')).ToString();// (char)(char.Parse(lstd?.Id ?? "0") + 'A');
                    param += tms.CarPlateTextEdit.Text.PadRight(14, ' ').Substring(0, 14);
                //    BLLWatcher.SendReturnInfoToLane(TCS.DAL.DALWatcher.GetLaneInfo(tms.TCO.Head.Network + tms.TCO.Head.Plaza, tms.TCO.Head.LaneNo), cmd, param);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public static void ShowTCOInfo(string topic,string message)
        {
            try
            {
                if (topic.EndsWith("TCOWEIGHT"))
                {
                    lock (thisLock1)
                    {
                        try
                        {
                            if (WeightTCOCall == null || WeightTCOCall.IsDisposed || !WeightTCOCall.IsHandleCreated)
                            {
                                WeightTCOCall = new frmWeightTCOCall
                                {
                                    Owner = Program.MainForm
                                };
                            }
                            WeightTCOCall.ShowTCOMsg( Newtonsoft.Json.JsonConvert.DeserializeObject<MsgWeightTCOCALL>( message));
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
            catch (Exception)
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