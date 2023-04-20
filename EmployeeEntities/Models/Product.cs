using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodkaEntities;

public class Product
{
    [Key]
    public string? ProductNum { get; set; }

    public string? ProductName { get; set; }

    public string? Descript { get; set; }

    public string? Price { get; set; }

    public string? Tax1 { get; set; }

    public string? Tax2 { get; set; }

    public string? Tax3 { get; set; }

    public string? Quan { get; set; }

    public string? IsActive { get; set; }

    public string? ImageSource { get; set; }
    public ICollection<Transactdetail>? Transactdetail { get; set; }

    [ForeignKey("CatId")]
    public string? CatId { get; set; }
    public VodkaEntities.Category? Category { get; set; }
}
