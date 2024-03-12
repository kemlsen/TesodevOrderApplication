using Customer.API.Application.Helpers;
using Customer.API.Application.Interfaces.Repository;
using Customer.API.Application.Wrappers;
using Customer.API.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.API.Application.Features.Commands
{
    public class UpdateCustomerCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Müşteri Kimliği boş olamaz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Müşteri adı boş olamaz.")
                .MinimumLength(3).WithMessage("Müşteri adı 3 karakterden fazla olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Doğru email formatı olmalıdır");
        }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ServiceResponse<bool>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidationHelper _validationHelper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IValidationHelper validationHelper)
        {
            _customerRepository = customerRepository;
            _validationHelper = validationHelper;
        }

        public async Task<ServiceResponse<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var error = await _validationHelper.ValidateAsync(request);

            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);

            var existingCustomer = await _customerRepository.GetById(request.Id);

            if (existingCustomer == null)
                return new ServiceResponse<bool>(false);

            existingCustomer.Name = request.Name;
            existingCustomer.Email = request.Email;
            existingCustomer.Address = request.Address;

            await _customerRepository.Update(existingCustomer);

            return new ServiceResponse<bool>(true);
        }
    }
}
