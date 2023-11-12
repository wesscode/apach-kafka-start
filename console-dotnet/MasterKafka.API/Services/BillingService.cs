namespace MasterKafka.API.Services
{
    public class BillingService : IBillingService
    {
        public async Task<string> CancelTransaction(int orderId)
        {
            //implementar cancelamento.


            return "transação cancelada";
        }
    }
}
