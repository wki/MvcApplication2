using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQ
{
    /* usage (producer)
     * 
     * var queue = MessageQ->new("exchange_name");
     * queue->Publish('render', renderOptions);
     * 
     */

    public class MessageQ
    {
        private const string DEFAULT_EXCHANGE = "cards"; 
        private string _exchange { get; set; }

        public MessageQ(string exchange = DEFAULT_EXCHANGE)
        {
            _exchange = exchange;
            Channel().ExchangeDeclare(_exchange, "direct");
        }

        private IModel Channel()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
        }

        public void Publish(string key, object data)
        {
            var channel = Channel();
            var serialized_json = JsonConvert.SerializeObject(data);
            var octets = Encoding.UTF8.GetBytes(serialized_json);
            
            // Console.WriteLine(serialized_json);
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.DeliveryMode = 1;
            channel.BasicPublish(_exchange, key, basicProperties, octets);
        }
    }
}
