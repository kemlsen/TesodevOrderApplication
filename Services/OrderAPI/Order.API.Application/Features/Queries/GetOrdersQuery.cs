using AutoMapper;
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
    public class GetOrdersQuery : IRequest<List<GetOrderDto>>
    {
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<GetOrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAll();

            return _mapper.Map<List<GetOrderDto>>(orders);
        }
    }
}
