
using Bogus.DataSets;
using ModelsDb;
using ModelsDb.Data;
using Services.Exceptions;
using Services.Filtres;
using Services.Storages;
using Currency = ModelsDb.Currency_db;

namespace Services
{
    public class ClientService
    {
        ApplicationContext _dbContext;

        public ClientService()
        {
            _dbContext = new ApplicationContext();
        }

        public Client_db GetClient(Guid clientId)
        {
            return _dbContext.Clients.FirstOrDefault(c => c.ClientId == clientId);
        }

        public List<Client_db> GetClients(ClientFilter filter)
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

            var paginatedClients = clients.Skip(filter.Page - 1).Take(filter.PageSize).ToList();

            return paginatedClients;
        }
        public void AddClient(Client_db client)
        {
            if (_dbContext.Clients.Contains(client))
                throw new PersonAlreadyExistException("Данный клиент уже существует");

            if (((DateTime.Now - client.DateOfBirth).Days / 365) < 18)
                throw new PersonAgeValidationException("Лицам до 18 регистрация запрещена");

            if (string.IsNullOrEmpty(client.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (client.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            var newAccount = new Account_db
            {
                AccountId = Guid.NewGuid(),
                Clientid = client.ClientId
            };

            var currency = new Currency_db()
            {
                Name = "RUB",
                CurrencyId = Guid.NewGuid(),
                AccountId = newAccount.AccountId
            };

            _dbContext.Currency.Add(currency);
            _dbContext.Accounts.Add(newAccount);
            _dbContext.Clients.Add(client);

            client.Accounts.Add(newAccount);

            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        public void AddClient(List<Client_db> clients)
        {
            foreach (var client in clients)
            {
                if (_dbContext.Clients.Contains(client))
                    throw new PersonAlreadyExistException("Данный клиент уже существует");

                if (((DateTime.Now - client.DateOfBirth).Days / 365) < 18)
                    throw new PersonAgeValidationException("Лицам до 18 регистрация запрещена");

                if (string.IsNullOrEmpty(client.SeriesOfPassport))
                    throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

                if (client.NumberOfPassport == null)
                    throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

                
                var newAccount = new Account_db()
                {
                    AccountId = Guid.NewGuid(),
                    Clientid = client.ClientId
                };


                var currency = new Currency_db()
                {
                    Name = "RUB",
                    CurrencyId = Guid.NewGuid(),
                    AccountId = newAccount.AccountId
                };
                
               // client.Accounts = null;
                _dbContext.Currency.Add(currency);
                _dbContext.Accounts.Add(newAccount);
                _dbContext.Clients.Add(client);
            }
            _dbContext.SaveChanges();
        }

        public void UpdateClient(Client_db client)
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

        public void AddAccount(Guid clientId, Account_db account)
        {
            if ((account.Clientid != Guid.Empty) && (account.Clientid != clientId))
                throw new AccountDoesntExistException("Данный аккаунт привязан к другому клиенту или не существует");

            if (account.Clientid == Guid.Empty)
                account.Clientid = clientId;

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
        }

        public void DeleteAccount(Guid accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == accountId);

            _dbContext.Accounts.Remove(account);
            _dbContext.SaveChanges();
        }

        public void UpdateAccount(Account_db account)
        {
            var priorAccount = _dbContext.Accounts.FirstOrDefault(c => c.AccountId == account.AccountId);
            var accountOwner = _dbContext.Clients.FirstOrDefault(c => c.ClientId == priorAccount.Clientid);

            if (!accountOwner.Accounts.Contains(account))
                throw new PersonAlreadyExistException("Данного аккаунта не существует");

            priorAccount.Currency = account.Currency;
            priorAccount.Amount = account.Amount;

            _dbContext.SaveChanges();
        }
    }
}
   


