using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
