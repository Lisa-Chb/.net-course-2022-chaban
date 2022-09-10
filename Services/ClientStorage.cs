using Models;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientStorage
    {
        readonly Dictionary<Client, List<Account>> clientsDict = new Dictionary<Client, List<Account>>();
        public void AddNewClient(Client client)
        {
            if (clientsDict.ContainsKey(client))
                throw new ClientAlreadyExistException("Данный клиент уже существует");

            var currency = new Currency();
            currency.Name = "USD";

            var account = new Account();
            account.Currency = currency;

            var newAcccountList = new List<Account>();
            newAcccountList.Add(account);

            clientsDict.Add(client, newAcccountList);
        }

        public Dictionary<Client, List<Account>> GetDictionary()
        {
            return clientsDict;
        }
    }
}
