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
        private ConnectionFactory _connectionFactory = null;
        private IConnection _connection = null;
        private IModel _channel = null;
        
        private ConnectionFactory connectionFactory
        {
            get
            {
                if (_connectionFactory == null)
                {
                    _connectionFactory = new ConnectionFactory()
                        {
                            HostName = this.HostName,
                            Port = this.Port,
                            VirtualHost = this.VirtualHost,
                            UserName = this.UserName,
                            Password = this.Password
                        };
                }

                return _connectionFactory;
            }
        }

        private IConnection connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = connectionFactory.CreateConnection();
                }

                // TODO: can we discover a connection-loss and reconnect?

                return _connection;
            }
        }

        private IModel channel
        {
            get 
            {
                if (_channel == null)
                {
                    _channel = connection.CreateModel();
                }

                return _channel;
            }
        }

        private const string DEFAULT_EXCHANGE = "xxx";

        private string _publish_exchange;
        private string _broadcast_exchange;

        public string HostName = "localhost";
        public int Port = 5672;
        public string VirtualHost = "/";
        public string UserName = "guest";
        public string Password = "guest";

        public MessageQ(string exchange = DEFAULT_EXCHANGE)
        {
            _publish_exchange = exchange + ".publish";
            _broadcast_exchange = exchange + ".broadcast";

            channel.ExchangeDeclare(_publish_exchange, "topic");
            channel.ExchangeDeclare(_broadcast_exchange, "fanout");
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
            var serialized_json = JsonConvert.SerializeObject(data);
            var octets = Encoding.UTF8.GetBytes(serialized_json);
            
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.DeliveryMode = 1;
            channel.BasicPublish(exchange, routing_key, basicProperties, octets);
        }
    }
}
