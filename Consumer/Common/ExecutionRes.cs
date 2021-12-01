using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Common
{
    class ExecutionRes<T> where T : class
    {
        public bool IsSuccess { get; set; } = false;
        public T Value { get; set; }
        public string ErrorMessage { get; set; }
    }
}
