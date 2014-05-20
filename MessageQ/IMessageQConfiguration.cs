using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQ
{
    public interface IMessageQConfiguration
    {
        string HostName {get; set; }
        int Port { get; set; }
        string VirtualHost { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
