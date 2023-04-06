using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BumbleBEEBuy_Pay.Model.DB;

public partial class AdminLogin
{
    [Key]
    public int AdminId { get; set; }

    public string? UserName { get; set; }

    public string? Userpassword { get; set; }
}
