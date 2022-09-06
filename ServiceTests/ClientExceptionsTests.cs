using Models;
using Models.ModelsValidationExceptions;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceTests
{
    public class ClientExceptionsTests
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
            var clientInTheDictionary = new Client();
            clientInTheDictionary.Age = 20;
            clientInTheDictionary.SeriesOfPassport = "I-ПР";
            clientInTheDictionary.NumberOfPassport = 356223435;

            ClientService testClientService = new ClientService();
            testClientService.AddNewClient(clientInTheDictionary);

            var clientOutOfTheDictionary = new Client();
            clientOutOfTheDictionary.Age = 20;
            clientOutOfTheDictionary.SeriesOfPassport = "I-ПР";
            clientOutOfTheDictionary.NumberOfPassport = 356223435;

            //Act Assert
            Assert.Throws<ClientAlreadyExistException>(() => testClientService.AddNewClient(clientOutOfTheDictionary));
        }
    }
}
