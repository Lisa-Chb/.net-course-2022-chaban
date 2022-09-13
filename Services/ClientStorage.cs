using Models;
using Services.Exceptions;
using Services.Storages;
using System.Runtime.Intrinsics.X86;

namespace Services
{
    public class ClientStorage : IClientStorage
    {
        public Dictionary<Client, List<Account>> Data { get; }

        public ClientStorage(Dictionary<Client, List<Account>> initData)
        {
            Data = initData;
        }

        public void Add(Client client)
        {
            if (Data.ContainsKey(client))
                throw new PersonAlreadyExistException("Данный клиент уже существует");

            var currency = new Currency();
            currency.Name = "USD";

            var account = new Account();
            account.Currency = currency;
            var newAcccountList = new List<Account>();
            newAcccountList.Add(account);

            Data.Add(client, newAcccountList);
        }

        public void Update(Client client)
        {
            var clientToUpdate = Data.FirstOrDefault(s => s.Key.NumberOfPassport == client.NumberOfPassport).Key;
            var clientAccounts = Data.FirstOrDefault(s => s.Key.NumberOfPassport == client.NumberOfPassport).Value;

            if (Data.ContainsKey(clientToUpdate))
            {
                Data.Remove(clientToUpdate);
                Data.Add(client, clientAccounts);             
            }
            else
                throw new PersonDoesntExistException("Указанный клиент не существует");
        }

        public void Delete(Client client)
        {
            if (Data.ContainsKey(client))
                Data.Remove(client);

            else
                throw new PersonDoesntExistException("Указанного клиента не сущетсвует");
        }

        public void AddAccount(Client client, Account account)
        {
            if (Data.ContainsKey(client))
                throw new PersonAlreadyExistException("Указанный клиент уже существует");

            if (Data[client].Contains(account))
                throw new AccountAlreadyExistException("Клиент уже содержит указанный аккаунт");

            Data[client].Add(account);
        }

        public void UpdateAccount(Client client, Account account)
        {
            var containsAccount = Data[client].Any(s => s.Currency.Name == account.Currency.Name);
            var accountToUpdate = Data[client].FirstOrDefault(s => s.Currency.Name == account.Currency.Name);

            if (!Data.ContainsKey(client))
                throw new PersonDoesntExistException("Указанного клиента не существует");

            if (!containsAccount)
                throw new AccountDoesntExistException("Клиент не имеет указанного аккаунта");

             Data[client].Remove(accountToUpdate);
             Data[client].Add(account);           
        }

        public void DeleteAccount(Client client, Account account)
        {         

            if (!Data.ContainsKey(client))
                throw new PersonDoesntExistException("Указанного клиента не существует");

            if (!Data[client].Contains(account))
                throw new AccountDoesntExistException("Клиент не имеет указанного аккаунта");

            Data[client].Remove(account);
        }
    }
}
