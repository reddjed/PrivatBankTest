using Newtonsoft.Json;
using PrivatBankTestApi.DTO;
using PrivatBankTestApi.Interfaces;
using PrivatBankTestApi.Messages;
using PrivatBankTestApi.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivatBankTestApi.Services
{
    public class MsgPublisherService : IPublisherService
    {
        private readonly IMsgPublisher _msgPublisher;
        public MsgPublisherService(IMsgPublisher msgPublisher)
        {
            _msgPublisher = msgPublisher;
        }
        public async Task<Result<string>> PublishRequestAsync(RequestMsg msg)
        {
            var body = JsonConvert.SerializeObject(msg);

            var data = await Task.Run(() => _msgPublisher.ToQueue(body, "rpc_queue1"));
            var response = JsonConvert.DeserializeObject<Result<string>>(data);
            return response;
        }

        public async Task<Result<Response_GetByIdDTO>> PublishRequestByIdAsync(ReqestByIdMsg msg)
        {
            var body = JsonConvert.SerializeObject(msg);

            var data = await Task.Run(() => _msgPublisher.ToQueue(body, "rpc_queue"));
            var response = JsonConvert.DeserializeObject<Result<Response_GetByIdDTO>>(data);
            return response;     
        }

        public Task<Result<ResponseDTO>> PublishRequestsAsync(RequestsMsg msg)
        {
            throw new NotImplementedException();
        }
    }
}
