using Bogus;
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

        public async Task<Client> GetClientAsync(Guid clientId)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);          

            if (client == null)
                throw new PersonDoesntExistException("Указанного клиента не сущетсвует");

            return ClientMapping(client);
        }

        public async Task<List<Client>> GetClientsAsync(ClientFilter filter)
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

            var paginatedClientsQuery = clientsDb.Skip(filter.Page - 1).Take(filter.PageSize);
            var paginatedClientsData = await paginatedClientsQuery.ToListAsync();

            var clients = new List<Client>();

            foreach (var client in paginatedClientsQuery)
            {
                clients.Add(ClientMapping(client));
            }

            return clients;
        }

        public async Task AddClientAsync(Client client)
        {
                var clientDb = ClientMapping(client);

                if (_dbContext.Clients.Contains(clientDb))
                    throw new PersonAlreadyExistException("Данный клиент уже существует");

                if (((DateTime.Now - clientDb.DateOfBirth).Days / 365) < 18)
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
                await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            var priorClient = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == client.ClientId);

            if (!_dbContext.Clients.Contains(priorClient))
                throw new PersonAlreadyExistException("Данного клиента не существует");

            priorClient.FirstName = client.FirstName;
            priorClient.LastName = client.LastName;
            priorClient.NumberOfPassport = client.NumberOfPassport;
            priorClient.SeriesOfPassport = client.SeriesOfPassport;
            priorClient.Phone = client.Phone;
            priorClient.DateOfBirth = client.DateOfBirth;
            priorClient.Accounts = ClientMapping(client).Accounts;
            priorClient.BonusDiscount = client.BonusDiscount;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Guid clientId)
        {
            var requiredClient = await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);

            if (requiredClient == null)
                throw new PersonDoesntExistException("Указанного клиента не сущетсвует");
            else
                 _dbContext.Clients.Remove(requiredClient);

           await _dbContext.SaveChangesAsync();
        }

        public async Task<Account> GetAccountAsync(Guid accountId)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (account == null)
                throw new AccountDoesntExistException("Указанного аккаунта не сущетсвует");

            return AccountMapping(account);
        }

        public async Task AddAccountAsync(Account account)
        {         
            if (account.Clientid == Guid.Empty)
                throw new AccountDoesntExistException("Данный аккаунт не привязан ни к одному клиенту");

            _dbContext.Accounts.Add(AccountMapping(account));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid accountId)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (account == null)
                throw new AccountDoesntExistException("Указанного аккаунта не сущетсвует");
            else
                _dbContext.Accounts.Remove(account);
      
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {      
            var priorAccount = await _dbContext.Accounts.FirstAsync(c => c.AccountId == account.AccountId);
            var accountOwner = await _dbContext.Clients.FirstAsync(c => c.ClientId == priorAccount.Clientid);

            if (!accountOwner.Accounts.Select(x => x.Clientid).Contains(priorAccount.Clientid))
                throw new PersonAlreadyExistException("Данного аккаунта не существует");

            priorAccount.Amount = account.Amount;

            await _dbContext.SaveChangesAsync();
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
                DateOfBirth = client.DateOfBirth,
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
                DateOfBirth = client.DateOfBirth,
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
   


