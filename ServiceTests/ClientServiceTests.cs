﻿
using Bogus;
using Models;
using ModelsDb;
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
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client> generatorClient = testDataGenerator.CreateClientListGenerator();
            List<Client> clients = generatorClient.Generate(5);

            var client = new ClientDb
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
            foreach (var c in clients)
            {
                service.AddClient(ClientMapping(c));
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
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client> generatorClient = testDataGenerator.CreateClientListGenerator();
            List<Client> clients = generatorClient.Generate(5);
            var service = new ClientService();

            var filter = new ClientFilter
            {
                FirstName = "Tom",
                PageSize = 1,
                //BonusDiscount = 1,
            };

            var clientTom = new ClientDb()
            {
                FirstName = "Tom",
                LastName = "Holland",
                Phone = "77768000",
                NumberOfPassport = 9854,
                SeriesOfPassport = "657755",
                ClientId = Guid.NewGuid(),
                BonusDiscount = 0,
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime(),
            };

            //Act
            foreach (var c in clients)
            {
                service.AddClient(ClientMapping(c));
            }
            service.AddClient(clientTom);
            // var clientsOrderBy = service.GetClients(filter);

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

            var client = new ClientDb
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
            var client = new ClientDb
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

            var updateClient = new ClientDb
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
            var clientId = Guid.NewGuid();

            var client = new ClientDb
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

            var currencys = new List<CurrencyDb>
            {
                new CurrencyDb
                {
                CurrencyId = Guid.NewGuid(),
                AccountId = accountId,
                Name = "USD",
                Code = 4567
                }
            };

            var account = new AccountDb
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 23546364,
                Currencys = null
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

            var client = new ClientDb
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

            var currency = new List<CurrencyDb>
            {
                new CurrencyDb
                {
                CurrencyId = Guid.NewGuid(),
                AccountId = accountId,
                Name = "USD",
                Code = 4567
                }
            };

            var account = new AccountDb
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 23546364,
                Currencys = null
            };

            var updateAccount = new AccountDb
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 1000,
                Currencys = null
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

            var client = new ClientDb
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


            var currency = new CurrencyDb
            {
                CurrencyId = Guid.NewGuid(),
                AccountId = accountId,
                Name = "RUB",
                Code = 4567
            };

            var account = new AccountDb
            {
                AccountId = accountId,
                Clientid = clientId,
                Amount = 23546364,
                Currencys = null
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
                ClientId = client.ClientId,
            };
        }
    }
}
