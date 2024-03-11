using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Application.Dto;
using Order.API.Application.Features.Commands;
using Order.API.Application.Features.Queries;
using Order.API.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Guid>>> Create(CreateOrderCommand request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<bool>>> Update(UpdateOrderCommand request)
            => await _mediator.Send(request);

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(DeleteOrderCommand request)
            => await _mediator.Send(request);


        [HttpPost("Get")]
        public async Task<ActionResult<List<GetOrderDto>>> Get(GetOrdersQuery request)
            => await _mediator.Send(request);

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<List<GetOrderDto>>> Get(Guid id)
        {
            var request = new GetOrdersByIdQuery { Id = id };
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDto>> GetById(Guid id)
        {
            var request = new GetOrderByIdQuery { Id = id };
            return await _mediator.Send(request);
        }
    }
}
