using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.Api.BussinessServices.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Accoon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValueService _valueService;

        public ValuesController(IValueService valueService)
        {
            this._valueService = valueService;
        }


        // GET api/values
        [HttpGet]
        public async Task<ValueDTO> Get()
        {
            return await this._valueService.TestValueServiceMethod();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Console.WriteLine("aa");
            Console.WriteLine("bb");
            Console.WriteLine("cc");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
