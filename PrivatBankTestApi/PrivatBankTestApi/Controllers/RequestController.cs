using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrivatBankTestApi.DTO;
using PrivatBankTestApi.Interfaces;
using PrivatBankTestApi.Messages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrivatBankTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IPublisherService _msgPublisherService;
        public RequestController(IPublisherService msgPublisherService)
        {
            _msgPublisherService = msgPublisherService;
        }
        // GET: api/<RequestController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        // GET api/<RequestController>/5
        
        [HttpGet("{RequestId}")]
        [ProducesResponseType(typeof(Result<ResponseByIdDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ResponseByIdDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestById([FromRoute] ReqestByIdMsg msg)
        {
            
            var response = await _msgPublisherService.PublishRequestByIdAsync(msg);

            return response.IsSuccess
                ? StatusCode(StatusCodes.Status200OK, response.Value)
                : StatusCode(StatusCodes.Status404NotFound, response.ErrorMessage);
        }

            // POST api/<RequestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
