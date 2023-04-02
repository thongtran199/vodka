using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VodkaEntities;

public class Category
{
    [Key]
    public string? CatId { get; set; }

    public string? CatName { get; set; }

    public string? Descript { get; set; }

    public string? IsActive { get; set; }
    public ICollection<Product>? Product { get; set; }
}
