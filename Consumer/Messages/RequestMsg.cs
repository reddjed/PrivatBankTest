using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.Messages
{
    public class RequestMsg
    {
        public string CLientId { get; set; }
        public string DepartmentAddress { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
    }
}
