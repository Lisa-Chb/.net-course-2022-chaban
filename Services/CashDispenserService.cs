using Models;
using Services.Exceptions;

namespace Services
{
    public class CashDispenserService
    {
        public async Task CashingOut(Guid accountId)
        {
            var clientService = new ClientService();
            var account = await clientService.GetAccountAsync(accountId);

            for (int i = 1; i <= 10; i++)
            {
                if (account.Amount >= 10)
                {
                    account.Amount -= 10;

                    await clientService.UpdateAccountAsync(account);
                }
                else
                    new InsufficientFundsInAccountException("Недостаточно средств на счете");
            }
            Task.Delay(5000).Wait();
        }
    }
}
