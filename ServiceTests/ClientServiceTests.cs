
using Bogus;
using Microsoft.VisualBasic;
using Models;
using ModelsDb;
using Services;
using Services.Filtres;
using System.Runtime.InteropServices;
using Xunit;

namespace ServiceTests
{
    public class ClientServiceTests
    {
        
        [Fact]

        public void GetClientsTest()
        {
            //Arrange
            TestDataGenerator testDataGenerator = new TestDataGenerator();
            Faker<Client> generatorClient = testDataGenerator.CreateClientListGenerator();
            List<Client> clients = generatorClient.Generate(10);

            var clientR = new Client_db
            {
                FirstName = "Александр",
                LastName = "Александров",
                NumberOfPassport = 25499,
                SeriesOfPassport = "876768",
                Phone = "77956734",
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                ClientId = Guid.Parse("9dc86979-08ac-4eac-891d-3dffa5d12082"),
                Accounts = null,
            };

            var service = new ClientService();

            //Act
            service.AddClient(ClientMapping(clients));
            service.AddClient(clientR);
            var getClient = service.GetClient(clientR.ClientId);
            
            //Assert
            Assert.True(getClient.SeriesOfPassport == clientR.SeriesOfPassport);

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
