using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NewCarRental.Application.Dtos.Categories;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Domain.Entities;

namespace NewCarRental.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            
            //_mapper.Map<CategoryCreateDto>(request);
            var oldCategory = await _categoryRepository.GetCategoryByIdAsync(request.Id);
            if (oldCategory == null)
            {
                throw new Exception();
            }

            oldCategory.CategoryName = request.CategoryName;
            oldCategory.ParentCategoryId = request.ParentCategoryId;
            oldCategory.VehicleType = request.VehicleType;
            oldCategory.SortOrder = request.SortOrder;
            oldCategory.IsActive = request.IsActive;
            oldCategory.Slug = request.Slug;
            await _categoryRepository.UpdateCategoryAsync(oldCategory);

            return Unit.Value;
        }
    }
}
