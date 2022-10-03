using Models;
using Services.Filtres;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceTests
{
    public class EmployeeStorageTests
    {
        [Fact]

        public void SelectEmployeeWithNameTest()
        {

            //Arrange
            var testEmployeeService = new EmployeeService(new EmployeeStorage());

            var employeeJohn = new Employee();
            employeeJohn.FirstName = "John";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 2367;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6);
            employeeJohn.Position = "Дизайнер";
            testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeJohnToo = new Employee();
            employeeJohnToo.FirstName = "John";
            employeeJohnToo.SeriesOfPassport = "PR -96";
            employeeJohnToo.NumberOfPassport = 2367;
            employeeJohnToo.DateOfBirth = new DateTime(1999, 5, 6);
            employeeJohnToo.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeJohnToo);

            var employeeEmily = new Employee();
            employeeEmily.FirstName = "Emily";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1998, 5, 6);
            employeeEmily.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.FirstName = "John";

            var expectedList = new List<Employee>();
            expectedList.AddRange(new Employee[] { employeeJohn, employeeJohnToo });

            //Act Assert
            Assert.Equal(testEmployeeService.GetEmployees(filter), expectedList);
        }


            [Fact]

        public void SelectEmployeeWithNumberOfPassportTest()
        {
            //Arrange
            var testEmployeeService = new EmployeeService(new EmployeeStorage());

            var employeeJohn = new Employee();
            employeeJohn.FirstName = "John";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 2367;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6);
            employeeJohn.Position = "Дизайнер";
            testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeJohnToo = new Employee();
            employeeJohnToo.FirstName = "John";
            employeeJohnToo.SeriesOfPassport = "PR -96";
            employeeJohnToo.NumberOfPassport = 2367;
            employeeJohnToo.DateOfBirth = new DateTime(1999, 5, 6);
            employeeJohnToo.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeJohnToo);

            var employeeEmily = new Employee();
            employeeEmily.FirstName = "Emily";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1998, 5, 6);
            employeeEmily.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.NumberOfPassport = 2367;

            var expectedList = new List<Employee>();
            expectedList.AddRange(new Employee[] { employeeJohn, employeeJohnToo });

            //Act Assert
            Assert.Equal(testEmployeeService.GetEmployees(filter), expectedList);
        }

        [Fact]

        public void SelectEmployeeWithDateRangeTest()
        {
            //Arrange
            var testEmployeeService = new EmployeeService(new EmployeeStorage());

            var employeeJohn = new Employee();
            employeeJohn.FirstName = "John";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 2367;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6);
            employeeJohn.Position = "Дизайнер";
            testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeJohnToo = new Employee();
            employeeJohnToo.FirstName = "John";
            employeeJohnToo.SeriesOfPassport = "PR -96";
            employeeJohnToo.NumberOfPassport = 2367;
            employeeJohnToo.DateOfBirth = new DateTime(1978, 5, 6);
            employeeJohnToo.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeJohnToo);

            var employeeEmily = new Employee();
            employeeEmily.FirstName = "Emily";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1956, 5, 6);
            employeeEmily.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.MinDateTime = new DateTime(1950, 1, 1);
            filter.MaxDateTime = new DateTime(1999, 1, 1);

            var expectedList = new List<Employee>();
            expectedList.AddRange(new Employee[] {employeeJohnToo, employeeEmily});

            //Act Assert
            Assert.Equal(testEmployeeService.GetEmployees(filter), expectedList);
        }

        [Fact]

        public void SelectEmployeeWithPositionTest()
        {

            //Arrange
            var testEmployeeService = new EmployeeService(new EmployeeStorage());

            var employeeJohn = new Employee();
            employeeJohn.FirstName = "John";
            employeeJohn.SeriesOfPassport = "PR -56";
            employeeJohn.NumberOfPassport = 2367;
            employeeJohn.DateOfBirth = new DateTime(2000, 5, 6);
            employeeJohn.Position = "Дизайнер";
            testEmployeeService.AddNewEmployee(employeeJohn);

            var employeeJohnToo = new Employee();
            employeeJohnToo.FirstName = "John";
            employeeJohnToo.SeriesOfPassport = "PR -96";
            employeeJohnToo.NumberOfPassport = 2367;
            employeeJohnToo.DateOfBirth = new DateTime(1999, 5, 6);
            employeeJohnToo.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeJohnToo);

            var employeeEmily = new Employee();
            employeeEmily.FirstName = "Emily";
            employeeEmily.SeriesOfPassport = "PR -56";
            employeeEmily.NumberOfPassport = 6865;
            employeeEmily.DateOfBirth = new DateTime(1998, 5, 6);
            employeeEmily.Position = "Программист";
            testEmployeeService.AddNewEmployee(employeeEmily);

            var filter = new EmployeeFilter();
            filter.Position = "Дизайнер";

            var expectedList = new List<Employee>();
            expectedList.Add(employeeJohn);

            //Act Assert
            Assert.Equal(testEmployeeService.GetEmployees(filter), expectedList);
        }
    }
}
