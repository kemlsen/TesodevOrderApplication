using AutoMapper;
using FluentValidation;
using MediatR;
using Order.API.Application.Dto;
using Order.API.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.API.Application.Features.Queries
{
    public class GetOrdersByIdQuery : IRequest<List<GetOrderDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetOrdersByIdQueryValidator : AbstractValidator<GetOrdersByIdQuery>
    {
        public GetOrdersByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sipariş id boş olamaz.");
        }
    }

    public class GetOrdersByIdQueryHandle : IRequestHandler<GetOrdersByIdQuery, List<GetOrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByIdQueryHandle(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<GetOrderDto>> Handle(GetOrdersByIdQuery request, CancellationToken cancellationToken)
        {
            var order = _orderRepository.GetAllById(request.Id);

            return _mapper.Map<List<GetOrderDto>>(order);
        }
    }
}
