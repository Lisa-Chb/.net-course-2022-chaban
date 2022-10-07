using Models;
using Services;
using Services.Filtres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExportTool
{
    public class ExportServiceTests
    {
        [Fact]
        public void writeClientTest()
        {
            //Arrange
            var service = new ClientService();
            var filter = new ClientFilter() { PageSize = 1000 };
            var clients = service.GetClients(filter);
            var pathToDirectory = @"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\";
            var exportService = new ExportService();

            //Act
            exportService.WriteClientToCsv(clients, pathToDirectory, "WriteClientPractise.csv");
            var clientsRead = exportService.ReadClientFromCsv(pathToDirectory, "WriteClientPractise.csv");

            //Asssert
            Assert.Equal(clients.Count, clientsRead.Count);
        }

        [Fact]
        public void readClientTest()
        {
            //Arrange
            var service = new ClientService();

            var clients = new List<Client>();
            var clientWick = new Client();
            clientWick.ClientId = Guid.NewGuid();
            clientWick.FirstName = "StreamTest";
            clientWick.LastName = "Wick";
            clientWick.Phone = "077888876";
            clientWick.SeriesOfPassport = "PR -56";
            clientWick.NumberOfPassport = 2367;
            clientWick.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            clients.Add(clientWick);

            var clientDave = new Client();
            clientDave.ClientId = Guid.NewGuid();
            clientDave.FirstName = "StreamTest";
            clientDave.LastName = "Dave";
            clientDave.Phone = "077888875";
            clientDave.SeriesOfPassport = "PR -96";
            clientDave.NumberOfPassport = 2367;
            clientDave.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            clients.Add(clientDave);

            var clientSames = new Client();
            clientSames.ClientId = Guid.NewGuid();
            clientSames.FirstName = "StreamTest";
            clientSames.LastName = "Sames";
            clientSames.Phone = "077888874";
            clientSames.SeriesOfPassport = "PR -56";
            clientSames.NumberOfPassport = 2367;
            clientSames.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            clients.Add(clientSames);

            //Act
            var pathToDirectory = @"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\";
            var exportService = new ExportService();
            exportService.WriteClientToCsv(clients, pathToDirectory, "ReadClientPractise.csv");

            var clientsRead = exportService.ReadClientFromCsv(pathToDirectory, "ReadClientPractise.csv");
            foreach (var client in clientsRead)
            {
                service.AddClient(client);
            }

            //Assert
            Assert.NotEmpty(clientsRead);
        }
    }
}

