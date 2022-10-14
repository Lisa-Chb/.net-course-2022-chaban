using Services.Filtres;
using Services;
using Xunit;
using Models;

namespace ServiceTests
{
    public class EmployeeStorageTests
    {
        [Fact]
        public async Task SelectEmployeeWithNameTest()
        {
            //Arrange
            var testEmployeeService = new EmployeeService();

            var employeeJohn = new Employee();
            employeeJohn.EmployeeId = Guid.NewGuid();
            employeeJohn.FirstName = "John";
            employeeJohn.LastName = "Wick";
            employeeJohn.Phone = "078666650";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 2367;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            employeeJohn.Position = "Дизайнер";
            employeeJohn.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeTestName = new Employee();
            employeeTestName.EmployeeId = Guid.NewGuid();
            employeeTestName.FirstName = "TestName";
            employeeTestName.LastName = "Wick";
            employeeTestName.Phone = "078666650";
            employeeTestName.SeriesOfPassport = "PR -96";
            employeeTestName.NumberOfPassport = 2367;
            employeeTestName.DateOfBirth = new DateTime(1999, 5, 6).ToUniversalTime();
            employeeTestName.Position = "Программист";
            employeeTestName.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeTestName);

            var employeeEmily = new Employee();
            employeeEmily.EmployeeId = Guid.NewGuid();
            employeeEmily.FirstName = "Emily";
            employeeEmily.LastName = "Wick";
            employeeEmily.Phone = "078666650";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1998, 5, 6).ToUniversalTime();
            employeeEmily.Position = "Программист";
            employeeEmily.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.FirstName = "TestName";
            filter.PageSize = 10;

            //Act Assert
            var expectedClients = await testEmployeeService.GetEmployees(filter);
            Assert.Equal("TestName", expectedClients.FirstOrDefault().FirstName);
        }


        [Fact]
        public async Task SelectEmployeeWithNumberOfPassportTest()
        {
            //Arrange
            var testEmployeeService = new EmployeeService();

            var employeeJohn = new Employee();
            employeeJohn.EmployeeId = Guid.NewGuid();
            employeeJohn.FirstName = "John";
            employeeJohn.LastName = "Wick";
            employeeJohn.Phone = "078666650";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 0000;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            employeeJohn.Position = "Дизайнер";
            employeeJohn.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeJohnToo = new Employee();
            employeeJohnToo.EmployeeId = Guid.NewGuid();
            employeeJohnToo.FirstName = "John";
            employeeJohnToo.LastName = "Wick";
            employeeJohnToo.Phone = "078666651";
            employeeJohnToo.SeriesOfPassport = "PR -96";
            employeeJohnToo.NumberOfPassport = 2369;
            employeeJohnToo.DateOfBirth = new DateTime(1999, 5, 6).ToUniversalTime();
            employeeJohnToo.Position = "Программист";
            employeeJohnToo.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeJohnToo);

            var employeeEmily = new Employee();
            employeeEmily.EmployeeId = Guid.NewGuid();
            employeeEmily.FirstName = "Emily";
            employeeEmily.LastName = "Wick";
            employeeEmily.Phone = "078666652";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1998, 5, 6).ToUniversalTime();
            employeeEmily.Position = "Программист";
            employeeEmily.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.NumberOfPassport = 0000;
            filter.PageSize = 10;

            //Act Assert
            var expectedEmployees = await testEmployeeService.GetEmployees(filter);
            var expectedNumber = expectedEmployees.FirstOrDefault().NumberOfPassport;
            Assert.Equal(expectedNumber, 0000);
        }

        /*
        [Fact]

        public void SelectEmployeeWithDateRangeTest()
        {
            //Arrange
            var testEmployeeService = new EmployeeService();

            var employeeJohn = new EmployeeDb();
            employeeJohn.EmployeeId = Guid.NewGuid();
            employeeJohn.FirstName = "John";
            employeeJohn.LastName = "Wick";
            employeeJohn.Phone = "078666653";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 2367;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            employeeJohn.Position = "Дизайнер";
            employeeJohn.Contract = "Принят на работу";
            testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeJohnToo = new EmployeeDb();
            employeeJohnToo.EmployeeId = Guid.NewGuid();
            employeeJohnToo.FirstName = "John";
            employeeJohnToo.LastName = "Wick";
            employeeJohnToo.Phone = "078666654";
            employeeJohnToo.SeriesOfPassport = "PR -96";
            employeeJohnToo.NumberOfPassport = 2367;
            employeeJohnToo.DateOfBirth = new DateTime(1978, 5, 6).ToUniversalTime();
            employeeJohnToo.Position = "Программист";
            employeeJohnToo.Contract = "Принят на работу";
            testEmployeeService.AddNewEmployee(employeeJohnToo);

            var employeeEmily = new EmployeeDb();
            employeeEmily.EmployeeId = Guid.NewGuid();
            employeeEmily.FirstName = "Emily";
            employeeEmily.LastName = "Wick";
            employeeEmily.Phone = "078666655";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1956, 5, 6).ToUniversalTime();
            employeeEmily.Position = "Программист";
            employeeEmily.Contract = "Принят на работу";
            testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.MinDateTime = new DateTime(1950, 1, 1).ToUniversalTime();
            filter.MaxDateTime = new DateTime(1999, 1, 1).ToUniversalTime();

            var expectedList = new List<EmployeeDb>();
            expectedList.AddRange(new EmployeeDb[] {employeeJohnToo, employeeEmily});

            //Act Assert
            Assert.Equal(testEmployeeService.GetEmployees(filter), expectedList);
        }
        */

        [Fact]
        public async Task SelectEmployeeWithPositionTest()
        {
            //Arrange
            var testEmployeeService = new EmployeeService();

            var employeeJohn = new Employee();
            employeeJohn.EmployeeId = Guid.NewGuid();
            employeeJohn.FirstName = "John";
            employeeJohn.LastName = "Wick";
            employeeJohn.Phone = "078666656";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 2367;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6).ToUniversalTime();
            employeeJohn.Position = "Дизайнер";
            employeeJohn.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeJohnToo = new Employee();
            employeeJohnToo.EmployeeId = Guid.NewGuid();
            employeeJohnToo.FirstName = "John";
            employeeJohnToo.LastName = "Wick";
            employeeJohnToo.Phone = "078666657";
            employeeJohnToo.SeriesOfPassport = "PR -96";
            employeeJohnToo.NumberOfPassport = 2367;
            employeeJohnToo.DateOfBirth = new DateTime(1999, 5, 6).ToUniversalTime();
            employeeJohnToo.Position = "Программист";
            employeeJohnToo.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeJohnToo);

            var employeeEmily = new Employee();
            employeeEmily.EmployeeId = Guid.NewGuid();
            employeeEmily.FirstName = "Emily";
            employeeEmily.LastName = "Wick";
            employeeEmily.Phone = "078666658";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1998, 5, 6).ToUniversalTime();
            employeeEmily.Position = "Тестовая должность";
            employeeEmily.Contract = "Принят на работу";
            await testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.Position = "Тестовая должность";
            filter.PageSize = 10;

            //Act Assert
            var expectedEmployees = await testEmployeeService.GetEmployees(filter);
            var expectedPosition = expectedEmployees.FirstOrDefault().Position;
            Assert.Equal("Тестовая должность", expectedPosition);
        }
    }
}
    

