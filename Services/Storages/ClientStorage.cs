using Models;
using Services.Exceptions;
using Services.Storages;
using System.Runtime.Intrinsics.X86;

namespace Services
{
    public class ClientStorage : IClientStorage
    {
        public Dictionary<Client, List<Account>> Data { get; }

        public ClientStorage()
        {
            Data = new Dictionary<Client, List<Account>>();
        }

        public void Add(Client client)
        {
            
            var account = new Account();
            var currency = new Currency();
            currency.Name = "USD";
            account.Currency = currency;
            var newAcccountList = new List<Account>();
            newAcccountList.Add(account);

            Data.Add(client, newAcccountList);
        }

        public void Update(Client client)
        {
            var clientToUpdate = Data.FirstOrDefault(s => s.Key.NumberOfPassport == client.NumberOfPassport).Key;
            var clientAccounts = Data[clientToUpdate];

            Data.Remove(clientToUpdate);
            Data.Add(client, clientAccounts);

        }

        public void Delete(Client client)
        {           
            Data.Remove(client);         
        }

        public void AddAccount(Client client, Account account)
        {
            Data[client].Add(account);
        }

        public void UpdateAccount(Client client, Account account)
        {           
          var accountToUpdate = Data[client].FirstOrDefault(s => s.Currency.Name == account.Currency.Name);

            Data[client].Remove(accountToUpdate);
            Data[client].Add(account);
        }

        public void DeleteAccount(Client client, Account account)
        {                   
            Data[client].Remove(account);
        }
    }
}
