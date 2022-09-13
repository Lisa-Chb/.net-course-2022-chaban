using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Services.Exceptions;

namespace ServiceTests
{
    public class EmployeeServiceTest
    {
        [Fact]

        public void EmployeeAgeValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutAge = new Employee();
            employeeWithoutAge.DateOfBirth = new DateTime(year: 2007, 5, 6);
            employeeWithoutAge.SeriesOfPassport = "I-ПР";
            employeeWithoutAge.NumberOfPassport = 356223435;
            employeeWithoutAge.Position = "Программист";

            //Act Assert
            var employeeStorage = new EmployeeStorage(new List<Employee>());
            EmployeeService testEmployeeService = new EmployeeService(employeeStorage);
            Assert.Throws<PersonAgeValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutAge));
        }

        [Fact]

        public void EmployeeSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutSeriesOfPassort = new Employee();
            employeeWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            employeeWithoutSeriesOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);
            employeeWithoutSeriesOfPassort.Position = "Программист";

            //Act Assert
            var employeeStorage = new EmployeeStorage(new List<Employee>());
            EmployeeService testEmployeeService = new EmployeeService(employeeStorage);
            Assert.Throws<PersonSeriesOfPassportValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutSeriesOfPassort));
        }

        [Fact]

        public void EmployeeNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutNumberOfPassort = new Employee();
            employeeWithoutNumberOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);
            employeeWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            employeeWithoutNumberOfPassort.Position = "Программист";

            //Act Assert
            var employeeStorage = new EmployeeStorage(new List<Employee>());
            EmployeeService testEmployeeService = new EmployeeService(employeeStorage);
            Assert.Throws<PersonNumberOfPassportValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutNumberOfPassort));
        }

        [Fact]

        public void EmployeePositionValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutPosition = new Employee();
            employeeWithoutPosition.DateOfBirth = new DateTime(year: 1998, 5, 5);
            employeeWithoutPosition.SeriesOfPassport = "I-ПР";
            employeeWithoutPosition.NumberOfPassport = 3264567;

            //Act Assert
            var employeeStorage = new EmployeeStorage(new List<Employee>());
            EmployeeService testEmployeeService = new EmployeeService(employeeStorage); ;
            Assert.Throws<EmployeePositionValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutPosition));
        }
    }
}

