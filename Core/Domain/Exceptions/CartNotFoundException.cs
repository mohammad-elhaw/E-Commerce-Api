namespace Domain.Exceptions
{
    public sealed class CartNotFoundException(string id) : 
        NotFoundException($"Cart Not Found With id: {id}")
    {

    }
}
