using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NewCarRental.Application.Dtos.Categories;
using NewCarRental.Application.Exceptions;
using NewCarRental.Application.Interfaces.Repositories;

namespace NewCarRental.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDetailDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDetailDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            if (category == null) throw new NotFoundException("Category", request.Id);
            var data = _mapper.Map<CategoryDetailDto>(category);
            return data;
        }
    }
}
