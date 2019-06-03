using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.CQRSCLApi.Persistence.infastructure
{
    public interface IEnvironmentService
    {
        string EnvironmentName { get; set; }
    }
}
