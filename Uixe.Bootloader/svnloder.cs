using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using SharpSvn;
using SharpSvn.Security;
using Uixe.Bootloader.Properties;

namespace Uixe.Bootloader
{
    public class SvnCredentialProvider : ICredentials
    {
        private readonly NetworkCredential _credential;

        public SvnCredentialProvider(string userName, string password, string url)
        {
            _credential = new NetworkCredential(userName, password, url);
        }

        #region ICredentials Members

        public NetworkCredential GetCredential(Uri uri, string authType)
        {
            return _credential;
        }

        #endregion
    }

    public class svnloder
    {
        public static bool Cancel;
        private readonly SvnClient SC;
        private string prog;


        public svnloder(Label info, string excsvn)
        {
            Info = info;
            SC = new SvnClient();
            SC.Authentication.ClearAuthenticationCache();
            SC.Authentication.Clear();
            SC.Authentication.DefaultCredentials = new SvnCredentialProvider(
                    Properties.Settings.Default.svnusername,
                    Properties.Settings.Default.svnpassword,
                    excsvn);
            
        }

        public Label Info { get; set; }


        public string GetAppLoc()
        {
            return GetAppLoc("");
        }
        public static string locroot { get; set; }
        public static string GetLoc(string loc)
        {
            string path = "";
            if (string.IsNullOrEmpty(locroot))
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                  Settings.Default.applocpath + loc;
            }
            else
            {
                path = Path.Combine(locroot, loc);
            }
            return path;
        }
        public string GetAppLoc(string loc)
        {
            return GetLoc(loc);
        }
        public bool CheckVer(string svn, string loc)
        {
            bool result = false;
            var repos = new SvnUriTarget(svn);
            var local = new SvnPathTarget(GetAppLoc(loc));
            try
            {
                notiny = "正在检查服务器版本...";
                ShowInfo();
                SvnInfoEventArgs serverInfo;
                bool oks = SC.GetInfo(repos, out serverInfo);
                notiny = "正在检查本地版本...";
                ShowInfo();
                SvnInfoEventArgs clientInfo;
                bool okc = SC.GetInfo(local, out clientInfo);
                if (oks && okc) //如果客户端服务端都会成功， 则对比服务器版本， 否则返回true 执行更新命令
                {
                    result = (serverInfo.Revision > clientInfo.Revision);
                    if (serverInfo.RepositorySize >clientInfo.WorkingCopySize )
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
         
                ShowInfo(string.Format("检查完毕，服务器版本{0}客户端版本{1}",
                                       (serverInfo != null ? serverInfo.Revision.ToString() : "(未知)"),
                                       (clientInfo != null ? clientInfo.Revision.ToString() : "(未知)")
                             ));
            }
            catch (Exception)
            {
                result = true;
                ShowInfo("检查文件是出现错误...");
            }
            return result;
        }

        public bool Init(string svn, string loc)
        {
            ShowInfo("正在初始化....");
            bool result = true;
            events();
            if (!Directory.Exists(GetAppLoc(loc)))
            {
                notiny = "正在检出文件.";
                ShowInfo();
                result = SC.CheckOut(new SvnUriTarget(svn), GetAppLoc(loc));
                ShowInfo("文件检出完成.");
            }
            try
            {
                var uri = SC.GetRepositoryRoot(GetAppLoc(loc));
               
            }
            catch (Exception)
            {

                 
            }

            ShowInfo("初始化完毕.");
            return result;
        }

        private void events()
        {
            SC.Authentication.UserNamePasswordHandlers +=
                new EventHandler<SvnUserNamePasswordEventArgs>(Authentication_UserNamePasswordHandlers);
            SC.Authentication.UserNameHandlers += new EventHandler<SvnUserNameEventArgs>(Authentication_UserNameHandlers);
            SC.Progress += new EventHandler<SvnProgressEventArgs>(SC_Progress);
            SC.Processing += new EventHandler<SvnProcessingEventArgs>(SC_Processing);
            SC.Notify += new EventHandler<SvnNotifyEventArgs>(SC_Notify);
            SC.SvnError += new EventHandler<SvnErrorEventArgs>(SC_SvnError);
            SC.Conflict += new EventHandler<SvnConflictEventArgs>(SC_Conflict);
            SC.Cancel += new EventHandler<SvnCancelEventArgs>(SC_Cancel);
        }

        private void SC_Cancel(object sender, SvnCancelEventArgs e)
        {
            e.Cancel = Cancel;
        }

        private void SC_Conflict(object sender, SvnConflictEventArgs e)
        {
            ShowInfo("文件冲突" + e.ConflictReason.ToString());
        }

        private void SC_SvnError(object sender, SvnErrorEventArgs e)
        {
            e.Cancel = true;
            WriteError(e.Exception);
        }

        public void WriteError(Exception e_Exception)
        {
            ShowInfo(e_Exception.Message + e_Exception.Source + "\r\n"
                     + e_Exception.StackTrace + "\r\n"
                     );
        }

        private void SC_Processing(object sender, SvnProcessingEventArgs e)
        {
            ShowInfo("请稍候.......");
        }

        private void SC_Progress(object sender, SvnProgressEventArgs e)
        {
            if (e.Progress == 0)
            {
                prog = "";
            }
            else
            {
                if (e.TotalProgress ==
                    -1)
                {
                    if (e.Progress > 1024)
                    {
                        prog = ((float)(e.Progress) / 1024F).ToString("0.00") + "KB";
                    }
                    else
                    {
                        prog = ((float)(e.Progress)).ToString("0.00") + "Bytes";
                    }
                }
                else
                {
                    prog = (e.Progress).ToString("0.00");
                }
            }
            ShowInfo();
        }

        public void ShowInfo()
        {
            ShowInfo(notiny + (string.IsNullOrWhiteSpace(prog) ? "" : "(" + prog + ")"));
        }

        public void ShowInfo(string info)
        {
            Info.Invoke((MethodInvoker)delegate
                                        {
                                            try
                                            {
                                                Info.Text = info;
                                                File.AppendAllText(
                                                    GetAppLoc() + DateTime.Now.ToString("yyyyMMddHH") + ".Log",
                                                    string.Format("{0}{1}\r\n", DateTime.Now.ToString("yyyyMMddHHmmss:"), info));
                                                Application.DoEvents();
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        });
        }

        public bool Update(int i)
        {
            bool ok = false;
            try
            {
                var sua = new SvnUpdateArgs { Revision = new SvnRevision(i) };
                events();
                ok = SC.CleanUp(GetAppLoc());
                ok = SC.Update(GetAppLoc(), sua);
            }
            catch (Exception ex)
            {
                ShowInfo("更新时遇到问题：" + ex.Message + "\r\n");
                WriteError(ex);
            }
            ShowInfo("更新完成....");
            return ok;
        }
    
        public bool Update(string loc)
        {
            ShowInfo("正在更新....");
            SC.CleanUp(GetAppLoc(loc));
            bool ok = SC.Update(GetAppLoc(loc));
            if (!ok)
            {
                ShowInfo("未成功,正在清理...");
                SC.CleanUp(GetAppLoc(loc));
                ShowInfo("清理完毕,正在更新....");
                ok = SC.Update(GetAppLoc(loc));
                if (!ok)
                {
                    ShowInfo("正在撤销本地修改....");
                    SC.Revert(GetAppLoc(loc));
                    ShowInfo("正在清理....");
                    SC.CleanUp(GetAppLoc(loc));
                    ShowInfo("正在更新....");
                    ok = SC.Update(GetAppLoc(loc));
                }
            }
            ShowInfo("更新操作完成");
            return ok;
        }

        private void Authentication_UserNameHandlers(object sender, SvnUserNameEventArgs e)
        {
            e.UserName = Settings.Default.svnusername;
        }

        private void Authentication_UserNamePasswordHandlers(object sender, SvnUserNamePasswordEventArgs e)
        {
            e.Password = Settings.Default.svnpassword;
            e.UserName = Settings.Default.svnusername;
        }

        #region 提示信息

        private string notiny;

        private void SC_Notify(object sender, SvnNotifyEventArgs e)
        {
            notiny = string.Format("{0}:{1}.", getstringact(e.Action), new FileInfo(e.FullPath).Name);
            ShowInfo();
        }

        #endregion

        #region 提示信息中文对照

        public string getstringact(SvnNotifyAction act)
        {
            string result = null;
            switch (act)
            {
                case SvnNotifyAction.Add:
                    result = "添加";
                    break;
                case SvnNotifyAction.BlameRevision:
                    break;
                case SvnNotifyAction.ChangeListClear:
                    break;
                case SvnNotifyAction.ChangeListMoved:
                    break;
                case SvnNotifyAction.ChangeListSet:
                    break;
                case SvnNotifyAction.CommitAddCopy:
                    result = "提交添加副本";
                    break;
                case SvnNotifyAction.CommitAdded:
                    break;
                case SvnNotifyAction.CommitDeleted:
                    break;
                case SvnNotifyAction.CommitModified:
                    break;
                case SvnNotifyAction.CommitReplaced:
                    break;
                case SvnNotifyAction.CommitReplacedWithCopy:
                    break;
                case SvnNotifyAction.CommitSendData:
                    break;
                case SvnNotifyAction.Copy:
                    break;
                case SvnNotifyAction.Delete:
                    break;
                case SvnNotifyAction.Excluded:
                    result = "要排除";
                    break;
                case SvnNotifyAction.Exists:
                    result = "已存在";
                    break;
                case SvnNotifyAction.ExternalFailed:
                    result = "外部失败";
                    break;
                case SvnNotifyAction.FailedConflict:
                    result = "失败冲突";
                    break;
                case SvnNotifyAction.FailedForbiddenByServer:
                    break;
                case SvnNotifyAction.FailedLocked:
                    break;
                case SvnNotifyAction.FailedMissing:
                    break;
                case SvnNotifyAction.FailedNoParent:
                    break;
                case SvnNotifyAction.FailedOutOfDate:
                    result = "已过期且失败";
                    break;
                case SvnNotifyAction.FollowUrlRedirect:
                    break;
                case SvnNotifyAction.LockFailedLock:
                    break;
                case SvnNotifyAction.LockFailedUnlock:
                    break;
                case SvnNotifyAction.LockLocked:
                    break;
                case SvnNotifyAction.LockUnlocked:
                    break;
                case SvnNotifyAction.MergeBegin:
                    break;
                case SvnNotifyAction.MergeBeginForeign:
                    break;
                case SvnNotifyAction.MergeCompleted:
                    break;
                case SvnNotifyAction.NonExistentPath:
                    break;
                case SvnNotifyAction.PatchApplied:
                    break;
                case SvnNotifyAction.PatchAppliedHunk:
                    break;
                case SvnNotifyAction.PatchFoundAlreadyApplied:
                    break;
                case SvnNotifyAction.PatchRejectedHunk:
                    break;
                case SvnNotifyAction.PropertyAdded:
                    break;
                case SvnNotifyAction.PropertyDeleted:
                    break;
                case SvnNotifyAction.PropertyDeletedNonExistent:
                    break;
                case SvnNotifyAction.PropertyModified:
                    break;
                case SvnNotifyAction.RecordMergeInfo:
                    break;
                case SvnNotifyAction.RecordMergeInfoElided:
                    break;
                case SvnNotifyAction.RecordMergeInfoStarted:
                    break;
                case SvnNotifyAction.Resolved:

                    break;
                case SvnNotifyAction.Restore:
                    result = "恢复";
                    break;
                case SvnNotifyAction.Revert:
                    result = "还原";
                    break;
                case SvnNotifyAction.RevertFailed:
                    result = "还原失败";
                    break;
                case SvnNotifyAction.RevisionPropertyDeleted:
                    break;
                case SvnNotifyAction.RevisionPropertySet:
                    break;
                case SvnNotifyAction.Skip:
                    result = "跳过";
                    break;
                case SvnNotifyAction.SkipConflicted:
                    result = "跳过冲突";
                    break;
                case SvnNotifyAction.StatusCompleted:
                    result = "状态已完成";
                    break;
                case SvnNotifyAction.StatusExternal:
                    break;
                case SvnNotifyAction.TreeConflict:
                    result = "数冲突";
                    break;
                case SvnNotifyAction.UpdateAdd:
                    result = "更新新增";
                    break;
                case SvnNotifyAction.UpdateCompleted:
                    result = "更新已完成";
                    break;
                case SvnNotifyAction.UpdateDelete:
                    result = "更新删除";
                    break;
                case SvnNotifyAction.UpdateExternal:
                    result = "更新外部";
                    break;
                case SvnNotifyAction.UpdateExternalRemoved:
                    result = "更新外部已删除";
                    break;
                case SvnNotifyAction.UpdateReplace:
                    result = "更新替换";
                    break;
                case SvnNotifyAction.UpdateShadowedAdd:
                    break;
                case SvnNotifyAction.UpdateShadowedDelete:
                    break;
                case SvnNotifyAction.UpdateShadowedUpdate:
                    break;
                case SvnNotifyAction.UpdateSkipAccessDenied:
                    break;
                case SvnNotifyAction.UpdateSkipObstruction:
                    break;
                case SvnNotifyAction.UpdateSkipWorkingOnly:
                    break;
                case SvnNotifyAction.UpdateStarted:
                    result = "更新已开始";
                    break;
                case SvnNotifyAction.UpdateUpdate:
                    result = "更新修改";
                    break;
                case SvnNotifyAction.UpgradedDirectory:
                    result = "更新目录";
                    break;
            }
            return result ?? (result = act.ToString());
        }

        #endregion
    }
}