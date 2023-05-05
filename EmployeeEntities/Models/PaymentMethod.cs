using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace VodkaEntities;

public class Paymentmethod
{
    [Key]
    public string? PaymentMethodId { get; set; }

    public string? Name { get; set; }

    public string? Descript { get; set; }
}
