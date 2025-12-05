using AutoMapper;
using NewCarRental.Application.Dtos.Categories;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDetailDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
        }
    }
}
