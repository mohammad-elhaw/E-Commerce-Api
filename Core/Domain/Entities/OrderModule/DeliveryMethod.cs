﻿namespace Domain.Entities.OrderModule;
public class DeliveryMethod : BaseEntity<int>
{
    public string ShortName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public decimal Price { get; set; }
}
