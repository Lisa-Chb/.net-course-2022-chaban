
using Bogus;
using ModelsDb;
using Services;
using Xunit;

namespace ServiceTests
{
    public class ClientServiceTests
    {
        [Fact]

        public void GetClientsTest()
        {
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client_db> generatorClient = testDataGenerator.CreateClientListGenerator();
            List<Client_db> clients = generatorClient.Generate(1000);
            var service = new ClientService();

        }

    }
}
