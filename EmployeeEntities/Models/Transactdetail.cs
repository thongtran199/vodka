﻿using System;
using System.Collections.Generic;

namespace VodkaEntities;

public class Transactdetail
{
    public string? TransactDetailId { get; set; }

    public string? ProductNum { get; set; }

    public string? CostEach { get; set; }

    public string? Tax1 { get; set; }

    public string? Tax2 { get; set; }

    public string? Tax3 { get; set; }

    public string? Total { get; set; }

    public int? Quan { get; set; }

    public string? Status { get; set; }

    public string? TransactId { get; set; }
}
