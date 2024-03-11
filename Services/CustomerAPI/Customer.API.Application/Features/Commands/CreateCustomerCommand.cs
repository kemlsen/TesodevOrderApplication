using AutoMapper;
using Customer.API.Application.Interfaces.Repository;
using Customer.API.Application.Wrappers;
using Customer.API.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.API.Application.Features.Commands
{
    public class CreateCustomerCommand : IRequest<ServiceResponse<Guid>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Müşteri adı boş olamaz.")
                .MinimumLength(3).WithMessage("Müşteri adı 3 karakterden fazla olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Doğru email formatı olmalıdır");
        }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ServiceResponse<Guid>>
    {
        ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<ServiceResponse<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Domain.Entities.Customer>(request);

            await _customerRepository.Create(customer);

            return new ServiceResponse<Guid>(customer.Id);
        }
    }
}
