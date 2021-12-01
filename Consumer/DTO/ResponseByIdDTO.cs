using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consumer.DTO
{
    public class ResponseByIdDTO
    {
        public int ClentId { get; set; }
        public string DepartmentAdress { get; set; }
        public bool? RequestStatus { get; set; } //true - Processing, null - Canceled, false - ready
    }
}
