
using Models;
using ModelsDb;
using Services;
using Services.Exceptions;
using Xunit;

namespace ServiceTests
{
    public class ClientServiceExceptionsTests
    {
        [Fact]

        public void ClientAgeValidationExceptionTest()
        {
            //Arrange
            var clientWithoutAge= new Client();
            clientWithoutAge.DateOfBirth = new DateTime(year: 2007, 5, 6);
            clientWithoutAge.SeriesOfPassport = "I-ПР";
            clientWithoutAge.NumberOfPassport = 356223435;

            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonAgeValidationException>(() => testClientService.AddClient(ClientMapping(clientWithoutAge)));
        }

        [Fact]

        public void ClientSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutSeriesOfPassort = new Client();
            clientWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            clientWithoutSeriesOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);

            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonSeriesOfPassportValidationException>(() => testClientService.AddClient(ClientMapping(clientWithoutSeriesOfPassort)));
        }

        [Fact]

        public void ClientNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutNumberOfPassort = new Client();
            clientWithoutNumberOfPassort.DateOfBirth = new DateTime(year:1998, 5, 5);
            clientWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            
            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonNumberOfPassportValidationException>(() => testClientService.AddClient(ClientMapping(clientWithoutNumberOfPassort)));
        }

        [Fact]

        public void ClientAlreadyExistExceptionTest()
        {
            //Arrange
            ClientService testClientService = new ClientService();

            var dictionaryClient = new Client();
            dictionaryClient.FirstName = "John";
            dictionaryClient.LastName = "Johanson";
            dictionaryClient.Phone = "66748563";
            dictionaryClient.DateOfBirth = new DateTime(year: 1998, 5, 5).ToUniversalTime();
            dictionaryClient.SeriesOfPassport = "I-ПР";
            dictionaryClient.NumberOfPassport = 356223435;
            dictionaryClient.ClientId = Guid.NewGuid();

            var client = new Client();
            dictionaryClient.FirstName = "John";
            dictionaryClient.LastName = "Johanson";
            dictionaryClient.Phone = "66748563";
            client.DateOfBirth = new DateTime(year: 1998, 5, 5).ToUniversalTime();
            client.SeriesOfPassport = "I-ПР";
            client.NumberOfPassport = 356223435;
            client.ClientId = dictionaryClient.ClientId;

            testClientService.AddClient(ClientMapping(dictionaryClient));


            //Act Assert
            Assert.Throws<PersonAlreadyExistException>(() => testClientService.AddClient(ClientMapping(client)));
        }

        private Client_db ClientMapping(Client client)
        {
            var client_Db = new Client_db
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
                           
            return client_Db;
        }
    }
}
