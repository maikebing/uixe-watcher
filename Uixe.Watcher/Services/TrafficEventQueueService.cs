using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Dtos;

namespace Uixe.Watcher.Services
{
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
        public void Enqueue(frmPlaza form, T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request)
        {
            ArgumentNullException.ThrowIfNull(form);
            ArgumentNullException.ThrowIfNull(plaza);
            ArgumentNullException.ThrowIfNull(lane);
            ArgumentNullException.ThrowIfNull(request);

            _queue.Enqueue(new TrafficEventQueueItem(form, plaza, lane, request));
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
                    await ShowAsync(item).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "处理交通事件提醒失败，LaneNo={LaneNo}, RecordId={RecordId}", item.Request.LaneNo, item.Request.RecordId);
                }
            }
        }

        private static Task ShowAsync(TrafficEventQueueItem item)
        {
            TaskCompletionSource<bool> taskCompletionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

            try
            {
                if (item.Form.IsDisposed || !item.Form.IsHandleCreated)
                {
                    taskCompletionSource.SetResult(true);
                    return taskCompletionSource.Task;
                }

                item.Form.BeginInvoke((MethodInvoker)delegate
                {
                    try
                    {
                        if (!item.Form.IsDisposed && item.Form.IsHandleCreated)
                        {
                            var trafficForm = item.Form.ShowTrafficEvent(item.Plaza, item.Lane, item.Request);
                            if (trafficForm == null || trafficForm.IsDisposed)
                            {
                                taskCompletionSource.TrySetResult(true);
                                return;
                            }

                            FormClosedEventHandler closedHandler = null;
                            closedHandler = delegate
                            {
                                trafficForm.FormClosed -= closedHandler;
                                taskCompletionSource.TrySetResult(true);
                            };
                            trafficForm.FormClosed += closedHandler;
                            return;
                        }

                        taskCompletionSource.TrySetResult(true);
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                taskCompletionSource.TrySetException(ex);
            }

            return taskCompletionSource.Task;
        }

        private sealed class TrafficEventQueueItem
        {
            public TrafficEventQueueItem(frmPlaza form, T_Plaza plaza, T_Lane lane, TrafficEventPushRequest request)
            {
                Form = form;
                Plaza = plaza;
                Lane = lane;
                Request = request;
            }

            public frmPlaza Form { get; }

            public T_Plaza Plaza { get; }

            public T_Lane Lane { get; }

            public TrafficEventPushRequest Request { get; }
        }
    }
}
