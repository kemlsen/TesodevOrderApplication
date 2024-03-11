using Customer.API.Application.Dto;
using Customer.API.Application.Features.Commands;
using Customer.API.Application.Features.Queries;
using Customer.API.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Guid>>> Create(CreateCustomerCommand request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<bool>>> Update(UpdateCustomerCommand request)
            => await _mediator.Send(request);

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(DeleteCustomerCommand request)
            => await _mediator.Send(request);


        [HttpPost("Get")]
        public async Task<ActionResult<List<GetCustomerDto>>> Get(GetCustomersQuery request)
            => await _mediator.Send(request);


        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> GetById(Guid id)
        {
            var request = new GetCustomerByIdQuery { Id = id };
            return await _mediator.Send(request);
        }

        [HttpPost("Validate")]
        public async Task<ActionResult<bool>> ValidateCustomer(ValidateCustomerQuery request)
            => await _mediator.Send(request);
    }
}
