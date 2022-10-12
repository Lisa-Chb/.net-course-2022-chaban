using Models;
using Services.Exceptions;

namespace Services
{
    public class CashDispenserService
    {
        public Task CashingOut(Guid accountId)
        {
            return Task.Factory.StartNew(() =>
             {
                 var clientService = new ClientService();
                 var account = clientService.GetAccount(accountId);

                 for (int i = 1; i <= 10; i++)
                 {
                     if (account.Amount >= 10)
                     {
                         account.Amount -= 10;
                         Task.Delay(500).Wait();
                         clientService.UpdateAccount(account);
                     }
                     else
                         new InsufficientFundsInAccountException("Недостаточно средств на счете");
                 }
             });
        }
    }
}
