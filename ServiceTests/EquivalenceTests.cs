using Bogus;
using Models;
using Services;

namespace ServiceTests
{
    public class EquivalenceTests
    {
        public static void GetHashCodeNecessityPositivTest()
        {
            Faker<Client> generatorClient = TestDataGenerator.ClientGenerator();
            List<Client> clients = generatorClient.Generate(1000);

            Account testAccount = new Account();
            testAccount.Currency = "USD";
            testAccount.Amount = 157000;

            Client testClient = new Client();
            testClient.Phone = "77658346";
            testClient.Age = 65;
            testClient.FirstName = "Johny";
            testClient.LastName = "Depp";
            testClient.Account = testAccount;

            clients.Add(testClient);
            Dictionary<Client, Account> dictionary = TestDataGenerator.NewDictionaryGenerator(clients);

            Account testAccount2 = new Account();
            testAccount2.Currency = "USD";
            testAccount2.Amount = 157000;

            Client testClient2 = new Client();
            testClient2.Phone = "77658346";
            testClient2.Age = 65;
            testClient2.FirstName = "Johny";
            testClient2.LastName = "Depp";
            testClient2.Account = testAccount2;

            Account testAccout = dictionary[testClient2];
        }


    }
}