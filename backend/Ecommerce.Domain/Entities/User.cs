using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Position { get; set; } = null!;

    public DateTime? CreateDate { get; set; }
}
