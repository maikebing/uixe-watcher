using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;

namespace Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;

public sealed class DatabaseTrafficEventRepository : ITrafficEventRepository
{
    public Task SaveAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("数据库仓储骨架已建立，后续接入 PostgreSQL/SQL Server 实现。");
    }

    public Task<IReadOnlyCollection<TrafficEventListItemDto>> GetRecentEventsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("数据库仓储骨架已建立，后续接入 PostgreSQL/SQL Server 实现。");
    }

    public Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("数据库仓储骨架已建立，后续接入 PostgreSQL/SQL Server 实现。");
    }

    public Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("数据库仓储骨架已建立，后续接入 PostgreSQL/SQL Server 实现。");
    }
}