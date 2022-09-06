
using Models;
using Models.ModelsValidationExceptions;

namespace Services
{
    public class ClientService
    {
        private Dictionary<Client, List<Account>> _clientsDict = new Dictionary<Client, List<Account>>();

        public void AddNewClient(Client client)
        {
            if (_clientsDict.ContainsKey(client))
                throw new ClientAlreadyExistException("Данный клиент уже существует");

            if (client.Age < 18)
                throw new PersonAgeValidationException("Лицам до 18 регистрация запрещена");

            if (string.IsNullOrEmpty(client.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (client.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            var newAcccountList = new List<Account>();

            var currency = new Currency();
            currency.Name = "USD";

            var account = new Account();
            account.Currency = currency;

            newAcccountList.Add(account);

            _clientsDict.Add(client, newAcccountList);
        }
    }
}

