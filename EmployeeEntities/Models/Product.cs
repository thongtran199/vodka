using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodkaEntities;

public class Product
{
    [Key]
    public string? ProductId { get; set; }

    public string? Name { get; set; }

    public string? Descript { get; set; }

    public decimal? Price { get; set; }

    public int? Quan { get; set; }

    public int? IsActive { get; set; }

    public string? ImageSource { get; set; }
    public ICollection<Transactdetail>? TransactDetails { get; set; }

    [ForeignKey("CategoryId")]
    public string? CategoryId { get; set; }
    public VodkaEntities.Category? Category { get; set; }
}
