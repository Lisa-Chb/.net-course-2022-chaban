
using ModelsDb;
using Services;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceTests
{
    public class ClientServiceExceptionsTests
    {
        [Fact]

        public void ClientAgeValidationExceptionTest()
        {
            //Arrange
            var clientWithoutAge= new Client_db();
            clientWithoutAge.DateOfBirth = new DateTime(year: 2007, 5, 6);
            clientWithoutAge.SeriesOfPassport = "I-ПР";
            clientWithoutAge.NumberOfPassport = 356223435;

            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonAgeValidationException>(() => testClientService.AddClient(clientWithoutAge));
        }

        [Fact]

        public void ClientSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutSeriesOfPassort = new Client_db();
            clientWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            clientWithoutSeriesOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);

            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonSeriesOfPassportValidationException>(() => testClientService.AddClient(clientWithoutSeriesOfPassort));
        }

        [Fact]

        public void ClientNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutNumberOfPassort = new Client_db();
            clientWithoutNumberOfPassort.DateOfBirth = new DateTime(year:1998, 5, 5);
            clientWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            
            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonNumberOfPassportValidationException>(() => testClientService.AddClient(clientWithoutNumberOfPassort));
        }

        [Fact]

        public void ClientAlreadyExistExceptionTest()
        {
            //Arrange
            ClientService testClientService = new ClientService();

            var dictionaryClient = new Client_db();
            dictionaryClient.DateOfBirth = new DateTime(year: 1998, 5, 5);
            dictionaryClient.SeriesOfPassport = "I-ПР";
            dictionaryClient.NumberOfPassport = 356223435;

            var client = new Client_db();
            client.DateOfBirth = new DateTime(year: 1998, 5, 5);
            client.SeriesOfPassport = "I-ПР";
            client.NumberOfPassport = 356223435;

            testClientService.AddClient(dictionaryClient);


            //Act Assert
            Assert.Throws<PersonAlreadyExistException>(() => testClientService.AddClient(client));
        }
    }
}
