using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace VodkaEntities;

public class Taxinfo
{
    [Key]
    public string? TaxId { get; set; }

    public string? Descript { get; set; }

    public decimal Rate { get; set; }
}
