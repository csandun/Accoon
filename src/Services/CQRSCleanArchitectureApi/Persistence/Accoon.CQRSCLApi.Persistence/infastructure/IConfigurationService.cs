using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCLApi.Persistence.infastructure
{
    public interface IConfigurationService
    {
        IConfiguration GetConfiguration();
    }
}
