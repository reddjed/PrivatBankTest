using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Common
{
    public class ExecutionRes
    {
        public bool IsSuccess { get; protected set; }
        
        public string ErrorMessage { get; protected set; }

        public static ExecutionRes CreateErrorResult(string errorMessage)
        {
            return new ExecutionRes
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
