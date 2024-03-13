using FluentValidation;
using MediatR;
using Order.API.Application.Helpers;
using Order.API.Application.Interfaces.Repository;
using Order.API.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.API.Application.Features.Commands
{
    public class ChangeStatusCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }

    public class ChangeStatusCommandValidator : AbstractValidator<ChangeStatusCommand>
    {
        public ChangeStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sipariş id boş olamaz.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Durum boş olamaz.");
        }
    }

    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, ServiceResponse<bool>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidationHelper _validationHelper;

        public ChangeStatusCommandHandler(IOrderRepository orderRepository, IValidationHelper validationHelper)
        {
            _orderRepository = orderRepository;
            _validationHelper = validationHelper;
        }

        public async Task<ServiceResponse<bool>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
        {
            var error = await _validationHelper.ValidateAsync(request);

            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);

            var order = await _orderRepository.GetById(request.Id);

            if (order == null)
                return new ServiceResponse<bool>(false);

            order.Status = request.Status;

            await _orderRepository.Update(order);

            return new ServiceResponse<bool>(true);
        }
    }

}
