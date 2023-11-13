namespace MasterKafka.API.Services
{
    public interface IBillingService
    {
        Task<string> CancelTransaction(Guid orderId);
    }
}
