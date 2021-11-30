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
            throw new NotImplementedException();
        }

        public async Task<Result<ResponseByIdDTO>> PublishRequestByIdAsync(ReqestByIdMsg msg)
        {
            var body = JsonConvert.SerializeObject(msg);
            var data = await Task.Run(() => _msgPublisher.ToQueue(body, "demo-queue"));
            var response = JsonConvert.DeserializeObject<Result<ResponseByIdDTO>>(data);
            return response;     
        }

        public Task<Result<ResponseDTO>> PublishRequestsAsync(RequestsMsg msg)
        {
            throw new NotImplementedException();
        }
    }
}
