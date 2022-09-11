
using Models;
using Services.Exceptions;
using Services.Filtres;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;


namespace Services
{
    public class ClientService
    {
        private readonly ClientStorage _clientStorage;

        public ClientService(ClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddNewClient(Client client)
        {
            if (((DateTime.Now - client.DateOfBirth).Days/365) < 18)
                throw new PersonAgeValidationException("Лицам до 18 регистрация запрещена");

            if (string.IsNullOrEmpty(client.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (client.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            _clientStorage.AddNewClient(client);            
        }

        public Dictionary<Client, List<Account>> GetClients(ClientFilter filter)
        {
            var clientDict = _clientStorage.GetDictionary();

            var result = clientDict.AsEnumerable();

            if (filter.FirstName != null)
                result = result.Where(s => s.Key.FirstName == filter.FirstName);

            if (filter.LastName != null)
                result =  result.Where(s => s.Key.LastName == filter.LastName);

            if(filter.NumberOfPassport != null)
                result  = result.Where(s => s.Key.NumberOfPassport == filter.NumberOfPassport);

            if (filter.MinDateTime != null)
                result = result.Where(s => s.Key.DateOfBirth <= filter.MinDateTime);

            if (filter.MaxDateTime != null)
                result = result.Where(s => s.Key.DateOfBirth >= filter.MaxDateTime);

            return new Dictionary<Client, List < Account>>(result);
        }
 
    }
}
   


