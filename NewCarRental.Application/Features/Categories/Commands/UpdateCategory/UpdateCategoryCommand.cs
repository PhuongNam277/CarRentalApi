using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace NewCarRental.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public int? ParentCategoryId { get; set; } = null;
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
    }
}
