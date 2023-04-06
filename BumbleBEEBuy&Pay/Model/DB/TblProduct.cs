using System;
using System.Collections.Generic;

namespace BumbleBEEBuy_Pay.Model.DB;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string? Category { get; set; }

    public string? ProductName { get; set; }

    public string? ProductBrand { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? CusId { get; set; }

    public int? CategoryId { get; set; }
}
