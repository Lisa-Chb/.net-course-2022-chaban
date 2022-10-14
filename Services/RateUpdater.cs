using Models;
using Services.Filtres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RateUpdater
    {
        public async Task StartAccrualAsync(CancellationToken token, ClientService service, ClientFilter filter)
        {
            var clients = await service.GetClientsAsync(filter);

            while (!token.IsCancellationRequested)
            {
                foreach (var client in clients)
                {
                    if (client.Accounts == null)
                        continue;

                    foreach (var account in client.Accounts)
                    {
                        account.Amount += account.Amount + 100;
                        await service.UpdateAccountAsync(account);
                    }
                }
                Task.Delay(5000).Wait();
            }
        }
    }
}
