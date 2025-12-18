using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NewCarRental.Application.Exceptions;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category
            {
                CategoryName = request.CategoryName,
                VehicleType = request.VehicleType,
                SortOrder = request.SortOrder,
                ParentCategoryId = request.ParentCategoryId,
                Slug = request.Slug,
                IsActive = request.IsActive
            };
            var result = await _categoryRepository.AddCategoryAsync(newCategory);
            if (result == null) throw new ValidationException("Lỗi trong quá trình thêm mới Category");
            return newCategory.CategoryId;
        }
    }
}
