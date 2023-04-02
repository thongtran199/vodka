using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodkaEntities;

public class Transactdetail
{
    [Key]
    public string? TransactDetailId { get; set; }

    public string? CostEach { get; set; }

    public string? Tax1 { get; set; }

    public string? Tax2 { get; set; }

    public string? Tax3 { get; set; }

    public string? Total { get; set; }

    public int? Quan { get; set; }

    public string? Status { get; set; }

    [ForeignKey("Transactheader")]
    public string? TransactId { get; set; }
    public Transactheader? Transactheader { get; set; }

    [ForeignKey("Product")]
    public string? ProductNum { get; set; }
    public Product? Product { get; set; }
}
