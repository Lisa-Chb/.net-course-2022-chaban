using Models;
using Services.Exceptions;

namespace Services
{
    public class CashDispenserService
    {
        public Task CashDispencer(Guid accountId)
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
                         Thread.Sleep(1000);
                     }
                     else
                         new InsufficientFundsInAccountException("Недостаточно средств на счете");
                 }

                 clientService.UpdateAccount(account);
             });
        }
    }
}
