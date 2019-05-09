using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.Api.Helpers;
using Accoon.BuildingBlocks.Common.Interfaces;
using Accoon.BuildingBlocks.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache memoryCache;

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger, IMemoryCache memoryCache)
        {
            this.customerService = customerService;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginationDto<CustomerDto>), StatusCodes.Status200OK)]
        //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        public ActionResult<PaginationDto<CustomerDto>> GetAll([FromQuery] int page = 1, [FromQuery] int size = 5, [FromQuery] bool withCache = true)
        {
            PaginationDto<CustomerDto> cachedCustomerPageOne = null;

            if (page == 1 && size == 5 && withCache)
            {  
                // using lambda
                cachedCustomerPageOne = this.memoryCache.GetOrCreate(CacheHelper.CustomerFirstPage, entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                    return this.customerService.GetCustomers(page, size);
                });
            }
            else
            {
                cachedCustomerPageOne = this.customerService.GetCustomers(page, size);
            }

            return Ok(cachedCustomerPageOne);
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