﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Accoon.Api.BussinessServices.Entities.EntityDTOs;
using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.BuildingBlocks.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accoon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult GetAll()
        {
            this.logger.LogInformation("start");
            var customerList = this.customerService.GetAllCustomers();
            return Ok(customerList);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Save([FromBody] CustomerDto customerDto)
        {
            var id = await this.customerService.SaveCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }

        [Route("{id:long}")]
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        public ActionResult<CustomerDto> GetById([FromRoute] long id)
        {
            return new CustomerDto();
        }

    }
}