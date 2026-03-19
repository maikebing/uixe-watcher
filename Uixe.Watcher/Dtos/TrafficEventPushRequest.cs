using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;

namespace Uixe.Watcher.Dtos
{
    public class TrafficEventPushRequest
    {
        [JsonPropertyName("recordId")]
        public string RecordId { get; set; }

        [JsonPropertyName("eventType")]
        public string EventType { get; set; }

        [JsonPropertyName("LaneNo")]
        public string LaneNo { get; set; }

        [JsonPropertyName("capTime")]
        public DateTime? CapTime { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonPropertyName("period")]
        public int? Period { get; set; }

        [JsonPropertyName("periodByMili")]
        public int? PeriodByMili { get; set; }

        [JsonPropertyName("maxQueueLen")]
        public float? MaxQueueLen { get; set; }

        [JsonPropertyName("imageList")]
        public string ImageList { get; set; }

        [JsonPropertyName("videoList")]
        public string VideoList { get; set; }

        /// <summary>
        /// 获取事件类型显示名称。
        /// </summary>
        public string GetEventTypeText()
        {
            return EventType switch
            {
                "7" => "拥堵",
                "45" => "排队溢出",
                "46" => "排队超限",
                "4" => "停车超时",
                "trafficFlowStat" => "车流量统计",
                _ => string.IsNullOrWhiteSpace(EventType) ? "未知事件" : EventType
            };
        }

        /// <summary>
        /// 获取统计时长显示文本。
        /// </summary>
        public string GetDurationText()
        {
            var totalMilliseconds = ((Period ?? 0) * 60 * 1000) + (PeriodByMili ?? 0);
            if (totalMilliseconds <= 0)
            {
                return string.Empty;
            }

            var duration = TimeSpan.FromMilliseconds(totalMilliseconds);
            List<string> parts = new List<string>();
            if (duration.Hours > 0)
            {
                parts.Add($"{duration.Hours}小时");
            }

            if (duration.Minutes > 0)
            {
                parts.Add($"{duration.Minutes}分钟");
            }

            if (duration.Seconds > 0 || parts.Count == 0)
            {
                parts.Add($"{Math.Max(1, duration.Seconds)}秒");
            }

            return string.Join(string.Empty, parts);
        }

        /// <summary>
        /// 获取事件发生时间显示文本。
        /// </summary>
        public string GetEventTimeText()
        {
            var eventTime = CapTime ?? StartTime;
            return eventTime?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? string.Empty;
        }

        /// <summary>
        /// 获取适合语音播报的事件时间文本。
        /// </summary>
        public string GetEventSpeechTimeText()
        {
            var eventTime = CapTime ?? StartTime;
            return eventTime?.ToString("HH点mm分", CultureInfo.InvariantCulture) ?? string.Empty;
        }

        /// <summary>
        /// 获取最大排队长度显示文本。
        /// </summary>
        public string GetQueueLengthText()
        {
            return MaxQueueLen.HasValue
                ? $"{MaxQueueLen.Value.ToString("0.##", CultureInfo.InvariantCulture)} 米"
                : string.Empty;
        }

        /// <summary>
        /// 生成用于语音播报的一句话摘要。
        /// </summary>
        public string GetSummary(string stationName, string laneNo = null)
        {
            List<string> parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(stationName))
            {
                parts.Add(stationName);
            }

            var currentLaneNo = string.IsNullOrWhiteSpace(laneNo) ? LaneNo : laneNo;
            if (!string.IsNullOrWhiteSpace(currentLaneNo))
            {
                parts.Add($"{currentLaneNo}车道");
            }

            parts.Add(GetEventTypeText());

            if (MaxQueueLen.HasValue)
            {
                parts.Add($"最大排队{MaxQueueLen.Value.ToString("0.##", CultureInfo.InvariantCulture)}米");
            }

            var durationText = GetDurationText();
            if (!string.IsNullOrWhiteSpace(durationText))
            {
                parts.Add($"统计时长{durationText}");
            }

            var eventTimeText = GetEventSpeechTimeText();
            if (!string.IsNullOrWhiteSpace(eventTimeText))
            {
                parts.Add($"时间{eventTimeText}");
            }

            return string.Join("，", parts.Where(part => !string.IsNullOrWhiteSpace(part))) + "。";
        }
    }

    public class TrafficEventPushResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
