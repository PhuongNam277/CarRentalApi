using System;
using System.Collections.Generic;

namespace NewCarRental.Domain.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string VehicleType { get; set; } = null!;

    public int? ParentCategoryId { get; set; }

    public string? Slug { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }

    public virtual ICollection<TenantCategory> TenantCategories { get; set; } = new List<TenantCategory>();
}
