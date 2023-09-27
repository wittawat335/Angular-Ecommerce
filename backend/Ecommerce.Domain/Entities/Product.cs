using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreateDate { get; set; }
}
