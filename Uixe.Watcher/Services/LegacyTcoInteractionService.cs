using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Responses;
using Uixe.Watcher.Msg;
using Uixe.Watcher.TCO;

namespace Uixe.Watcher.Services;

public sealed class LegacyTcoInteractionService : ILegacyTcoInteractionService
{
    private readonly IPlazaContextService _plazaContextService;
    private readonly ILegacyWindowCoordinator _legacyWindowCoordinator;

    public LegacyTcoInteractionService(IPlazaContextService plazaContextService, ILegacyWindowCoordinator legacyWindowCoordinator)
    {
        _plazaContextService = plazaContextService;
        _legacyWindowCoordinator = legacyWindowCoordinator;
    }

    public Task<ApiResult> ShowWeightMessageAsync(string plazaId, object message, CancellationToken cancellationToken = default)
    {
        var frm = _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;
        if (frm == null)
        {
            return Task.FromResult(new ApiResult(ApiCode.NotFound, $"没有找到{plazaId}的收费站ID"));
        }

        frm.Invoke(() =>
        {
            var window = (frmWeightTCOCall)_legacyWindowCoordinator.GetOrCreateWeightTcoWindow(plazaId, frm, () =>
            {
                var wtco = new frmWeightTCOCall(frm.GetPlaza(plazaId), frm._runtimeSetting, frm.settings, frm._logger);
                wtco.LoadInfo();
                wtco.Hide();
                return wtco;
            });
            window.ShowTCOMsg((MsgWeightTCOCALL)message);
        });

        return Task.FromResult(new ApiResult(ApiCode.OK, "OK"));
    }

    public Task<ApiResult> ShowTcoConfirmAsync(string plazaId, object message, CancellationToken cancellationToken = default)
    {
        var frm = _plazaContextService.GetPlazaHost(plazaId) as frmPlaza;
        if (frm == null)
        {
            return Task.FromResult(new ApiResult(ApiCode.NotFound, $"没有找到{plazaId}的收费站ID"));
        }

        frm.Invoke(() =>
        {
            var window = (frmShowTCOCall)_legacyWindowCoordinator.GetOrCreateTcoCallWindow(plazaId, frm, () => new frmShowTCOCall(frm, frm.GetPlaza(plazaId)));
            window.TCOCallxxx = (TCOCall)message;
            window.Show();
        });

        return Task.FromResult(new ApiResult(ApiCode.OK, "OK"));
    }
}
