
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
        public async Task ClientAgeValidationExceptionTest()
        {
            //Arrange
            var clientWithoutAge = new Client();
            clientWithoutAge.DateOfBirth = new DateTime(year: 2007, 5, 6);
            clientWithoutAge.SeriesOfPassport = "I-ПР";
            clientWithoutAge.NumberOfPassport = 356223435;

            //Act Assert
            var testClientService = new ClientService();
            await Assert.ThrowsAsync<PersonAgeValidationException>(async () => await testClientService.AddClientAsync(clientWithoutAge));
        }

        [Fact]
        public async Task ClientSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutSeriesOfPassort = new Client();
            clientWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            clientWithoutSeriesOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);

            //Act Assert
            var testClientService = new ClientService();
            await Assert.ThrowsAsync<PersonSeriesOfPassportValidationException>(async () => await testClientService.AddClientAsync(clientWithoutSeriesOfPassort));
        }

        [Fact]
        public async Task ClientNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var clientWithoutNumberOfPassort = new Client();
            clientWithoutNumberOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);
            clientWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";

            //Act Assert
            var testClientService = new ClientService();
            await Assert.ThrowsAsync<PersonNumberOfPassportValidationException>(async () => await testClientService.AddClientAsync(clientWithoutNumberOfPassort));
        }

        [Fact]
        public async Task ClientAlreadyExistExceptionTest()
        {
            //Arrange
            var testClientService = new ClientService();
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

            await testClientService.AddClientAsync(dictionaryClient);

            //Act Assert
            await Assert.ThrowsAsync<PersonAlreadyExistException>(async () => await testClientService.AddClientAsync(client));
        }
    }
}
