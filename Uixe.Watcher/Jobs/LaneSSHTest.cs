
using DevExpress.XtraSpellChecker.Parser;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Quartz;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;


namespace Uixe.Watcher.Jobs
{
    [QuartzJobScheduler(12 * 60 * 60, true, 60, Desciption = "车道连接性检查")]
    [DisallowConcurrentExecution]
    public class LaneSSHTest : IJob
    {
        private readonly ILogger _logger;
        private readonly LiteDB.ConnectionString _connection;
        private readonly Encoding _encoding = Encoding.GetEncoding(936);
        private List<string> passwords = "Cdaq!147\r\nMdtlfpq!594\r\nMdkelpq!839\r\nMdtcpq!53\r\nMdklampq!8976\r\nMdhmpq!67\r\nMdakspq!182\r\nMdktpq!85\r\nMdylpq!69\r\nMdcjpq!37\r\nMdatspq!152\r\nMdkspq!82\r\nMdaltpq!195\r\nMdwlmqpq!2971\r\nMdblpq!59\r\nMdhtpq!65\r\nCdaq!147\r\n".Split("\r\n").ToList();
        private readonly AppSettings _setting;
        public LaneSSHTest(ILogger<LaneSSHTest> logger, IServiceScopeFactory scopeFactor, LiteDB.ConnectionString connection, IOptions<AppSettings> option)
        {
            _logger = logger;
            _connection = connection;
            _setting = option.Value;
        }

        public  Task Execute(IJobExecutionContext context)
        {
            try
            {
                using (var db = new LiteDatabase(_connection))
                {
                    if (db.BeginTrans())
                    {
                        var tlane = db.GetCollection<t_lane>();
                        if (tlane.Count() == 0)
                        {
                            _setting.whoiam?.plazas?.ForEach(p =>
                            {
                                p.lanes?.ForEach(l =>
                                {

                                    t_lane tl = new t_lane();
                                    tl.plazaid = p.id;
                                    tl.lane_id = l.lane_id;
                                    tl.lane_no = l.lane_no;
                                    tl.ip = l.ip;
                                    tl.password = "";
                                    tl.usename = "root";
                                    tl.Id = p.id + l.lane_no;
                                    tlane.Insert(tl);
                                });
                            });
                        }
                        db.Commit();
                    }
                    var ltb = db.GetCollection<t_lane>();
                    var lanes = ltb.FindAll().ToList();
                    lanes.ForEach(lan =>
                    {
                        SshClient client = null;
                        var password = lan.password;
                        if (!passwords.Contains(password))
                        {
                            passwords.Insert(0, password);
                        }
                        string msg = "";
                        try
                        {
                            client = new SshClient(lan.ip, lan.usename, password);
                            client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                            client.Connect();
                            if (!client.IsConnected)
                            {
                                password = "";
                            }
                        }
                        catch (SshAuthenticationException)
                        {
                            password = "";
                        }
                        catch (SocketException ex)
                        {
                            msg = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }

                        if (string.IsNullOrEmpty(password))
                        {

                            var pel = Parallel.ForEach(passwords, (passwd, pls) =>
                            {
                                try
                                {
                                    var clientx = new SshClient(lan.ip, lan.usename, passwd);
                                    clientx.ConnectionInfo.Timeout = TimeSpan.FromSeconds(5);
                                    clientx.Connect();
                                    if (clientx.IsConnected)
                                    {
                                        client = clientx;
                                        password = passwd;
                                        pls.Break();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    msg = ex.Message;
                                }
                            });
                        }
                        if (client == null || !client.IsConnected)
                        {
                            lan.password = "";
                        }
                        else if (client != null && client.IsConnected)
                        {
                            lan.ip = password;
                        }
                        ltb.Update(lan);
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}
