using Models;
using Services;
using Services.Filtres;
using Xunit;

namespace ExportTool
{
    public class ExportServiceTests
    {
        [Fact]
        public async Task WriteAndReadClientsSerializeTest()
        {
            var pathToDirectory = @"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\";
            var exportService = new ExportService();
            var service = new ClientService();

            var clients = new List<Client>();
            var clientWick = new Client();
            clientWick.ClientId = Guid.NewGuid();
            clientWick.FirstName = "SerializeClient";
            clientWick.LastName = "Wick";
            clientWick.Phone = "077888876";
            clientWick.SeriesOfPassport = "PR -56";
            clientWick.NumberOfPassport = 2367;
            clientWick.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            clientWick.Accounts = new List<Account> { new Account()
            { AccountId = Guid.NewGuid(), Amount = 200, Clientid = Guid.NewGuid(), CurrencyCode = 648, Currency = new Currency(){Name = "USD"}}};
            clients.Add(clientWick);

            var clientDave = new Client();
            clientDave.ClientId = Guid.NewGuid();
            clientDave.FirstName = "SerializeClient";
            clientDave.LastName = "Dave";
            clientDave.Phone = "077888875";
            clientDave.SeriesOfPassport = "PR -96";
            clientDave.NumberOfPassport = 2367;
            clientDave.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            clientDave.Accounts = new List<Account> { new Account()
            { AccountId = Guid.NewGuid(), Amount = 200, Clientid = Guid.NewGuid(), CurrencyCode = 648, Currency = new Currency(){Name = "USD"}}};
            clients.Add(clientDave);

            var clientSames = new Client();
            clientSames.ClientId = Guid.NewGuid();
            clientSames.FirstName = "SerializeClient";
            clientSames.LastName = "Same";
            clientSames.Phone = "077888874";
            clientSames.SeriesOfPassport = "PR -56";
            clientSames.NumberOfPassport = 2367;
            clientSames.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            clientSames.Accounts = new List<Account> { new Account()
            { AccountId = Guid.NewGuid(), Amount = 200, Clientid = Guid.NewGuid(), CurrencyCode = 648, Currency = new Currency(){Name = "USD"}}};
            clients.Add(clientSames);

            //Act

            await exportService.WriteSerializePersonToCsv(clients, pathToDirectory, "ClientSerializePractise.json");

            var clientsRead = await exportService.ReadSerializePersonFromCsv<List<Client>>(pathToDirectory, "ClientSerializePractise.json");

            foreach (var client in clientsRead)
            {
                await service.AddClientAsync(client);
            }

            //Assert
            var testClient = clientsRead.First();
            var testAccount = testClient.Accounts.First();
            var testCurrency = testAccount.Currency;
            Assert.Equal("SerializeClient", testClient.FirstName);
            Assert.Equal(200, testAccount.Amount);
            Assert.Equal("USD", testCurrency.Name);
        }


        [Fact]
        public async Task WriteAndReadEmployeesSerializeTest()
        {
            var pathToDirectory = @"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\";
            var exportService = new ExportService();
            var service = new EmployeeService();

            var employees = new List<Employee>();

            var employeeWick = new Employee();
            employeeWick.EmployeeId = Guid.NewGuid();
            employeeWick.FirstName = "SerializeEmployee";
            employeeWick.LastName = "Wick";
            employeeWick.Phone = "077888876";
            employeeWick.SeriesOfPassport = "PR -56";
            employeeWick.NumberOfPassport = 2367;
            employeeWick.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            employeeWick.Contract = "Принят на работу";
            employeeWick.Position = "Программист";
            employeeWick.Salary = 300;
            employees.Add(employeeWick);

            var employeeDave = new Employee();
            employeeDave.EmployeeId = Guid.NewGuid();
            employeeDave.FirstName = "SerializeEmployee";
            employeeDave.LastName = "Dave";
            employeeDave.Phone = "077888876";
            employeeDave.SeriesOfPassport = "PR -56";
            employeeDave.NumberOfPassport = 2367;
            employeeDave.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            employeeDave.Contract = "Принят на работу";
            employeeDave.Position = "Дизайнер";
            employeeDave.Salary = 300;
            employees.Add(employeeDave);

            var employeeSam = new Employee();
            employeeSam.EmployeeId = Guid.NewGuid();
            employeeSam.FirstName = "SerializeEmployee";
            employeeSam.LastName = "Wick";
            employeeSam.Phone = "077888876";
            employeeSam.SeriesOfPassport = "PR -56";
            employeeSam.NumberOfPassport = 2367;
            employeeSam.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            employeeSam.Contract = "Принят на работу";
            employeeSam.Position = "Тестировщик";
            employeeSam.Salary = 300;
            employees.Add(employeeSam);

            //Act

            await exportService.WriteSerializePersonToCsv(employees, pathToDirectory, "EmployeeSerializePractise.json");

            var employeesRead = await exportService.ReadSerializePersonFromCsv<List<Employee>>(pathToDirectory, "EmployeeSerializePractise.json");

            foreach (var employee in employeesRead)
            {
                await service.AddNewEmployee(employee);
            }

            //Assert
            Assert.Equal("SerializeEmployee", employeesRead.First().FirstName);
        }
        [Fact]
        public async Task WriteClientTest()
        {
            //Arrange
            var service = new ClientService();
            var filter = new ClientFilter() { PageSize = 1000 };
            var clients = await service.GetClientsAsync(filter);
            var pathToDirectory = @"C:\Users\Hi-tech\source\repos\.net-course-2022-chaban\ExportTool\ExportData\";
            var exportService = new ExportService();

            //Act
            await exportService.WriteClientToCsv(clients, pathToDirectory, "WriteClientPractise.csv");
            var clientsRead = await exportService.ReadClientFromCsv(pathToDirectory, "WriteClientPractise.csv");

            //Asssert
            Assert.Equal(clients.Count, clientsRead.Count);
        }

        [Fact]
        public async Task ReadClientTest()
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
            await exportService.WriteClientToCsv(clients, pathToDirectory, "ReadClientPractise.csv");

            var clientsRead = await exportService.ReadClientFromCsv(pathToDirectory, "ReadClientPractise.csv");
            foreach (var client in clientsRead)
            {
               await service.AddClientAsync(client);
            }

            //Assert
            Assert.NotEmpty(clientsRead);
        }
    }
}

