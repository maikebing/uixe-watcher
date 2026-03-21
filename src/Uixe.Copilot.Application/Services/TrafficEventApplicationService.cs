using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Contracts.Responses;

namespace Uixe.Copilot.Application.Services;

public sealed class TrafficEventApplicationService : ITrafficEventApplicationService
{
    private readonly ITrafficEventWorkflowService _workflowService;
    private readonly IPlazaContextService _plazaContextService;
    private readonly IRealtimePushService _realtimePushService;
    private readonly ITrafficEventRepository _trafficEventRepository;

    public TrafficEventApplicationService(
        ITrafficEventWorkflowService workflowService,
        IPlazaContextService plazaContextService,
        IRealtimePushService realtimePushService,
        ITrafficEventRepository trafficEventRepository)
    {
        _workflowService = workflowService;
        _plazaContextService = plazaContextService;
        _realtimePushService = realtimePushService;
        _trafficEventRepository = trafficEventRepository;
    }

    public async Task<TrafficEventPushResponse> SubmitAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return CreateResponse(1, "请求体不能为空");
        }

        if (string.IsNullOrWhiteSpace(request.LaneNo))
        {
            return CreateResponse(1, "LaneNo不能为空");
        }

        var matched = await _workflowService.EnqueueAsync(request, _plazaContextService.GetPlazas(), cancellationToken);
        if (!matched)
        {
            return CreateResponse(1, $"未匹配到车道：{request.LaneNo}");
        }

        await _trafficEventRepository.SaveAsync(request, cancellationToken);
        await _realtimePushService.PublishTrafficEventSubmittedAsync(request, cancellationToken);
        return CreateResponse(0, "推送成功");
    }

    private static TrafficEventPushResponse CreateResponse(int code, string message)
    {
        return new TrafficEventPushResponse
        {
            Code = code,
            Message = message,
            Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
        };
    }
}
