using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivatBankTestApi
{
    public class Result<T> where T : class
    {
        public bool IsSuccess { get; set; } = false;
        public T Value { get; set; }
        public string ErrorMessage { get; set; }
    }
}

