using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.Api.Helpers;
using Accoon.BuildingBlocks.Common.Entities;
using Accoon.BuildingBlocks.Common.Filters;
using Accoon.BuildingBlocks.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

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
        private readonly PaginationOption defaultPaginationOption;


        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger, IMemoryCache memoryCache, IOptions<PaginationOption> defaultPaginationOption)
        {
            this.customerService = customerService;
            this.logger = logger;
            this.memoryCache = memoryCache;
            this.defaultPaginationOption = defaultPaginationOption.Value;

        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginationDto<CustomerDto>), StatusCodes.Status200OK)]
        //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        [ServiceFilter(typeof(PaginationOptionFilter))]
        public ActionResult<PaginationDto<CustomerDto>> GetAll([FromQuery] PaginationOption paginationQueryParams, [FromQuery] bool withCache = true)
        {
            
            //paginationQueryParams.Page = paginationQueryParams.Page ?? this.paginationOption.Page;
            //paginationQueryParams.Size = paginationQueryParams.Size ?? this.paginationOption.Size;

            PaginationDto<CustomerDto> cachedCustomerPageOne = null;

            if (paginationQueryParams.Page == defaultPaginationOption.Page && paginationQueryParams.Size == defaultPaginationOption.Page && withCache)
            {
                // using lambda
                cachedCustomerPageOne = this.memoryCache.GetOrCreate(CacheHelper.CustomerFirstPage, entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                    return this.customerService.GetCustomers(paginationQueryParams.Page, paginationQueryParams.Size);
                });
            }
            else
            {
                cachedCustomerPageOne = this.customerService.GetCustomers(paginationQueryParams.Page, paginationQueryParams.Size);
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