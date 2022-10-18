using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Services.Exceptions;
using ModelsDb;

namespace ServiceTests
{
    public class EmployeeServiceTest
    { 
        [Fact]
        public async Task EmployeeAgeValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutAge = new Employee();
            employeeWithoutAge.DateOfBirth = new DateTime(year: 2007, 5, 6);
            employeeWithoutAge.SeriesOfPassport = "I-ПР";
            employeeWithoutAge.NumberOfPassport = 356223435;
            employeeWithoutAge.Position = "Программист";

            //Act Assert
            var testEmployeeService = new EmployeeService();
            await Assert.ThrowsAsync<PersonAgeValidationException>(async() => await testEmployeeService.AddNewEmployee(employeeWithoutAge));       
        }

        [Fact]
        public async Task EmployeeSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutSeriesOfPassort = new Employee();
            employeeWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            employeeWithoutSeriesOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);
            employeeWithoutSeriesOfPassort.Position = "Программист";

            //Act Assert
            var testEmployeeService = new EmployeeService();           
            await Assert.ThrowsAsync<PersonSeriesOfPassportValidationException>(async() => await testEmployeeService.AddNewEmployee(employeeWithoutSeriesOfPassort));
        }

        [Fact]
        public async Task EmployeeNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutNumberOfPassort = new Employee();
            employeeWithoutNumberOfPassort.DateOfBirth = new DateTime(year: 1998, 5, 5);
            employeeWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            employeeWithoutNumberOfPassort.Position = "Программист";

            //Act Assert
            var testEmployeeService = new EmployeeService();
            await Assert.ThrowsAsync<PersonNumberOfPassportValidationException>(async() => await testEmployeeService.AddNewEmployee(employeeWithoutNumberOfPassort));
        }

        [Fact]
        public async Task EmployeePositionValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutPosition = new Employee();
            employeeWithoutPosition.DateOfBirth = new DateTime(year: 1998, 5, 5);
            employeeWithoutPosition.SeriesOfPassport = "I-ПР";
            employeeWithoutPosition.NumberOfPassport = 3264567;

            //Act Assert
            var testEmployeeService = new EmployeeService();
            await Assert.ThrowsAsync<EmployeePositionValidationException>(async() => await testEmployeeService.AddNewEmployee(employeeWithoutPosition));
        }      
    }
}

