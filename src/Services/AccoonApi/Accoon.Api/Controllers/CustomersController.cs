using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.BuildingBlocks.Common.Interfaces;
using Accoon.BuildingBlocks.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accoon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<CustomersController> logger;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginationDto<CustomerDto>), StatusCodes.Status200OK)]
        public ActionResult<PaginationDto<CustomerDto>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 5)
        {            
            var paginationDto = this.customerService.GetCustomers(page, size);
            return Ok(paginationDto);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save([FromBody] CustomerDto customerDto)
        {  
            var id = await this.customerService.SaveCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }

        [Route("{id:long}")]
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetById([FromRoute] long id)
        {
            var customer = await this.customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}