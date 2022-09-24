
using Bogus;
using Microsoft.VisualBasic;
using Models;
using ModelsDb;
using Services;
using Services.Exceptions;
using Services.Filtres;
using System.Runtime.InteropServices;
using Xunit;

namespace ServiceTests
{
    public class ClientServiceTests
    {
        [Fact]

        public void AddGetClientTest()
        {
            //Arrange
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client> generatorClient = testDataGenerator.CreateClientListGenerator();
            List<Client> clients = generatorClient.Generate(10);

            var client = new Client_db
            {
                FirstName = "Александр",
                LastName = "Александров",
                NumberOfPassport = 25499,
                SeriesOfPassport = "876768",
                Phone = "77956734",
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = Guid.NewGuid(),
                Accounts = null,
            };

            var service = new ClientService();

            //Act
            service.AddClient(ClientMapping(clients));
            service.AddClient(client);
            var getClient = service.GetClient(client.ClientId);

            //Assert
            Assert.Equal(getClient, client);
        }

        [Fact]

        public void GetClientsTest()
        {
            //Arrange
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client> generatorClient = testDataGenerator.CreateClientListGenerator();
            List<Client> clients = generatorClient.Generate(5);
            var service = new ClientService();

            var filter = new ClientFilter
            {
                FirstName = "Tom",
                PageSize = 1
            };

            var listTom = new List<Client_db> {
              new Client_db
            {
                FirstName = "Tom",
                LastName = "Holland",
                Phone = "77768000",
                NumberOfPassport = 9854,
                SeriesOfPassport = "657755",
                ClientId = Guid.NewGuid(),
                BonusDiscount = 0,
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime(),
            },
             new Client_db
            {
                FirstName = "Tom",
                LastName = "Hemsword",
                Phone = "77768654",
                NumberOfPassport = 6854,
                SeriesOfPassport = "657778",
                ClientId = Guid.NewGuid(),
                BonusDiscount = 0,
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime(),
             }
            };


            //Act
            service.AddClient(ClientMapping(clients));
            service.AddClient(listTom);

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

            var client = new Client_db
            {
                FirstName = "Пауо",
                LastName = "Коэльо",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId,
                Accounts = null,
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
            var client = new Client_db
            {
                FirstName = "Пауо",
                LastName = "Коэльо",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId,
                Accounts = null,
            };

            var updateClient = new Client_db
            {
                FirstName = "Говард",
                LastName = "Лавкрафт",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666666",
                Phone = "77777777",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 10,
                ClientId = clientId,
                Accounts = null,
            };

            //Act
            service.AddClient(client);
            var getClient = service.GetClient(clientId);
            Assert.Equal(client, getClient);

            service.UpdateClient(updateClient);

            var updatedCl = service.GetClient(clientId);

            //Assert
            Assert.Equal(updateClient, updatedCl);
        }


        [Fact]

        public void AddGetAccountTest()
        {
            //Arrange
            var accountId = Guid.NewGuid();
            var currencyId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var client = new Client_db
            {
                FirstName = "Павел",
                LastName = "Коэльо",
                NumberOfPassport = 6666,
                SeriesOfPassport = "111986",
                Phone = "77956730",
                DateOfBirth = new DateTime(1990, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId,
                Accounts = null,
            };

            var currency = new Currency_db
            {
                CurrencyId = currencyId,
                AccountId = accountId,
                Name = "USD",
                Code = 4567
            };

            var account = new Account_db
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 23546364,
                Currency = currency
            };

            var service = new ClientService();

            //Act
            service.AddClient(client);
            service.AddAccount(account);
            var getAccount = service.GetAccount(accountId);

            //Assert
            Assert.Equal(account, getAccount);
        }

        [Fact]
        public void UpdateAccountTest()
        {
            //Arrange

            var service = new ClientService();

            var accountId = Guid.NewGuid();
            var clientId = Guid.NewGuid();

            var client = new Client_db
            {
                FirstName = "Павел",
                LastName = "Коэльо",
                NumberOfPassport = 9997,
                SeriesOfPassport = "111666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1990, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId,
                Accounts = null,
            };

            var currency = new Currency_db
            {
                CurrencyId = Guid.NewGuid(),
                AccountId = accountId,
                Name = "USD",
                Code = 4567
            };

            var account = new Account_db
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 23546364,
                Currency = currency
            };

            var updateAccount = new Account_db
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 1000,
                Currency = currency
            };

            //Act
            service.AddClient(client);
            service.AddAccount(account);
            var getAccount = service.GetAccount(accountId);

            Assert.Equal(account, getAccount);

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

            var client = new Client_db
            {
                FirstName = "Иван",
                LastName = "Иванов",
                NumberOfPassport = 4444,
                SeriesOfPassport = "333986",
                Phone = "77956730",
                DateOfBirth = new DateTime(1990, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = clientId,
                Accounts = null,
            };


            var currency = new Currency_db
            {
                CurrencyId = Guid.NewGuid(),
                AccountId = accountId,
                Name = "RUB",
                Code = 4567
            };

            var account = new Account_db
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 23546364,
                Currency = currency
            };

            //Act
            service.AddClient(client);
            service.AddAccount(account);

            var getAccount = service.GetAccount(accountId);
            Assert.Equal(account, getAccount);
            service.DeleteAccount(accountId);

            //Assert
            Assert.Throws<AccountDoesntExistException>(() =>
            {
                service.GetAccount(accountId);
            });
        }
        private List<Client_db> ClientMapping(List<Client> clients)
        {
            var client_DbList = new List<Client_db>();
            foreach (var client in clients)
            {
                client_DbList.Add(new Client_db
                {
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    NumberOfPassport = client.NumberOfPassport,
                    SeriesOfPassport = client.SeriesOfPassport,
                    Phone = client.Phone,
                    DateOfBirth = client.DateOfBirth,
                    BonusDiscount = client.BonusDiscount,
                    ClientId = client.ClientId,
                });
            }
            return client_DbList;
        }
    }
}
