using AutoMapper;
using KitchenDisplaySystem.Models;

namespace KitchenDisplaySystem.DTO.Mapping
{
    public class MappingProfiles : Profile
    { 
        public MappingProfiles()
        {
            CreateMap<Food, FoodDTO>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Waiter, WaiterDTO>();
        }
    }
}
