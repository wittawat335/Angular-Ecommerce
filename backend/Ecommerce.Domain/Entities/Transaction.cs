using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Entities;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Discount { get; set; }

    public decimal ShippingCost { get; set; }

    public decimal TaxPercent { get; set; }

    public decimal Total { get; set; }

    public decimal Paid { get; set; }

    public decimal Change { get; set; }

    public string OrderList { get; set; } = null!;

    public string PaymentType { get; set; } = null!;

    public string? PaymentDetail { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public string? Comment { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
