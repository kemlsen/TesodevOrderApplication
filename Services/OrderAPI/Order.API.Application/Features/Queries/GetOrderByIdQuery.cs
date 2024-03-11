using AutoMapper;
using FluentValidation;
using MediatR;
using Order.API.Application.Dto;
using Order.API.Application.Interfaces.Repository;
using Order.API.Application.Wrappers;
using Order.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.API.Application.Features.Queries
{
    public class GetOrderByIdQuery : IRequest<GetOrderDto>
    {
        public Guid Id { get; set; }
    }

    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sipariş id boş olamaz.");
        }
    }

    public class GetOrderByIdQueryHandle : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandle(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = _orderRepository.GetById(request.Id);

            return _mapper.Map<GetOrderDto>(order);
        }
    }
}
