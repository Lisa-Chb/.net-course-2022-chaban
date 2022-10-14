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
        public void AddGetClientTest()
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
                service.AddClient(c);
            }
            service.AddClient(client);

            var getClient = service.GetClient(client.ClientId);

            //Assert
            Assert.Equal(getClient, client);
        }

        [Fact]
        public void GetClientsTest()
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
                service.AddClient(c);
            }
            service.AddClient(clientTom);

            //Assert
            Assert.True(service.GetClients(filter).Count == 1);
            Assert.True(service.GetClients(filter).FirstOrDefault().FirstName == "Tom");
        }

        [Fact]
        public void DeleteClientTest()
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
            service.AddClient(client);

            var getClient = service.GetClient(clientId);

            Assert.Equal(getClient, client);

            service.DeleteClient(clientId);

            //Assert
            Assert.Throws<PersonDoesntExistException>(() =>
            {
                service.GetClient(clientId);
            });
        }

        [Fact]
        public void UpdateClientTest()
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
            service.AddClient(client);
            var getClient = service.GetClient(clientId);

            service.UpdateClient(updateClient);
            var updatedCl = service.GetClient(clientId);

            Assert.Equal(client, getClient);

            //Assert
            Assert.Equal(updateClient.FirstName, updatedCl.FirstName);
        }


        [Fact]
        public void AddGetAccountTest()
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
            service.AddClient(client);
            service.AddAccount(account);

            var getAccount = service.GetAccount(accountId);

            //Assert
            Assert.Equal(account.Amount, getAccount.Amount);
        }

        [Fact]
        public void UpdateAccountTest()
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
            service.AddClient(client);
            service.AddAccount(account);

            var getAccount = service.GetAccount(accountId);

            Assert.Equal(account.Amount, getAccount.Amount);

            service.UpdateAccount(updateAccount);
            var updatedAcc = service.GetAccount(accountId);

            //Assert
            Assert.Equal(updateAccount.Amount, updatedAcc.Amount);
        }

        [Fact]
        public void DeleteAccountTest()
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
            service.AddClient(client);
            service.AddAccount(account);

            var getAccount = service.GetAccount(accountId);

            Assert.Equal(account.Amount, getAccount.Amount);
            service.DeleteAccount(accountId);

            //Assert
            Assert.Throws<AccountDoesntExistException>(() =>
            {
                service.GetAccount(accountId);
            });
        }
    }
}
