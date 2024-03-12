using AutoMapper;
using FluentValidation;
using MediatR;
using Order.API.Application.Dto;
using Order.API.Application.Helpers;
using Order.API.Application.Interfaces.Repository;
using Order.API.Application.Wrappers;
using Order.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Order.API.Application.Features.Commands
{
    public class CreateOrderCommand : IRequest<ServiceResponse<Guid>>
    {
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public AddressDto Address { get; set; }
        public ProductDto Product { get; set; }

    }
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Müşteri ID boş olamaz.")
                .Must(id => id != Guid.Empty).WithMessage("Müşteri ID geçersiz.");

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

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ServiceResponse<Guid>>
    {
        IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IValidationHelper _validationHelper;


        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, HttpClient httpClient, IValidationHelper validationHelper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _httpClient = httpClient;
            _validationHelper = validationHelper;
        }

        public async Task<ServiceResponse<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var error = await _validationHelper.ValidateAsync(request);

            if (!string.IsNullOrEmpty(error))
                throw new Exception(error); 

            var isCustomerIdValid = await ValidateCustomerId(request.CustomerId);

            if (!isCustomerIdValid)
                throw new Exception(isCustomerIdValid.ToString());

            var order = _mapper.Map<Domain.Entities.Order>(request);

            await _orderRepository.Create(order);

            return new ServiceResponse<Guid>(order.Id);
        }
        private async Task<bool> ValidateCustomerId(Guid customerId)
        {
            var requestBody = "{\"id\": \"" + customerId.ToString() + "\"}";

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44390/api/Customer/Validate", content);

            string responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(responseContent);
        }
    }

}
