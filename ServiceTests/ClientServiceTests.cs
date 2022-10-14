using Bogus;
using Models;
using Services;
using Services.Exceptions;
using Services.Filtres;
using Xunit;

namespace ServiceTests
{
    public class ClientServiceTests
    {
        [Fact]
        public async Task  AddGetClientTest()
        {
            //Arrange
            var service = new ClientService();

            var testDataGenerator = new TestDataGenerator();
            var generatorClient = testDataGenerator.CreateClientListGenerator();
            var clients = generatorClient.Generate(5);

            var client = new Client
            {
                FirstName = "Александр",
                LastName = "Александров",
                NumberOfPassport = 25499,
                SeriesOfPassport = "876768",
                Phone = "77956734",
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = Guid.NewGuid()
            };

            //Act
            foreach (var c in clients)
            {
                await service.AddClientAsync(c);
            }

            await service.AddClientAsync(client);

            var getClient = await service.GetClientAsync(client.ClientId);

            //Assert
            Assert.Equal(getClient, client);
        }

        [Fact]
        public async Task GetClientsTest()
        {
            //Arrange
            var service = new ClientService();

            var testDataGenerator = new TestDataGenerator();
            var generatorClient = testDataGenerator.CreateClientListGenerator();
            var clients = generatorClient.Generate(5);

            var clientTom = new Client()
            {
                FirstName = "Tom",
                LastName = "Holland",
                Phone = "77768000",
                NumberOfPassport = 9854,
                SeriesOfPassport = "657755",
                ClientId = Guid.NewGuid(),
                BonusDiscount = 0,
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime()
            };

            var filter = new ClientFilter
            {
                FirstName = "Tom",
                PageSize = 1
            };

            //Act
            foreach (var c in clients)
            {
                await service.AddClientAsync(c);
            }
            await service.AddClientAsync(clientTom);

            //Assert
            var clientsCount = await service.GetClientsAsync(filter);
            var clientsName = await service.GetClientsAsync(filter);

            Assert.True(clientsCount.Count == 1);
            Assert.True(clientsName.FirstOrDefault().FirstName == "Tom");
        }

        [Fact]
        public async Task DeleteClientTest()
        {
            //Arrange
            var service = new ClientService();

            var clientId = Guid.NewGuid();
            var client = new Client
            {
                FirstName = "Пауо",
                LastName = "Коэльо",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId
            };

            //Act
            await service.AddClientAsync(client);

            var getClient = await service.GetClientAsync(clientId);

            Assert.Equal(getClient, client);

            await service.DeleteClientAsync(clientId);

            //Assert
            await Assert.ThrowsAsync<PersonDoesntExistException>(async () => await service.GetClientAsync(clientId));          
        }

        [Fact]
        public async Task UpdateClientTest()
        {
            //Arrange
            var service = new ClientService();

            var clientId = Guid.NewGuid();
            var client = new Client
            {
                FirstName = "Пауо",
                LastName = "Коэльо",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId
            };

            var updateClient = new Client
            {
                FirstName = "Говард",
                LastName = "Лавкрафт",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 10,
                ClientId = clientId
            };

            //Act
            await service.AddClientAsync(client);
            var getClient = await service.GetClientAsync(clientId);

            await service.UpdateClientAsync(updateClient);
            var updatedCl = await service.GetClientAsync(clientId);

            Assert.Equal(client, getClient);

            //Assert
            Assert.Equal(updateClient.FirstName, updatedCl.FirstName);
        }


        [Fact]
        public async Task AddGetAccountTest()
        {
            //Arrange
            var service = new ClientService();

            var accountId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var client = new Client
            {
                FirstName = "Павел",
                LastName = "Коэльо",
                NumberOfPassport = 6666,
                SeriesOfPassport = "111986",
                Phone = "77956730",
                DateOfBirth = new DateTime(1990, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId
            };

            var account = new Account
            {
                AccountId = accountId,
                Clientid = clientId,
                CurrencyCode = 840,
                Amount = 23546364
            };

            //Act
            await service.AddClientAsync(client);
            await service.AddAccountAsync(account);

            var getAccount = await service.GetAccountAsync(accountId);

            //Assert
            Assert.Equal(account.Amount, getAccount.Amount);
        }

        [Fact]
        public async Task UpdateAccountTest()
        {
            //Arrange
            var service = new ClientService();

            var accountId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var client = new Client
            {
                FirstName = "Павел",
                LastName = "Коэльо",
                NumberOfPassport = 9997,
                SeriesOfPassport = "111666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1990, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId
            };

            var account = new Account
            {
                AccountId = accountId,
                Clientid = clientId,
                CurrencyCode = 840,
                Amount = 23546364
            };

            var updateAccount = new Account
            {
                AccountId = accountId,
                Clientid = clientId,
                CurrencyCode = 840,
                Amount = 10000000
            };

            //Act
            await service.AddClientAsync(client);
            await service.AddAccountAsync(account);

            var getAccount = await service.GetAccountAsync(accountId);

            Assert.Equal(account.Amount, getAccount.Amount);

            await service.UpdateAccountAsync(updateAccount);
            var updatedAcc = await service.GetAccountAsync(accountId);

            //Assert
            Assert.Equal(updateAccount.Amount, updatedAcc.Amount);
        }

        [Fact]
        public async Task DeleteAccountTest()
        {
            //Arrange
            var service = new ClientService();

            var accountId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var client = new Client
            {
                FirstName = "Иван",
                LastName = "Иванов",
                NumberOfPassport = 4444,
                SeriesOfPassport = "333986",
                Phone = "77956730",
                DateOfBirth = new DateTime(1990, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId
            };

            var account = new Account
            {
                AccountId = accountId,
                Clientid = clientId,
                CurrencyCode = 840,
                Amount = 23546364
            };

            //Act
            await service.AddClientAsync(client);
            await  service.AddAccountAsync(account);

            var getAccount = await service.GetAccountAsync(accountId);

            Assert.Equal(account.Amount, getAccount.Amount);
            await service.DeleteAccountAsync(accountId);

            //Assert
            await Assert.ThrowsAsync<AccountDoesntExistException>(async () =>  await service.GetAccountAsync(accountId));         
        }
    }
}
