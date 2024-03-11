using AutoMapper;
using Order.API.Application.Dto;
using Order.API.Application.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.API.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Domain.Entities.Order, GetOrderDto>().ReverseMap();
            CreateMap<Domain.Entities.Customer, CustomerDto>().ReverseMap();
            CreateMap<Domain.Entities.Address, AddressDto>().ReverseMap();
            CreateMap<Domain.Entities.Product, ProductDto>().ReverseMap();
        }
    }
}
