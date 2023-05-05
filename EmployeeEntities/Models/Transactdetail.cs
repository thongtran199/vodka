using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodkaEntities;

public class Transactdetail
{
    [Key]
    public string? TransactDetailId { get; set; }

    public decimal? CostEach { get; set; }

    public decimal? Total { get; set; }

    public int? Quan { get; set; }

    [ForeignKey("Transactheader")]
    public string? TransactHeaderId { get; set; }
    public Transactheader? TransactHeader { get; set; }

    [ForeignKey("Product")]
    public string? ProductId { get; set; }
    public Product? Product { get; set; }
}
