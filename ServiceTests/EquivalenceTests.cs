using Bogus;
using Models;
using Services;
using Xunit;

namespace ServiceTests
{
    public class EquivalenceTests
    {

        [Fact]

        public void GetHashCodeNecessityPositivTest()
        {
            //Arrange
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client> generatorClient = testDataGenerator.CreateClientList();
            List<Client> clients = generatorClient.Generate(1000);

            Client testClient = new Client();
            testClient.Phone = "77658346";
            testClient.Age = 65;
            testClient.FirstName = "Johny";
            testClient.LastName = "Depp";

            Account testAccount = new Account();
            Currency currency = new Currency();
            currency.Name = "RUB";
            currency.Code = 5637;
            testAccount.Currency = currency;
            testAccount.Amount = 157000;

            Dictionary<Client, Account> dictionary = testDataGenerator.CreateClientDictionaryWithAccount(clients);
            dictionary.Add(testClient, testAccount);

            Account testAccount2 = new Account();
            Currency currency2 = new Currency();
            currency2.Name = "RUB";
            currency2.Code = 5637;
            testAccount2.Currency = currency2;
            testAccount2.Amount = 157000;

            Client testClient2 = new Client();
            testClient2.Phone = "77658346";
            testClient2.Age = 65;
            testClient2.FirstName = "Johny";
            testClient2.LastName = "Depp";

            //Act
            Account finalAccout = dictionary[testClient2];

            //Assert
            Assert.Equal(finalAccout, testAccount);
        }

        [Fact]

        public void GetHashCodeNecessityPositiv()
        {

        }
    }
}