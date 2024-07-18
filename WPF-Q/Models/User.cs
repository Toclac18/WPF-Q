using System;
using System.Collections.Generic;

namespace WPF_Q.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public int Role { get; set; } = 0;

    public virtual ICollection<UserTakeTest> UserTakeTests { get; set; } = new List<UserTakeTest>();
}
