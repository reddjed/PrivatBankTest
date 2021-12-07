using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Common
{
    class ExecutionRes<T> : ExecutionRes where T : class 
    {
        public T Value { get; private set; }

        public static ExecutionRes<T> CreateSuccessResult(T obj)
        {
            return new ExecutionRes<T>
            {
                IsSuccess = true,
                Value = obj
            };
        }

    }
}
