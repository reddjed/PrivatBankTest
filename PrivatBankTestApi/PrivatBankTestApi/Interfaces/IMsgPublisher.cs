using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivatBankTestApi.Interfaces
{
    public interface IMsgPublisher
    {
        string ToQueue(string message, string queue);
        void Close();
    }
}
