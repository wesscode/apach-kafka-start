namespace MasterKafka.API.Services
{
    public interface IBillingService
    {
        Task<string> CancelTransaction(int orderId);
    }
}
