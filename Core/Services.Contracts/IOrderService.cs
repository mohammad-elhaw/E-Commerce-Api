using Domain.Common;
using Shared.Dtos.OrderModuleDto;

namespace Services.Contracts;
public interface IOrderService
{
    Task<Result<OrderForResponseDto>> CreateOrder(OrderForRequestDto orderDto, string email);
    Task<List<DeliveryMethodsDto>> GetDeliveryMethods();
    Task<List<OrderForResponseDto>> GetOrders(string email);
    Task<Result<OrderForResponseDto>> GetOrder(Guid orderId);
}
