﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivatBankTestApi.DTO
{/// <summary>
///   DTO for Request
/// </summary>
    public class ResponseDTO
    {
        public int CLientId { get; set; }
        public string DepartmentAdress { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public bool? RequestStatus { get; set; } // value represents true - Processing, null - Canceled, false - ready
    }
}
