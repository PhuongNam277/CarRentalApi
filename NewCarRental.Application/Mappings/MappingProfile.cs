using AutoMapper;
using NewCarRental.Application.Dtos.Categories;
using NewCarRental.Application.Dtos.Users;
using NewCarRental.Application.Features.Categories.Commands.CreateCategory;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDetailDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            //CreateMap<CreateCategoryCommand, Category>();

            // User
            CreateMap<User, UserDetailDto>().ReverseMap();
        }
    }
}
