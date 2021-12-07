using Microsoft.CodeAnalysis;
using Dapper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using Consumer.Messages;
using Consumer.DA;

namespace Consumer
{
    public class Program
    {
        public static DbHandler db = new DbHandler();
        static void Main(string[] args)
        {


            // var factory = new ConnectionFactory() { HostName = "localhost" };

            // var connection = factory.CreateConnection();
            // var channel = connection.CreateModel();
            // var replyQueueName = channel.QueueDeclare("rpc_queue");
            // channel.BasicQos(0, 1, false);
            // var consumer = new EventingBasicConsumer(channel);
            // channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);
            // consumer.Received += async (model, e) =>
            //{

            //    var props = e.BasicProperties;
            //    var replyProps = channel.CreateBasicProperties();
            //    replyProps.CorrelationId = props.CorrelationId;
            //    props.CorrelationId = e.BasicProperties.CorrelationId;

            //    string response = "";
            //    try
            //    {

            //        var body = e.Body.ToArray();
            //        var request = Encoding.UTF8.GetString(body);
            //        var id = JsonConvert.DeserializeObject<ReqestByIdMsg>(request);


            //        var res = await db.GetById_spAsync(id.RequestId);
            //        response = JsonConvert.SerializeObject(res);
            //    }

            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(" [.] " + ex.Message);
            //        response = "";
            //    }
            //    finally
            //    {
            //        var responseBytes = Encoding.UTF8.GetBytes(response);
            //        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
            //           basicProperties: replyProps, body: responseBytes);
            //        channel.BasicAck(deliveryTag: e.DeliveryTag,
            //           multiple: false);
            //    }

            //};
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var replyQueueName = channel.QueueDeclare("rpc_queue1");
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "rpc_queue1", autoAck: false, consumer: consumer);
            consumer.Received += async (model, e) =>
            {

                var props = e.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;
                props.CorrelationId = e.BasicProperties.CorrelationId;

                string response = "";
                try
                {

                    var body = e.Body.ToArray();
                    var request = Encoding.UTF8.GetString(body);
                    var d = JsonConvert.DeserializeObject<RequestMsg>(request);


                    var res = await db.SaveRequest_spAsync(d.CLientId,d.Currency,d.Status,Decimal.Parse(d.Amount),d.DepartmentAddress);
                    response = JsonConvert.SerializeObject(res);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(" [.] " + ex.Message);
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                       basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: e.DeliveryTag,
                       multiple: false);
                }

            };
            Console.ReadLine();
        }
    }
}
