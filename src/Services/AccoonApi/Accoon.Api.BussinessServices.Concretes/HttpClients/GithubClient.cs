using Accoon.Api.BussinessServices.Interfaces.HttpClients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Accoon.Api.BussinessServices.Concretes.HttpClients
{
    public class GithubClient : IGitHubClient
    {
        private readonly HttpClient _client;

        public GithubClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetGithubRoot()
        {
            var s = await _client.GetStringAsync("/");
            return s;
        }
    }
}
