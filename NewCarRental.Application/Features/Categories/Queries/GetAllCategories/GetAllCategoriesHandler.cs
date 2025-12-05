using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NewCarRental.Application.Dtos.Categories;
using NewCarRental.Application.Interfaces.Repositories;

namespace NewCarRental.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDetailDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        // Tiêm Repository và AutoMapper vào
        public GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        
        public async Task<List<CategoryDetailDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            // B1: Gọi Repository lấy dữ liệu thô
            var categoryList = await _categoryRepository.GetCategoriesAsync();
            // B2: Map sang DTO
            var data = _mapper.Map<List<CategoryDetailDto>>(categoryList);
            // B3: Trả về data
            return data;
        }
    }
}
