using MQTTnet.Client;
using MQTTnet.Client.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uixe.Watcher.Uitls
{

    public static class MqttClientExtensions
    {

        public static Task<MqttClientPublishResult> PublishAsync<T>(this IMqttClient client, string topic, T payload) where T : class
        {
            return client.PublishAsync(topic, Newtonsoft.Json.JsonConvert.SerializeObject(payload), MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce);
        }
    }
}
