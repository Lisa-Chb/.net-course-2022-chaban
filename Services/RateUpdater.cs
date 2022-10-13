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

        public Task StartAccrual(CancellationToken token, ClientService service, ClientFilter filter)
        {
            return Task.Run(() =>
            {
                var clients = service.GetClients(filter);

                while (!token.IsCancellationRequested)
                {
                    foreach (var client in clients)
                    {
                        foreach (var account in client.Accounts)
                        {
                            account.Amount += account.Amount + 100;
                            service.UpdateAccount(account);
                        }
                    }
                    Task.Delay(5000).Wait();
                }
            });
        }
    }
}
