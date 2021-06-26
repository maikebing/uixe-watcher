using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using System;
using System.Threading.Tasks;

namespace Uixe
{
    public static class MqttClientExtensions
    {
        public static Task<MqttClientPublishResult> PublishAsync<T>(this IMqttClient client, string topic, T payload) where T : class
        {
            var pjson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            Console.WriteLine($"Topic{topic}  Playload{pjson}");
            return client.PublishAsync(topic, pjson, MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce);
        }
    }
}