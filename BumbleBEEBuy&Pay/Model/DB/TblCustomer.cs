using System;
using System.Collections.Generic;

namespace BumbleBEEBuy_Pay.Model.DB;

public partial class TblCustomer
{
    public int CusId { get; set; }

    public string? CusFirstName { get; set; }

    public string? CusLastName { get; set; }

    public DateTime? CusDateofBirth { get; set; }

    public string? CusEmail { get; set; }

    public string? CusGender { get; set; }

    public string? CusNic { get; set; }

    public string? CusMobileNo { get; set; }

    public string? CusPassword { get; set; }

    public DateTime? CusRegistrationDate { get; set; }

    public bool? CusIsActive { get; set; }
}
