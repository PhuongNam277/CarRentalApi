using System.ComponentModel.DataAnnotations;

namespace NewCarRental.Application.Dtos.Categories
{
    public class CategoryCreateDto
    {
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Tên loại xe không được để trống"), MaxLength(20)]
        public string CategoryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Loại phương tiện không được để trống"), MaxLength(20)]
        public string VehicleType { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;
        public int? ParentCategoryId { get; set; } = null;
        public string? Slug { get; set; }

        [Required(ErrorMessage = "Trạng thái xe không được để trống")]
        public bool IsActive { get; set; }
    }
}
