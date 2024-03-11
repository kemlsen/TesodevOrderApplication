using AutoMapper;
using Customer.API.Application.Dto;
using Customer.API.Application.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.API.Application.Features.Queries
{
    public class GetCustomersQuery : IRequest<List<GetCustomerDto>>
    {
    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<GetCustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<List<GetCustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAll();

            return _mapper.Map<List<GetCustomerDto>>(customers);
        }
    }
}
