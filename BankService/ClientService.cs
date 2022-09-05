
using Models;
using Models.ModelsValidationExceptions;

namespace Services
{
    public class ClientService
    {   
        private Dictionary<Client, List <Account>> _clientsDict;

        public void AddNewClient(Client client)
        {      
            if (client.Age < 18)
                throw new ClientAgeValidationException("Лицам до 18 регистрация запрещена");

            if (string.IsNullOrEmpty(client.SeriesOfPassport))
                throw new ClientSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (client.NumberOfPassport == null)
                throw new ClientNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            var newAcccountList = new List <Account>();

            var currency = new Currency();
            currency.Name = "USD";

            var account = new Account();
            account.Currency = currency;

            newAcccountList.Add(account);

            _clientsDict.Add(client, newAcccountList);       
        }
    }
}

