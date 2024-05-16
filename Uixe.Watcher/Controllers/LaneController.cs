using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;
using Uixe.Watcher.Msg;
using Uixe.Watcher.TCO;
namespace Uixe.Watcher.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LaneController : ControllerBase
    {

        private readonly ILogger<LaneController> _logger;
        private readonly AppSettings option;
        private readonly IMemoryCache _cache;

        public LaneController(ILogger<LaneController> logger, IOptions<AppSettings> option, IMemoryCache cache)
        {
            _logger = logger;
            this.option = option.Value;
            _cache = cache;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_status(string plazaid, string laneno, LaneStatus status)
        {
            try
            {
                await Task.Run(() =>
                 {
                     var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                     frm?.ShowLaneInfor(plazaid, laneno, status);
                 });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_tco_weightAsync(string plazaid, MsgWeightTCOCALL msgWeight)
        {
            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                        if (frm != null)
                        {
                            frm?.ShowTCOInfo(msgWeight);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"无法打开{nameof(frmPlaza)} 站代码为{plazaid}");
                      
                    }
                    
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, ex.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_tco_confirmAsync(string plazaid, TCOCall msg)
        {
            try
            {
                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    frm?.ShowTCOInfo(msg);
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }


        [HttpPost]
        public async Task<ActionResult<ApiResult>> emrc_main_message(string plazaid, MsgInfo msg)
        {
            try
            {

                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    frm?.ShowMessageView(msg);
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApiResult>> Lanespecial(string plazaid, Lanespecial msg)
        {
            try
            {
                await Task.Run(() =>
                {
                    var frm = _cache.Get<frmPlaza>($"{nameof(frmPlaza)}_{plazaid}");
                    bool canshow = true;
                    if (frm.Onley6769)
                    {
                        if (msg.SubHead.StaffID == "777777" || msg.SubHead.StaffID == "999999")
                        {
                            canshow = true;
                        }
                        else
                        {
                            canshow=false;
                        }
                    }
                    else
                    {
                        canshow = true;
                    }
                    if (canshow)
                    {
                        string laneid = $"650{msg.Head.NetNo}{msg.Head.PlazaNo}{msg.Head.LaneID}";
                        Lanespecial msgold = null;
                        if (!_cache.TryGetValue(laneid, out msgold))
                        {
                            var msg1 = _cache.Set(laneid, msg, TimeSpan.FromSeconds(3));

                            frm?.Invoke(() => frm.Alert(msg.Title, msg.Context));
                            if (frm?.EnableLanespecialSpeeker == true)
                            {
                                SpeechUtils.Speecher.SpeakAsync(msg.Title);
                            }
                        }
                    }
                });
                return Ok(new ApiResult(ApiCode.OK, "OK"));
            }
            catch (Exception)
            {

                return BadRequest(new ApiResult(ApiCode.BadRequst, "OK"));
            }
        }
    }
}

