using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace VodkaEntities;

public class Paymentmethod
{
    [Key]
    public string? PaymentId { get; set; }

    public string? PaymentName { get; set; }

    public string? Descript { get; set; }
}
