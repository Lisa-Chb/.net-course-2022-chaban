﻿
using Bogus.DataSets;
using Models;
using Services.Exceptions;
using Services.Filtres;
using Services.Storages;

namespace Services
{
    public class ClientService
    {
        private readonly IClientStorage _clientStorage;
        
        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddNewClient(Client client)
        {
            if (_clientStorage.Data.ContainsKey(client))
                throw new PersonAlreadyExistException("Данный клиент уже существует");

            if (((DateTime.Now - client.DateOfBirth).Days/365) < 18)
                throw new PersonAgeValidationException("Лицам до 18 регистрация запрещена");

            if (string.IsNullOrEmpty(client.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (client.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            _clientStorage.Add(client);            
        }

        public void UpdateClient(Client client)
        {
            var existindClient = _clientStorage.Data.FirstOrDefault(s => s.Key.NumberOfPassport == client.NumberOfPassport).Key;

            if (!_clientStorage.Data.ContainsKey(existindClient))
                    throw new PersonDoesntExistException("Указанный клиент не существует");

            _clientStorage.Update(client);
        }

        public void DeleteClient(Client client)
        {
            if (!_clientStorage.Data.ContainsKey(client))
                throw new PersonDoesntExistException("Указанного клиента не сущетсвует");

            _clientStorage.Delete(client);
        }

        public void AddAccount(Client client, Account account)
        {
            var clientDict = _clientStorage.Data;

            if (clientDict.ContainsKey(client))
                throw new PersonAlreadyExistException("Указанный клиент уже существует");

            if (clientDict[client].Contains(account))
                throw new AccountAlreadyExistException("Клиент уже содержит указанный аккаунт");

            _clientStorage.AddAccount(client, account);
        }

        public void UpdateAccount(Client client, Account account)
        {
            var clientDict = _clientStorage.Data;
            var accounts = clientDict[client];
            var accountExist = clientDict[client].Any(s => s.Currency.Name == account.Currency.Name);
            
            if (!clientDict.ContainsKey(client))
                throw new PersonDoesntExistException("Указанного клиента не существует");

            if (!accountExist)
                throw new AccountDoesntExistException("Клиент не имеет указанного аккаунта");

            _clientStorage.UpdateAccount(client, account);
        }

        public void DeleteAccount(Client client, Account account)
        {
            var clientDict = _clientStorage.Data;

            if (!clientDict.ContainsKey(client))
                throw new PersonDoesntExistException("Указанного клиента не существует");

            if (!clientDict[client].Contains(account))
                throw new AccountDoesntExistException("Клиент не имеет указанного аккаунта");

            _clientStorage.DeleteAccount(client, account);
        }
        public Dictionary<Client, List<Account>> GetClients(ClientFilter filter)
        {
            var clientDict = _clientStorage.Data;

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
   


