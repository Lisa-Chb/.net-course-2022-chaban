using Microsoft.EntityFrameworkCore;
using Models;
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

        public Client GetClient(Guid clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);          

            if (client == null)
                throw new PersonDoesntExistException("Указанного клиента не сущетсвует");

            return ClientMapping(client);
        }

        public List<Client> GetClients(ClientFilter filter)
        {
            var clientsDb = _dbContext.Clients.AsQueryable();

            if (filter.FirstName != null)
                clientsDb = clientsDb.Where(s => s.FirstName == filter.FirstName);

            if (filter.LastName != null)
                clientsDb = clientsDb.Where(s => s.LastName == filter.LastName);

            if (filter.NumberOfPassport != null)
                clientsDb = clientsDb.Where(s => s.NumberOfPassport == filter.NumberOfPassport);

            if (filter.MinDateTime != null)
                clientsDb = clientsDb.Where(s => s.DateOfBirth <= filter.MinDateTime);

            if (filter.MaxDateTime != null)
                clientsDb = clientsDb.Where(s => s.DateOfBirth >= filter.MaxDateTime);

            var paginatedClients = clientsDb.Skip(filter.Page - 1).Take(filter.PageSize).ToList();

            var clients = new List<Client>();
            foreach (var client in paginatedClients)
            {
                clients.Add(ClientMapping(client));
            }

            return clients;
        }

        public void AddClient(Client client)
        {
            var clientDb = ClientMapping(client);

            if (_dbContext.Clients.Contains(clientDb))
                throw new PersonAlreadyExistException("Данный клиент уже существует");

            if (((DateTime.Now.ToUniversalTime() - clientDb.DateOfBirth).Days / 365) < 18)
                throw new PersonAgeValidationException("Лицам до 18 регистрация запрещена");

            if (string.IsNullOrEmpty(clientDb.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (clientDb.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            var newAccount = new AccountDb
            {
                AccountId = Guid.NewGuid(),
                Clientid = clientDb.ClientId,
                CurrencyCode = 643
            };

            _dbContext.Accounts.Add(newAccount);
            _dbContext.Clients.Add(clientDb);
            _dbContext.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            var priorClient = _dbContext.Clients.FirstOrDefault(c => c.ClientId == client.ClientId);

            if (!_dbContext.Clients.Contains(priorClient))
                throw new PersonAlreadyExistException("Данного клиента не существует");

            priorClient.FirstName = client.FirstName;
            priorClient.LastName = client.LastName;
            priorClient.NumberOfPassport = client.NumberOfPassport;
            priorClient.SeriesOfPassport = client.SeriesOfPassport;
            priorClient.Phone = client.Phone;
            priorClient.DateOfBirth = client.DateOfBirth.ToUniversalTime();
            priorClient.Accounts = ClientMapping(client).Accounts;
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

        public Account GetAccount(Guid accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == accountId);

            if (account == null)
                throw new AccountDoesntExistException("Указанного аккаунта не сущетсвует");

            return AccountMapping(account);
        }

        public void AddAccount(Account account)
        {         
            if (account.Clientid == null)
                throw new AccountDoesntExistException("Данный аккаунт не привязан ни к одному клиенту");

            _dbContext.Accounts.Add(AccountMapping(account));
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == accountId);

            if (account == null)
                throw new AccountDoesntExistException("Указанного аккаунта не сущетсвует");
            else
                _dbContext.Accounts.Remove(account);
      
            _dbContext.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {      
            var priorAccount = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == account.AccountId);
            var accountOwner = _dbContext.Clients.FirstOrDefault(c => c.ClientId == priorAccount.Clientid);

            if (!accountOwner.Accounts.Select(x => x.Clientid).Contains(priorAccount.Clientid))
                throw new PersonAlreadyExistException("Данного аккаунта не существует");

            priorAccount.Amount = account.Amount;

            _dbContext.SaveChanges();
        }

        private ClientDb ClientMapping(Client client)
        {
            return new ClientDb()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                NumberOfPassport = client.NumberOfPassport,
                SeriesOfPassport = client.SeriesOfPassport,
                Phone = client.Phone,
                DateOfBirth = client.DateOfBirth.ToUniversalTime(),
                BonusDiscount = client.BonusDiscount,
                ClientId = client.ClientId
            };
        }

        private Client ClientMapping(ClientDb client)
        {
            return new Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                NumberOfPassport = client.NumberOfPassport,
                SeriesOfPassport = client.SeriesOfPassport,
                Phone = client.Phone,
                DateOfBirth = client.DateOfBirth.ToUniversalTime(),
                BonusDiscount = client.BonusDiscount,
                ClientId = client.ClientId
            };
        }

        private AccountDb AccountMapping(Account account)
        {
            return new AccountDb()
            { AccountId = account.AccountId,
              Clientid = account.Clientid,
              CurrencyCode = account.CurrencyCode,
              Amount = account.Amount
            };
        }

        private Account AccountMapping(AccountDb account)
        {
            return new Account()
            {
                AccountId = account.AccountId,
                Clientid = account.Clientid,
                CurrencyCode = account.CurrencyCode,
                Amount = account.Amount
            };
        }
    }
}
   


