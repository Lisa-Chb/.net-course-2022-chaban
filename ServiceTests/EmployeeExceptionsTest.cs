using Models.ModelsValidationExceptions;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServiceTests
{
    public class EmployeeExceptionsTest
    {
        [Fact]

        public void EmployeeAgeValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutAge = new Employee();
            employeeWithoutAge.SeriesOfPassport = "I-ПР";
            employeeWithoutAge.NumberOfPassport = 356223435;
            employeeWithoutAge.Position = "Программист";

            //Act Assert
            EmployeeService testEmployeeService = new EmployeeService();
            Assert.Throws<PersonAgeValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutAge));
        }

        [Fact]

        public void EmployeeSeriesOfPassportValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutSeriesOfPassort = new Employee();
            employeeWithoutSeriesOfPassort.NumberOfPassport = 356223435;
            employeeWithoutSeriesOfPassort.Age = 20;
            employeeWithoutSeriesOfPassort.Position = "Программист";

            //Act Assert
            EmployeeService testEmployeeService = new EmployeeService();
            Assert.Throws<PersonSeriesOfPassportValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutSeriesOfPassort));
        }

        [Fact]

        public void EmployeeNumberOfPassportValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutNumberOfPassort = new Employee();
            employeeWithoutNumberOfPassort.Age = 20;
            employeeWithoutNumberOfPassort.SeriesOfPassport = "I-ПР";
            employeeWithoutNumberOfPassort.Position = "Программист";

            //Act Assert
            EmployeeService testEmployeeService = new EmployeeService();
            Assert.Throws<PersonNumberOfPassportValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutNumberOfPassort));
        }

        [Fact]

        public void EmployeePositionValidationExceptionTest()
        {
            //Arrange
            var employeeWithoutPosition = new Employee();
            employeeWithoutPosition.Age = 20;
            employeeWithoutPosition.SeriesOfPassport = "I-ПР";
            employeeWithoutPosition.NumberOfPassport = 3264567;

            //Act Assert
            EmployeeService testEmployeeService = new EmployeeService();
            Assert.Throws<EmployeePositionValidationException>(() => testEmployeeService.AddNewEmployee(employeeWithoutPosition));
        }
    }
}

