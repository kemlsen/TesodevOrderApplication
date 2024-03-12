using AutoMapper;
using Customer.API.Application.Dto;
using Customer.API.Application.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Domain.Entities.Customer, GetCustomerDto>().ReverseMap();
            CreateMap<Domain.Entities.Address, AddressDto>().ReverseMap();
        }
    }
}
