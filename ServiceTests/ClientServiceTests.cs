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
            clientWithoutAge.SeriesOfPassport = "I-ПР";
            clientWithoutAge.NumberOfPassport = 356223435;

            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonAgeValidationException>(() => testClientService.AddNewClient(clientWithoutAge));
        }

        [Fact]

        public void ClientSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutSeriesOfPassort = new Client();
            clientWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            clientWithoutSeriesOfPassort.Age = 20;
            
            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonSeriesOfPassportValidationException>(() => testClientService.AddNewClient(clientWithoutSeriesOfPassort));
        }

        [Fact]

        public void ClientNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutNumberOfPassort = new Client();
            clientWithoutNumberOfPassort.Age = 20;
            clientWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            
            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<PersonNumberOfPassportValidationException>(() => testClientService.AddNewClient(clientWithoutNumberOfPassort));
        }

        [Fact]

        public void ClientAlreadyExistExceptionTest()
        {
            //Arrange
            ClientService testClientService = new ClientService();

            var dictionaryClient = new Client();
            dictionaryClient.Age = 20;
            dictionaryClient.SeriesOfPassport = "I-ПР";
            dictionaryClient.NumberOfPassport = 356223435;

            var client = new Client();
            client.Age = 20;
            client.SeriesOfPassport = "I-ПР";
            client.NumberOfPassport = 356223435;

            testClientService.AddNewClient(dictionaryClient);

            //Act Assert
            Assert.Throws<ClientAlreadyExistException>(() => testClientService.AddNewClient(client));
        }
    }
}
