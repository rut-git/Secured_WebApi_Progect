using AutoMapper;
using Entities;
using DTOs;
namespace MyFirstWebApiSite
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, productDTO>().ForMember(dest => dest.CategoryName,opts => opts.MapFrom(src => src.Category.CategoryName)).ReverseMap();

           // CreateMap<Order, orderDTO>().ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.Firstname)).ReverseMap();
            CreateMap<Order, orderDTO>().ReverseMap();


            CreateMap<OrderItem, orderItemDTO>().ReverseMap();

            CreateMap<Category, categoryDTO>().ReverseMap();
            
            CreateMap<User, userDTO>().ReverseMap();

            CreateMap<UserLogin, userLoginDTO>().ReverseMap();
        }
    }
}
