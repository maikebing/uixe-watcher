using System.Text.Json;
using Uixe.Copilot.Application.Abstractions;
using Uixe.Copilot.Contracts.Dtos;
using Uixe.Copilot.Domain.Entities;
using Uixe.Copilot.Infrastructure.TrafficEvents;

namespace Uixe.Copilot.Infrastructure.Persistence.TrafficEvents;

public sealed class FileTrafficEventRepository : ITrafficEventRepository
{
    private readonly string _storagePath;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public FileTrafficEventRepository(InfrastructureOptions options)
    {
        _storagePath = Path.GetFullPath(options.TrafficEventStoragePath);
        var directory = Path.GetDirectoryName(_storagePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public async Task SaveAsync(TrafficEventPushRequestDto request, CancellationToken cancellationToken = default)
    {
        await _lock.WaitAsync(cancellationToken);
        try
        {
            var events = await ReadEntitiesAsync(cancellationToken);
            var entity = request.ToEntity();
            var index = events.FindIndex(x => string.Equals(x.Id, entity.Id, StringComparison.OrdinalIgnoreCase));
            if (index >= 0)
            {
                events[index] = entity;
            }
            else
            {
                events.Add(entity);
            }

            await WriteEntitiesAsync(events, cancellationToken);
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task<IReadOnlyCollection<TrafficEventListItemDto>> GetRecentEventsAsync(CancellationToken cancellationToken = default)
    {
        var events = await ReadEntitiesAsync(cancellationToken);
        return events.OrderByDescending(x => x.OccurredAt).Select(x => x.ToListItemDto()).ToList().AsReadOnly();
    }

    public async Task<TrafficEventListItemDto?> GetByIdAsync(string eventId, CancellationToken cancellationToken = default)
    {
        var events = await ReadEntitiesAsync(cancellationToken);
        return events.FirstOrDefault(x => string.Equals(x.Id, eventId, StringComparison.OrdinalIgnoreCase))?.ToListItemDto();
    }

    public async Task<IReadOnlyCollection<TrafficEventListItemDto>> QueryAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        var pageNo = query.PageNo <= 0 ? 1 : query.PageNo;
        var pageSize = query.PageSize <= 0 ? 20 : query.PageSize;

        var events = ApplyFilter(await ReadEntitiesAsync(cancellationToken), query);
        return events
            .OrderByDescending(x => x.OccurredAt)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x.ToListItemDto())
            .ToList()
            .AsReadOnly();
    }

    public async Task<int> CountAsync(TrafficEventHistoryQueryDto query, CancellationToken cancellationToken = default)
    {
        var events = await ReadEntitiesAsync(cancellationToken);
        return ApplyFilter(events, query).Count();
    }

    private async Task<List<TrafficEvent>> ReadEntitiesAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_storagePath))
        {
            return new List<TrafficEvent>();
        }

        await using var stream = File.OpenRead(_storagePath);
        var result = await JsonSerializer.DeserializeAsync<List<TrafficEvent>>(stream, cancellationToken: cancellationToken);
        return result ?? new List<TrafficEvent>();
    }

    private async Task WriteEntitiesAsync(List<TrafficEvent> events, CancellationToken cancellationToken)
    {
        await using var stream = File.Create(_storagePath);
        await JsonSerializer.SerializeAsync(stream, events, cancellationToken: cancellationToken);
    }

    private static IEnumerable<TrafficEvent> ApplyFilter(IEnumerable<TrafficEvent> events, TrafficEventHistoryQueryDto query)
    {
        return events
            .Where(x => !query.StartTime.HasValue || x.OccurredAt >= query.StartTime.Value)
            .Where(x => !query.EndTime.HasValue || x.OccurredAt <= query.EndTime.Value)
            .Where(x => string.IsNullOrWhiteSpace(query.PlazaName) || x.PlazaName.Contains(query.PlazaName, StringComparison.OrdinalIgnoreCase))
            .Where(x => string.IsNullOrWhiteSpace(query.EventType) || x.Title.Contains(query.EventType, StringComparison.OrdinalIgnoreCase))
            .Where(x => string.IsNullOrWhiteSpace(query.Status) || x.Status.Contains(query.Status, StringComparison.OrdinalIgnoreCase));
    }
}