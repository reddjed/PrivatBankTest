using Newtonsoft.Json;
using PrivatBankTestApi.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivatBankTestApi.Publisher
{
    public class MsgPublisher : IMsgPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _replyQueueName;
        private readonly EventingBasicConsumer _consumer;
        private readonly BlockingCollection<string> _respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties _props;

        public MsgPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _replyQueueName = _channel.QueueDeclare().QueueName;
            _consumer = new EventingBasicConsumer(_channel);

            _props = _channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            _props.CorrelationId = correlationId;
            _props.ReplyTo = _replyQueueName;

            _consumer.Received += (model, e) =>
            {
                var body = e.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (e.BasicProperties.CorrelationId == correlationId)
                {
                    _respQueue.Add(response);
                }
            };

            _channel.BasicConsume(
                consumer: _consumer,
                queue: _replyQueueName,
                autoAck: true);
        }

        public string ToQueue(string message, string queue)
        {
            var messageBuffer= Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: "",
                routingKey: queue,
                basicProperties: _props,
                body: messageBuffer);

            return _respQueue.Take();
        }

        public void Close()
        {
            _connection?.Close();
        }
    }
}
