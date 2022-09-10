using Bogus;
using Models;
using Services;
using Services.Filtres;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceTests
{
    public class ClientStorageTest 
    {
        [Fact]

        public void SelectClientWithNameTest()
        {
            //Arrange
            var testClientService = new ClientService(new ClientStorage());       
        
            var clientJohn = new Client();
            clientJohn.FirstName = "John";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.FirstName = "John";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.FirstName = "Emily";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 2367;
            clientEmily.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientEmily);

            var filter = new ClientFilter();
            filter.FirstName = "John";

            //Act Assert
            Assert.False(testClientService.GetClients(filter).ContainsKey(clientEmily));                    
        }


        [Fact]

        public void SelectClientWithNumberOfPassportTest()
        {
            //Arrange
            var testClientService = new ClientService(new ClientStorage());

            var clientJohn = new Client();
            clientJohn.FirstName = "John";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.FirstName = "Dave";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(1999, 5, 6);
            testClientService.AddNewClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.FirstName = "Emily";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 6865;
            clientEmily.DateOfBirth = new DateTime(1998, 5, 6);
            testClientService.AddNewClient(clientEmily);

            var filter = new ClientFilter();
            filter.NumberOfPassport = 2367;

            //Act Assert
            Assert.False(testClientService.GetClients(filter).ContainsKey(clientEmily));
        }

        [Fact]

        public void SelectClientWithDateRangeTest()
        {
            //Arrange
            var testClientService = new ClientService(new ClientStorage());

            var clientJohn = new Client();
            clientJohn.FirstName = "John";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.FirstName = "John";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6);
            testClientService.AddNewClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.FirstName = "Emily";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 2367;
            clientEmily.DateOfBirth = new DateTime(1954, 5, 6);
            testClientService.AddNewClient(clientEmily);

            var filter = new ClientFilter();
            filter.MinDateTime = new DateTime(1950, 1,1);
            filter.MaxDateTime = new DateTime(1999, 1, 1);

            //Act Assert
            Assert.False(testClientService.GetClients(filter).ContainsKey(clientJohn));
        }

        [Fact]

        public void SelectYoungerClientTest()
        {
            //Arrange
            var testClientService = new ClientService(new ClientStorage());

            var clientJohn = new Client();
            clientJohn.FirstName = "John";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.FirstName = "Dave";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6);
            testClientService.AddNewClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.FirstName = "Emily";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 6865;
            clientEmily.DateOfBirth = new DateTime(1954, 5, 6);
            testClientService.AddNewClient(clientEmily);

            //Act
            Dictionary<Client, List<Account>> dict = testClientService.GetClients(new ClientFilter());

            var resultDict = new Dictionary<Client, List<Account>>(dict.Where(x => x.Key.DateOfBirth == dict.Max(a => a.Key.DateOfBirth)).ToArray());

            var youngerClient = resultDict.Keys.FirstOrDefault();

            //Assert
            Assert.Equal(youngerClient, clientJohn);
        }

        [Fact]

        public void SelectElderClientTest()
        {
            //Arrange
            var testClientService = new ClientService(new ClientStorage());

            var clientJohn = new Client();
            clientJohn.FirstName = "John";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.FirstName = "Dave";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6);
            testClientService.AddNewClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.FirstName = "Emily";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 6865;
            clientEmily.DateOfBirth = new DateTime(1954, 5, 6);
            testClientService.AddNewClient(clientEmily);
           
            //Act
            Dictionary<Client, List<Account>> clientDict = testClientService.GetClients(new ClientFilter());

            var resultDict = new Dictionary<Client, List<Account>>(clientDict.Where(x => x.Key.DateOfBirth == clientDict.Min(a => a.Key.DateOfBirth)).ToArray());

            var elderClient = resultDict.Keys.FirstOrDefault();

            //Assert
            Assert.Equal(elderClient, clientEmily);         
        }

        [Fact]

        public void SelectAverageClientAgeTest()
        {
            //Arrange
            var testClientService = new ClientService(new ClientStorage());

            var clientJohn = new Client();
            clientJohn.FirstName = "John";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6);
            testClientService.AddNewClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.FirstName = "Dave";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6);
            testClientService.AddNewClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.FirstName = "Emily";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 6865;
            clientEmily.DateOfBirth = new DateTime(1954, 5, 6);
            testClientService.AddNewClient(clientEmily);

            //Act
            Dictionary<Client, List<Account>> clientDict = testClientService.GetClients(new ClientFilter());

            var clients = clientDict.Keys.ToArray();
            var averageTicks = (long)clients.Select(d => d.DateOfBirth.Ticks).Average();
            var averageAge = (DateTime.Now.Year - new DateTime(averageTicks).Year);          

            //Assert
            Assert.Equal(averageAge, 45);
        }
    }
}
