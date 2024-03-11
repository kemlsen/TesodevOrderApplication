﻿using FluentValidation;
using MediatR;
using Order.API.Application.Interfaces.Repository;
using Order.API.Application.Wrappers;
using Order.API.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.API.Application.Features.Commands
{
    public class UpdateOrderCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public Address Address { get; set; }
        public Product Product { get; set; }
    }

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Sipariş ID boş olamaz.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Miktar boş olamaz.")
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Fiyat boş olamaz.")
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Durum boş olamaz.");
        }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ServiceResponse<bool>>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ServiceResponse<bool>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetById(request.OrderId);

            if (existingOrder == null)
                return new ServiceResponse<bool>(false);

            existingOrder.Quantity = request.Quantity;
            existingOrder.Price = request.Price;
            existingOrder.Status = request.Status;
            existingOrder.Address = request.Address;
            existingOrder.Product = request.Product;

            await _orderRepository.Update(existingOrder);

            return new ServiceResponse<bool>(true);
        }
    }
}
