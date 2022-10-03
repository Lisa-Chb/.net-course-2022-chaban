using Models;
using Services;
using Services.Filtres;
using Xunit;

namespace ServiceTests
{
    public class ClientFilterTest
    {    
        [Fact]

        public void SelectClientWithNameTest()
        {
            //Arrange
            var testClientService = new ClientService();

            var clientJohn = new Client();
            clientJohn.ClientId = Guid.NewGuid();
            clientJohn.FirstName = "John";
            clientJohn.LastName = "Wick";
            clientJohn.Phone = "077888876";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.ClientId = Guid.NewGuid();
            clientJohnToo.FirstName = "John";
            clientJohnToo.LastName = "Wick";
            clientJohnToo.Phone = "077888875";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.ClientId = Guid.NewGuid();
            clientEmily.FirstName = "Emily";
            clientEmily.LastName = "Wick";
            clientEmily.Phone = "077888874";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 2367;
            clientEmily.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientEmily);

            var filter = new ClientFilter();
            filter.FirstName = "John";
            filter.PageSize = 10;

            //Act
            var clients = testClientService.GetClients(filter);

            //Assert
            Assert.DoesNotContain(clientEmily, clients);
        }


        [Fact]

        public void SelectClientWithNumberOfPassportTest()
        {
            //Arrange
            var testClientService = new ClientService();

            var clientJohn = new Client();
            clientJohn.ClientId = Guid.NewGuid();
            clientJohn.FirstName = "John";
            clientJohn.LastName = "Wick";
            clientJohn.Phone = "077888873";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.ClientId = Guid.NewGuid();
            clientJohnToo.FirstName = "Dave";
            clientJohnToo.LastName = "Wikc";
            clientJohnToo.Phone = "077888872";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(1999, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.ClientId = Guid.NewGuid();
            clientEmily.FirstName = "Emily";
            clientEmily.LastName = "Wick";
            clientEmily.Phone = "077888871";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 6865;
            clientEmily.DateOfBirth = new DateTime(1998, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientEmily);

            var filter = new ClientFilter();
            filter.NumberOfPassport = 2367;
            filter.PageSize = 10;

            //Act 
            var clients = testClientService.GetClients(filter);

            //Assert
            Assert.DoesNotContain(clientEmily, clients);
        }

        [Fact]

        public void SelectClientWithDateRangeTest()
        {
            //Arrange
            var testClientService = new ClientService();

            var clientJohn = new Client();
            clientJohn.ClientId = Guid.NewGuid();
            clientJohn.FirstName = "John";
            clientJohn.LastName = "Wick";
            clientJohn.Phone = "077888870";
            clientJohn.SeriesOfPassport = "PR -56";
            clientJohn.NumberOfPassport = 2367;
            clientJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientJohn);

            var clientJohnToo = new Client();
            clientJohnToo.ClientId = Guid.NewGuid();
            clientJohnToo.FirstName = "John";
            clientJohnToo.LastName = "Wick";
            clientJohnToo.Phone = "077888877";
            clientJohnToo.SeriesOfPassport = "PR -96";
            clientJohnToo.NumberOfPassport = 2367;
            clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientJohnToo);

            var clientEmily = new Client();
            clientEmily.ClientId = Guid.NewGuid();
            clientEmily.FirstName = "Emily";
            clientEmily.LastName = "Wick";
            clientEmily.Phone = "077888878";
            clientEmily.SeriesOfPassport = "PR -56";
            clientEmily.NumberOfPassport = 2367;
            clientEmily.DateOfBirth = new DateTime(1954, 5, 6).ToUniversalTime();
            testClientService.AddClient(clientEmily);

            var filter = new ClientFilter();
            filter.MinDateTime = new DateTime(1950, 1, 1).ToUniversalTime();
            filter.MaxDateTime = new DateTime(1999, 1, 1).ToUniversalTime();
            filter.PageSize = 10;

            //Act 
            var clients = testClientService.GetClients(filter);

            //Assert
            Assert.DoesNotContain(clientJohn, clients);
        }
        /*
                [Fact]

                public void SelectYoungerClientTest()
                {
                    //Arrange
                    var testClientService = new ClientService();

                    var clientJohn = new Client();
                    clientJohn.ClientId = Guid.NewGuid();
                    clientJohn.FirstName = "John";
                    clientJohn.LastName = "Wick";
                    clientJohn.Phone = "077888879";
                    clientJohn.SeriesOfPassport = "PR -56";
                    clientJohn.NumberOfPassport = 2367;
                    clientJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientJohn));

                    var clientJohnToo = new Client();
                    clientJohnToo.ClientId = Guid.NewGuid();
                    clientJohnToo.FirstName = "Dave";
                    clientJohnToo.LastName = "Wick";
                    clientJohnToo.Phone = "077888880";
                    clientJohnToo.SeriesOfPassport = "PR -96";
                    clientJohnToo.NumberOfPassport = 2367;
                    clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientJohnToo));

                    var clientEmily = new Client();
                    clientEmily.ClientId = Guid.NewGuid();
                    clientEmily.FirstName = "Emily";
                    clientEmily.LastName = "Wick";
                    clientEmily.Phone = "077888881";
                    clientEmily.SeriesOfPassport = "PR -56";
                    clientEmily.NumberOfPassport = 6865;
                    clientEmily.DateOfBirth = new DateTime(1954, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientEmily));

                    //Act
                    //
                    var clients = testClientService.GetClients(new ClientFilter() { PageSize = 10 });

                    var resultDict = clients.Where(x => x.DateOfBirth == clients.Max(a => a.DateOfBirth)).ToArray();

                    var youngerClient = resultDict.FirstOrDefault();

                    //Assert
                    Assert.Equal(youngerClient, ClientMapping(clientJohn));
                }

                [Fact]

                public void SelectElderClientTest()
                {
                    //Arrange
                    var testClientService = new ClientService();

                    var clientJohn = new Client();
                    clientJohn.ClientId = Guid.NewGuid();
                    clientJohn.FirstName = "John";
                    clientJohn.LastName = "Wick;";
                    clientJohn.Phone = "077888882";
                    clientJohn.SeriesOfPassport = "PR -56";
                    clientJohn.NumberOfPassport = 2367;
                    clientJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientJohn));

                    var clientJohnToo = new Client();
                    clientJohnToo.ClientId = Guid.NewGuid();
                    clientJohnToo.FirstName = "Dave";
                    clientJohnToo.LastName = "Wick";
                    clientJohnToo.Phone = "077888883";
                    clientJohnToo.SeriesOfPassport = "PR -96";
                    clientJohnToo.NumberOfPassport = 2367;
                    clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientJohnToo));

                    var clientEmily = new Client();
                    clientEmily.ClientId = Guid.NewGuid();
                    clientEmily.FirstName = "Emily";
                    clientEmily.LastName = "Wick";
                    clientEmily.Phone = "077888884";
                    clientEmily.SeriesOfPassport = "PR -56";
                    clientEmily.NumberOfPassport = 6865;
                    clientEmily.DateOfBirth = new DateTime(1954, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientEmily));

                    //Act
                    var clients = testClientService.GetClients(new ClientFilter(){ PageSize = 10 });

                    var resultClient = clients.Where(x => x.DateOfBirth == clients.Min(a => a.DateOfBirth)).ToArray();

                    var elderClient = resultClient.FirstOrDefault();

                    //Assert
                    Assert.Equal(elderClient, ClientMapping(clientEmily));
                }

                [Fact]

                public void SelectAverageClientAgeTest()
                {
                    //Arrange
                    var testClientService = new ClientService();

                    var clientJohn = new Client();
                    clientJohn.ClientId = Guid.NewGuid();
                    clientJohn.FirstName = "John";
                    clientJohn.LastName = "Wick";
                    clientJohn.Phone = "077888885";
                    clientJohn.SeriesOfPassport = "PR -56";
                    clientJohn.NumberOfPassport = 2367;
                    clientJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientJohn));

                    var clientJohnToo = new Client();
                    clientJohnToo.ClientId = Guid.NewGuid();
                    clientJohnToo.FirstName = "Dave";
                    clientJohnToo.LastName = "Wick";
                    clientJohnToo.Phone = "077888886";
                    clientJohnToo.SeriesOfPassport = "PR -96";
                    clientJohnToo.NumberOfPassport = 2367;
                    clientJohnToo.DateOfBirth = new DateTime(1978, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientJohnToo));

                    var clientEmily = new Client();
                    clientEmily.ClientId = Guid.NewGuid();
                    clientEmily.FirstName = "Emily";
                    clientEmily.LastName = "Wick";
                    clientEmily.Phone = "077888887";
                    clientEmily.SeriesOfPassport = "PR -56";
                    clientEmily.NumberOfPassport = 6865;
                    clientEmily.DateOfBirth = new DateTime(1954, 5, 6).ToUniversalTime();
                    testClientService.AddClient(ClientMapping(clientEmily));

                    //Act
                    var clients = testClientService.GetClients(new ClientFilter() { PageSize = 10 });

                    var averageTicks = (long)clients.Select(d => d.DateOfBirth.Ticks).Average();
                    var expectedAge = (DateTime.Now.Year - new DateTime(averageTicks).Year);

                    //Assert
                    Assert.Equal(expectedAge, 45);

                }
        */      
    }
}
    

