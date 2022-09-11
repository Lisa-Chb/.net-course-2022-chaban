using Models;
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
    public class ClientServiceTests
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
            ClientService testClientService = new ClientService(new ClientStorage());
            Assert.Throws<PersonAgeValidationException>(() => testClientService.AddNewClient(clientWithoutAge));
        }

        [Fact]

        public void ClientSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutSeriesOfPassort = new Client();
            clientWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            clientWithoutSeriesOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);

            //Act Assert
            ClientService testClientService = new ClientService(new ClientStorage());
            Assert.Throws<PersonSeriesOfPassportValidationException>(() => testClientService.AddNewClient(clientWithoutSeriesOfPassort));
        }

        [Fact]

        public void ClientNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutNumberOfPassort = new Client();
            clientWithoutNumberOfPassort.DateOfBirth = new DateTime(year:1998, 5, 5);
            clientWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            
            //Act Assert
            ClientService testClientService = new ClientService(new ClientStorage());
            Assert.Throws<PersonNumberOfPassportValidationException>(() => testClientService.AddNewClient(clientWithoutNumberOfPassort));
        }

        [Fact]

        public void ClientAlreadyExistExceptionTest()
        {
            //Arrange
            ClientService testClientService = new ClientService(new ClientStorage());

            var dictionaryClient = new Client();
            dictionaryClient.DateOfBirth = new DateTime(year: 1998, 5, 5);
            dictionaryClient.SeriesOfPassport = "I-ПР";
            dictionaryClient.NumberOfPassport = 356223435;

            var client = new Client();
            client.DateOfBirth = new DateTime(year: 1998, 5, 5);
            client.SeriesOfPassport = "I-ПР";
            client.NumberOfPassport = 356223435;

            testClientService.AddNewClient(dictionaryClient);

            //Act Assert
            Assert.Throws<ClientAlreadyExistException>(() => testClientService.AddNewClient(client));
        }
    }
}
