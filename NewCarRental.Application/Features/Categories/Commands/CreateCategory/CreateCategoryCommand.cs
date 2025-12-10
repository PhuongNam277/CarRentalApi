using MediatR;

namespace NewCarRental.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<int>
    {
        public string CategoryName { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public int? ParentCategoryId { get; set; } = null;
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
    }
}
