namespace Domain.Contracts
{
    public interface IDataSeeding
    {
        Task DataSeed();
        Task IdentityDataSeed();
    }
}
