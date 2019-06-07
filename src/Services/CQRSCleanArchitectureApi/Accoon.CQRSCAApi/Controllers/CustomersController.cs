using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accoon.CQRSCAApi.Application.UserCases.CreateCustomer;
using Accoon.CQRSCAApi.Application.UserCases.GetCustomer;
using Accoon.CQRSCAApi.Application.UserCases.GetCustomersList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accoon.CQRSCAApi.Controllers
{
    
    public class CustomersController : BaseController
    {
      
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand  createCustomerCommand)
        {
            var customer = await Mediator.Send(createCustomerCommand);
            return CreatedAtAction(nameof(Get),new { id = customer.CustomerId}, null);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<CustomerModel>> Get([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetCustomerQuery() { Id = id}));
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<CustomerListViewModel>> Get()
        {
            var customerListModel = await Mediator.Send(new GetCustomersListQuery());
            return customerListModel;
        }


    }
}