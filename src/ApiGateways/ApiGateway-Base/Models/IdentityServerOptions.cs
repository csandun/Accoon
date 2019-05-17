using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway_Base.Models
{
    public class IdentityServerOptions
    {
        public string ServerIP { get; set; }

        public int ServerPort { get; set; }

        public string IdentityScheme { get; set; }

        public string ResourceName { get; set; }
    }
}
