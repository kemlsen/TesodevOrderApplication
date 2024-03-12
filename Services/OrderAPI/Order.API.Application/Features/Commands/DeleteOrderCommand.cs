using FluentValidation;
using MediatR;
using Order.API.Application.Helpers;
using Order.API.Application.Interfaces.Repository;
using Order.API.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.API.Application.Features.Commands
{
    public class DeleteOrderCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sipariş id boş olamaz.");
        }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ServiceResponse<bool>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidationHelper _validationHelper;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IValidationHelper validationHelper)
        {
            _orderRepository = orderRepository;
            _validationHelper = validationHelper;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var error = await _validationHelper.ValidateAsync(request);

            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);

            var existingOrder = await _orderRepository.GetById(request.Id);

            if (existingOrder == null)
                return new ServiceResponse<bool>(false);

            await _orderRepository.Delete(request.Id);

            return new ServiceResponse<bool>(true);
        }
    }
}
