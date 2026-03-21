using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Services
{
    public interface ITrafficEventDisplayHandler
    {
        Task ShowAsync(T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request, CancellationToken cancellationToken = default);
    }

    public sealed class TrafficEventQueueService
    {
        private readonly ConcurrentQueue<TrafficEventQueueItem> _queue = new ConcurrentQueue<TrafficEventQueueItem>();
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);
        private readonly ILogger<TrafficEventQueueService> _logger;

        public TrafficEventQueueService(ILogger<TrafficEventQueueService> logger)
        {
            _logger = logger;
            _ = Task.Run(ProcessQueueAsync);
        }

        /// <summary>
        /// 加入交通事件提醒队列。
        /// </summary>
        public void Enqueue(ITrafficEventDisplayHandler displayHandler, T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request)
        {
            ArgumentNullException.ThrowIfNull(displayHandler);
            ArgumentNullException.ThrowIfNull(plaza);
            ArgumentNullException.ThrowIfNull(lane);
            ArgumentNullException.ThrowIfNull(request);

            _queue.Enqueue(new TrafficEventQueueItem(displayHandler, plaza, lane, request));
            _signal.Release();
        }

        private async Task ProcessQueueAsync()
        {
            while (true)
            {
                await _signal.WaitAsync().ConfigureAwait(false);
                if (!_queue.TryDequeue(out var item))
                {
                    continue;
                }

                try
                {
                    await item.DisplayHandler.ShowAsync(item.Plaza, item.Lane, item.Request).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "处理交通事件提醒失败，LaneNo={LaneNo}, RecordId={RecordId}", item.Request.LaneNo, item.Request.RecordId);
                }
            }
        }

        private sealed class TrafficEventQueueItem
        {
            public TrafficEventQueueItem(ITrafficEventDisplayHandler displayHandler, T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request)
            {
                DisplayHandler = displayHandler;
                Plaza = plaza;
                Lane = lane;
                Request = request;
            }

            public ITrafficEventDisplayHandler DisplayHandler { get; }

            public T_Plaza Plaza { get; }

            public T_Lane Lane { get; }

            public TrafficEventPushRequest Request { get; }
        }
    }
}
