using System;
using System.Collections.Generic;

namespace WPF_Q.Models;

public partial class UserTakeTest
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Answer { get; set; }

    public DateTime? TakenDate { get; set; }

    public virtual User User { get; set; } = null!;

}
