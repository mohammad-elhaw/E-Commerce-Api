using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.Dtos.OrderModuleDto;
using Shared.ErrorModels;

namespace Presentation;

[Authorize]
public class OrderController(IOrderService orderService) : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<OrderForResponseDto>> CreateOrder(OrderForRequestDto orderDto)
    {
        var result = await orderService.CreateOrder(orderDto, GetEmailFromToken());
        if (result.IsFailure)
            return BadRequest(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status400BadRequest
            });
        return Ok(result.Value);
    }

    [HttpGet("DeliveryMethods")]
    public async Task<ActionResult<List<DeliveryMethodsDto>>> GetDeliveryMethods()
    {
        var result = await orderService.GetDeliveryMethods();
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderForResponseDto>>> GetOrders()
    {
        var result = await orderService.GetOrders(GetEmailFromToken());
        return Ok(result);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<ActionResult<OrderForResponseDto>> GetOrder(Guid orderId)
    {
        var result = await orderService.GetOrder(orderId);
        if (result.IsFailure)
            return BadRequest(new ErrorDetails
            {
                Message = result.Error,
                StatusCode = StatusCodes.Status400BadRequest
            });
        return Ok(result.Value);
    }
}
