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
     * var queue = new MessageQ();
     * var queue = new MessageQ(connectionFactory);
     * 
     * queue->Publish('render', renderOptions);
     * 
     */

    public class MessageQ
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        
        private const string DEFAULT_EXCHANGE = "xxx";

        private string _publish_exchange;
        private string _broadcast_exchange;

        public MessageQ(string exchange = DEFAULT_EXCHANGE)
            :this(new ConnectionFactory(), exchange)
        {
        }

        public MessageQ(ConnectionFactory connectionFactory, string exchange)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
            _publish_exchange = exchange + ".publish";
            _broadcast_exchange = exchange + ".broadcast";

            var channel = Channel();
            channel.ExchangeDeclare(_publish_exchange, "topic");
            channel.ExchangeDeclare(_broadcast_exchange, "fanout");
        }

        private IModel Channel()
        {
            return _connection.CreateModel();
        }

        public void Publish<T>(T data)
        {
            var routing_key = data.GetType().FullName;
            _emit(data, _publish_exchange, routing_key);
        }

        public void Broadcast<T>(T data)
        {
            var routing_key = data.GetType().FullName;
            _emit(data, _broadcast_exchange, routing_key);
        }

        private void _emit(Object data, string exchange, string routing_key)
        {
            var channel = Channel();
            var serialized_json = JsonConvert.SerializeObject(data);
            var octets = Encoding.UTF8.GetBytes(serialized_json);
            
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.DeliveryMode = 1;
            channel.BasicPublish(exchange, routing_key, basicProperties, octets);
        }
    }
}
