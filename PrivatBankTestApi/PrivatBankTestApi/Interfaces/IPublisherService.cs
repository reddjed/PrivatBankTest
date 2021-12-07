using PrivatBankTestApi.DTO;
using PrivatBankTestApi.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivatBankTestApi.Interfaces
{
   public interface IPublisherService
    {
        Task<Result<Response_GetByIdDTO>> PublishRequestByIdAsync(ReqestByIdMsg msg);
        Task<Result<ResponseDTO>> PublishRequestsAsync(RequestsMsg msg);
        Task<Result<string>> PublishRequestAsync(RequestMsg msg);
    }
}
