using FluentValidation;
using MediatR;
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

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetById(request.Id);

            if (existingOrder == null)
                return new ServiceResponse<bool>(false);

            await _orderRepository.Delete(request.Id);

            return new ServiceResponse<bool>(true);
        }
    }
}
