using Customer.API.Application.Helpers;
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

namespace Customer.API.Application.Features.Commands
{
    public class DeleteCustomerCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Müşteri id boş olamaz.");
        }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ServiceResponse<bool>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidationHelper _validationHelper;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IValidationHelper validationHelper)
        {
            _customerRepository = customerRepository;
            _validationHelper = validationHelper;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var error = await _validationHelper.ValidateAsync(request);

            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);

            var existingCustomer = await _customerRepository.GetById(request.Id);

            if (existingCustomer == null)
                return new ServiceResponse<bool>(false);

            await _customerRepository.Delete(existingCustomer);

            return new ServiceResponse<bool>(true);
        }
    }
}
