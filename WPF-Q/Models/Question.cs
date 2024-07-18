using System;
using System.Collections.Generic;

namespace WPF_Q.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public string? Answer { get; set; }

    public string? CorrectAnswer { get; set; }

    public DateTime? CreatedDate { get; set; }

}
