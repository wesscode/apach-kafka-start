namespace MasterKafka.API.Services
{
    public class BillingService : IBillingService
    {
        public async Task<string> CancelTransaction(Guid orderId)
        {
            //implementar cancelamento.


            return "transação cancelada";
        }
    }
}
