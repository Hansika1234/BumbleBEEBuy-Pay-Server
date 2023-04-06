using System;
using System.Collections.Generic;

namespace BumbleBEEBuy_Pay.Model.DB;

public partial class TblInstallmentDetail
{
    public int InstallId { get; set; }

    public int ProductId { get; set; }

    public int? CusId { get; set; }

    public decimal? LoanBalance { get; set; }

    public decimal? UsedAmount { get; set; }

    public int? InstallmentPlan { get; set; }
}
