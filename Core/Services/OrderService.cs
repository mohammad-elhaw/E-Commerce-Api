using Domain.Common;
using Domain.Contracts;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Services.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModuleDto;

namespace Services;
public class OrderService(
    ICartRepository cartRepository,
    IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Result<OrderForResponseDto>> CreateOrder(OrderForRequestDto orderDto, string email)
    {
        var orderAddress = new OrderAddress
        {
            City = orderDto.Address.City,
            Country = orderDto.Address.Country,
            Street = orderDto.Address.Street,
        };

        var cart = await cartRepository.GetCart(orderDto.CartId);
        if (cart is null) 
            return Result<OrderForResponseDto>
                .Failure($"Cart with id: {orderDto.CartId} is not Exist");

        List<OrderItem> orderItems = [];

        var productRepo = unitOfWork.GetRepository<Product, int>();

        foreach(var item in cart.Items)
        {
            var product = await productRepo.GetByIdAsync(item.Id);
            if (product is null)
                return Result<OrderForResponseDto>
                    .Failure($"Product with id: {item.Id} is not Exist");

            orderItems.Add(new OrderItem
            {
                Price = product.Price,
                Product = new ProductItemOrdered
                {
                    ProductId = product.Id,
                    PictureUrl = product.PictureUrl,
                    ProductName = product.Name
                },
                Quantity = item.Quantity,
            });
        }

        if (orderItems.Count == 0)
            return Result<OrderForResponseDto>
                .Failure($"Order items are empty");

        var deliveryMethod = await unitOfWork
            .GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);

        if(deliveryMethod is null)
            return Result<OrderForResponseDto>
                .Failure($"Delivery method with id: {orderDto.DeliveryMethodId} is not Exist");

        var order = new Order
        {
            UserEmail = email,
            Address = orderAddress,
            DeliveryMethodId = orderDto.DeliveryMethodId,
            items = orderItems,
            DeliveryMethod = deliveryMethod,
            SubTotal = orderItems.Sum(i => i.Price * i.Quantity)
        };

        await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
        int result = await unitOfWork.SaveChangesAsync();

        if (result == 0)
            return Result<OrderForResponseDto>
                .Failure("Failed to create order");

        return Result<OrderForResponseDto>
            .Success(new OrderForResponseDto
            {
                Id = order.Id,
                UserEmail = order.UserEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                Address = new OrderAddressDto
                {
                    City = order.Address.City,
                    Country = order.Address.Country,
                    Street = order.Address.Street,
                },
                DeliveryMethodName = deliveryMethod.ShortName,
                items = order.items.Select(i => new OrderItemDto
                (
                    ProductName: i.Product.ProductName,
                    PictureUrl: i.Product.PictureUrl,
                    Price : i.Price,
                    Quantity : i.Quantity
                )).ToList(),
                SubTotal = order.SubTotal,
                Total = order.GetTotal()
            });
    }

    public async Task<List<DeliveryMethodsDto>> GetDeliveryMethods()
    {
        var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>()
            .GetAllAsync(withTrack: false);

        List<DeliveryMethodsDto> deliveryMethodsToReturn = [];
        foreach (var deliveryMethod in deliveryMethods)
        {
            deliveryMethodsToReturn.Add(new DeliveryMethodsDto
            (
                ShortName: deliveryMethod.ShortName,
                DeliveryTime: deliveryMethod.DeliveryTime,
                Description: deliveryMethod.Description,
                Price: deliveryMethod.Price
            ));
        }
        return deliveryMethodsToReturn;
    }
    public async Task<List<OrderForResponseDto>> GetOrders(string email)
    {
        var orders = await unitOfWork.GetRepository<Order, Guid>()
            .GetAllAsync(new OrderSpecifications(o => o.UserEmail == email));

        List<OrderForResponseDto> ordersToReturn = [];

        foreach (var order in orders)
        {
            ordersToReturn.Add(new OrderForResponseDto
            {
                Address = new OrderAddressDto
                {
                    City = order.Address.City,
                    Country = order.Address.Country,
                    Street = order.Address.Street,
                },
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                SubTotal = order.SubTotal,
                Total = order.GetTotal(),
                DeliveryMethodName = order.DeliveryMethod.ShortName,
                UserEmail = order.UserEmail,
                items = order.items.Select(i => new OrderItemDto
                (
                    ProductName: i.Product.ProductName,
                    Price: i.Price,
                    Quantity: i.Quantity,
                    PictureUrl: i.Product.PictureUrl
                )).ToList()
            });
        }
        return ordersToReturn;
    }

    public async Task<Result<OrderForResponseDto>> GetOrder(Guid orderId)
    {
        var order = await unitOfWork.GetRepository<Order, Guid>()
            .GetByIdAsync(new OrderSpecifications(o => o.Id == orderId));

        if (order is null)
            return Result<OrderForResponseDto>
                .Failure($"Order with id: {orderId} is not Exist");

        return Result<OrderForResponseDto>.Success(new OrderForResponseDto
        {
            Address = new OrderAddressDto
            {
                City = order.Address.City,
                Country = order.Address.Country,
                Street = order.Address.Street,
            },
            Id = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status.ToString(),
            SubTotal = order.SubTotal,
            Total = order.GetTotal(),
            DeliveryMethodName = order.DeliveryMethod.ShortName,
            UserEmail = order.UserEmail,
            items = order.items.Select(i => new OrderItemDto
            (
                ProductName: i.Product.ProductName,
                Price: i.Price,
                Quantity: i.Quantity,
                PictureUrl: i.Product.PictureUrl
            )).ToList()
        });
    }

}
