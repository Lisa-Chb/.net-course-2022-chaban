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
            Client clientWithoutAge= new Client();
            clientWithoutAge.SeriesOfPassport = "I-ПР";
            clientWithoutAge.NumberOfPassport = 356223435;

            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<ClientAgeValidationException>(() => testClientService.AddNewClient(clientWithoutAge));
        }

        [Fact]

        public void ClientSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            Client clientWithoutSeriesOfPassort = new Client();
            clientWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            clientWithoutSeriesOfPassort.Age = 20;
            
            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<ClientSeriesOfPassportValidationException>(() => testClientService.AddNewClient(clientWithoutSeriesOfPassort));
        }

        [Fact]

        public void ClientNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            Client clientWithoutNumberOfPassort = new Client();
            clientWithoutNumberOfPassort.Age = 20;
            clientWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            
            //Act Assert
            ClientService testClientService = new ClientService();
            Assert.Throws<ClientNumberOfPassportValidationException>(() => testClientService.AddNewClient(clientWithoutNumberOfPassort));
        }
    }
}
