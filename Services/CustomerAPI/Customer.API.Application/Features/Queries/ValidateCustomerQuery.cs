using Customer.API.Application.Interfaces.Repository;
using Customer.API.Application.Wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.API.Application.Features.Queries
{
    public class ValidateCustomerQuery : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class ValidateCustomerQueryValidator : AbstractValidator<ValidateCustomerQuery>
    {
        public ValidateCustomerQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Müşteri Kimliği boş olamaz.");
        }
    }

    public class ValidateCustomerQueryHandler : IRequestHandler<ValidateCustomerQuery, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public ValidateCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(ValidateCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.Id);

            return customer == null ? false : true;
        }
    }
}
