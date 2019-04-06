using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.Api.BussinessServices.Interfaces.HttpClients
{
    public interface IGitHubClient
    {
        Task<string> GetGithubRoot();
    }
}
