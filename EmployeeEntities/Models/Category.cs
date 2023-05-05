using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VodkaEntities;

public class Category
{
    [Key]
    public string? CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Descript { get; set; }

    public int? IsActive { get; set; }
    public ICollection<Product>? Products { get; set; }
}
