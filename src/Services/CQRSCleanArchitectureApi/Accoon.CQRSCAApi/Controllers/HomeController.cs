using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accoon.CQRSCAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok("Running api");
        }
    }
    internal class Test
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}