using AutoMapper;
using Project.Models;
using System.Runtime.InteropServices;

namespace Project.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Meal, MealDto>().ReverseMap();
            CreateMap<UserMeal, UserMealDto>().ReverseMap();
            CreateMap<MealProduct, MealProductDto>().ReverseMap();
        }
    }
}
