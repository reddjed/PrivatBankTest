using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrivatBankTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        // GET: api/<RequestController>
        [HttpGet]
        public StatusCodeResult Get()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-qeue",
                durable: true,
                exclusive: false,
                arguments: null);
            var massege = new { Data = "Test" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(massege));
            channel.BasicPublish("", "demo-qeue", null, body);
            return StatusCode(200);
        }

        // GET api/<RequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RequestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
