using AutoMapper;
using Order.Application.DTOs.OrderDTOs;
using Order.Domain.Entities;

namespace Order.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDetail, AddOrderDTO>().ReverseMap();
            CreateMap<OrderDetail, DetailOrderDTO>().ReverseMap();
            CreateMap<OrderDetail, EditOrderDTO>().ReverseMap();
        }
    }
}
