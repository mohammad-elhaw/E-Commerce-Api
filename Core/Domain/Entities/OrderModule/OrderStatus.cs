namespace Domain.Entities.OrderModule;
public enum OrderStatus: byte
{
    Pending,
    PaymentReceived,
    PaymentFailed,
    Shipped,
    Delivered,
    Cancelled,
}
