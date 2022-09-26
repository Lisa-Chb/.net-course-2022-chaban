
using Microsoft.EntityFrameworkCore;
using ModelsDb;
using ModelsDb.Data;
using Services.Exceptions;
using Services.Filtres;

namespace Services
{
    public class ClientService
    {
        ApplicationContext _dbContext;

        public ClientService()
        {
            _dbContext = new ApplicationContext();
        }

        public ClientDb GetClient(Guid clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);

            if (client == null)
                throw new PersonDoesntExistException("Указанного клиента не сущетсвует");

            return client;
        }

        public List<ClientDb> GetClients(ClientFilter filter)
        {

            var clients = _dbContext.Clients.AsQueryable();

            if (filter.FirstName != null)
                clients = clients.Where(s => s.FirstName == filter.FirstName);

            if (filter.LastName != null)
                clients = clients.Where(s => s.LastName == filter.LastName);

            if (filter.NumberOfPassport != null)
                clients = clients.Where(s => s.NumberOfPassport == filter.NumberOfPassport);

            if (filter.MinDateTime != null)
                clients = clients.Where(s => s.DateOfBirth <= filter.MinDateTime);

            if (filter.MaxDateTime != null)
                clients = clients.Where(s => s.DateOfBirth >= filter.MaxDateTime);

            /* if (filter.BonusDiscount != null)
                 clients = clients.OrderBy(s => s.BonusDiscount);*/

            var paginatedClients = clients.Skip(filter.Page - 1).Take(filter.PageSize).ToList();

            return paginatedClients;
        }

        public void AddClient(ClientDb client)
        {
            if (_dbContext.Clients.Contains(client))
                throw new PersonAlreadyExistException("Данный клиент уже существует");

            if (((DateTime.Now - client.DateOfBirth).Days / 365) < 18)
                throw new PersonAgeValidationException("Лицам до 18 регистрация запрещена");

            if (string.IsNullOrEmpty(client.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (client.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            var newAccount = new AccountDb
            {
                AccountId = Guid.NewGuid(),
                Clientid = client.ClientId
            };

            var currency = new CurrencyDb()
            {
                Name = "RUB",
                CurrencyId = Guid.NewGuid(),
                AccountId = newAccount.AccountId
            };

            _dbContext.Currency.Add(currency);
            _dbContext.Accounts.Add(newAccount);
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        public void UpdateClient(ClientDb client)
        {
            var priorClient = _dbContext.Clients.FirstOrDefault(c => c.ClientId == client.ClientId);

            if (!_dbContext.Clients.Contains(priorClient))
                throw new PersonAlreadyExistException("Данного клиента не существует");

            priorClient.FirstName = client.FirstName;
            priorClient.LastName = client.LastName;
            priorClient.NumberOfPassport = client.NumberOfPassport;
            priorClient.SeriesOfPassport = client.SeriesOfPassport;
            priorClient.Phone = client.Phone;
            priorClient.DateOfBirth = client.DateOfBirth;
            priorClient.Accounts = client.Accounts;
            priorClient.BonusDiscount = client.BonusDiscount;

            _dbContext.SaveChanges();
        }

        public void DeleteClient(Guid clientId)
        {
            var requiredClient = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);

            if (requiredClient == null)
                throw new PersonDoesntExistException("Указанного клиента не сущетсвует");
            else
                _dbContext.Clients.Remove(requiredClient);

            _dbContext.SaveChanges();
        }

        public AccountDb GetAccount(Guid accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == accountId);

            if (account == null)
                throw new AccountDoesntExistException("Указанного аккаунта не сущетсвует");

            return _dbContext.Accounts.FirstOrDefault(c => c.AccountId == accountId);
        }
        public void AddAccount(AccountDb account)
        {
            if (account.Clientid == null)
                throw new AccountDoesntExistException("Данный аккаунт не привязан ни к одному клиенту");

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == accountId);

            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }

        public void UpdateAccount(AccountDb account)
        {
            var priorAccount = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == account.AccountId);
            var accountOwner = _dbContext.Clients.FirstOrDefault(c => c.ClientId == priorAccount.Clientid);

            if (!accountOwner.Accounts.Select(x => x.Clientid).Contains(priorAccount.Clientid))
                throw new PersonAlreadyExistException("Данного аккаунта не существует");

            priorAccount.Currencys = account.Currencys;
            priorAccount.Amount = account.Amount;

            _dbContext.SaveChanges();
        }
    }
}
   


