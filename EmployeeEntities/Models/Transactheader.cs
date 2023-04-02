using System;
using System.Collections.Generic;

namespace VodkaEntities;

public class Transactheader
{
    public string? TransactId { get; set; }

    public string? Net { get; set; }

    public string? Tax1 { get; set; }

    public string? Tax2 { get; set; }

    public string? Tax3 { get; set; }

    public string? Total { get; set; }

    public DateTime? TimePayment { get; set; }

    public string? WhoPay { get; set; }

    public string? Status { get; set; }
}
