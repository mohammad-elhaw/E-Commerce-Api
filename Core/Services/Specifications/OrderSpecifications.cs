using Domain.Contracts;
using Domain.Entities.OrderModule;
using System.Linq.Expressions;

namespace Services.Specifications;
public class OrderSpecifications : Specifications<Order>
{
    public OrderSpecifications(Expression<Func<Order, bool>>? criteria) : base(criteria)
    {
        AddInclude(o => o.DeliveryMethod);
        AddInclude(o => o.items);
        SetOrderByDescending(o => o.OrderDate);
    }
}
