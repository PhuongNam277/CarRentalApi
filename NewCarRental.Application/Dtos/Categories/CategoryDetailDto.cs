using System.ComponentModel.DataAnnotations;

namespace NewCarRental.Application.Dtos.Categories
{
    public class CategoryDetailDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string VehicleType {  get; set; } = string.Empty;
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
        
    }
}
