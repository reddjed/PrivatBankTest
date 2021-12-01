using PrivatBankTestApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivatBankTestApi.DTO
{
    public class ResponseByIdDTO
    {
        public int ClentId { get; set; }
        public string DepartmentAdress { get; set; }
        public Status RequestStatus { get; set; }
    }
}
