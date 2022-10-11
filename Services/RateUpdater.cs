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
                var accounts = new List<Account>();

                while (!token.IsCancellationRequested)
                {               
                    foreach (var client in clients)
                    {                      
                        foreach (var account in client.Accounts)
                        {
                            account.Amount += account.Amount + 100;
                            accounts.Add(account);
                        }
                    }
                    Task.Delay(5000).Wait();
                }

                foreach (var acc in accounts)
                    service.UpdateAccount(acc);
            });
        }
    }
}
