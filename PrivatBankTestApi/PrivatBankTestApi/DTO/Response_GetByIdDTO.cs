using System;
using System.Collections.Generic;
using System.Text;

namespace PrivatBankTestApi.DTO
{
    public class Response_GetByIdDTO
    {
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

    }
}
