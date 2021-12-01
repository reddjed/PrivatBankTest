using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Messages
{
    public class RequestMsg
    {
        public int CLientId { get; set; }
        public string DepartmentAdress { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
