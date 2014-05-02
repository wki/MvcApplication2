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
     * var queue = MessageQ->new;
     * queue->Publish('render', renderOptions);
     * 
     */
    public class MessageQ
    {
        public MessageQ()
        {
           Channel().ExchangeDeclare("cards", "direct");
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
            var body = Encoding.UTF8.GetBytes(data.ToString());
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.DeliveryMode = 1;
            channel.BasicPublish("cards", key, basicProperties, body);
        }
    }
}
