using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accoon.Api.BussinessServices.Interfaces.HttpClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accoon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientTestController : ControllerBase
    {
        private readonly IGitHubClient gitHubClient;

        public HttpClientTestController(IGitHubClient gitHubClient)
        {
            this.gitHubClient = gitHubClient;
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetGithubRoot()
        {
            var result =  await this.gitHubClient.GetGithubRoot();
            return Ok(result);
        }
    }
}