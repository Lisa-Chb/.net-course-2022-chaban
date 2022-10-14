using Bogus;
using Models;
using Services.Exceptions;
using Services;
using Xunit;

namespace ServiceTests
{
    public class EmployeeServiseTest
    {
        [Fact]
        public async Task AddGetEmployeeTest()
        {
            //Arrange
            var testDataGenerator = new TestDataGenerator();
            var generatorEmployee = testDataGenerator.CreateEmployeeListGenerator();
            var employees = generatorEmployee.Generate(10);

            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                FirstName = "Дмитрий",
                LastName = "Кузнецов",
                NumberOfPassport = 25422,
                SeriesOfPassport = "876755",
                Phone = "77906732",
                DateOfBirth = new DateTime(2000, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                Salary = 3000,
                Position = "Дизайнер",
                Contract = "Принят на работу"
            };

            var service = new EmployeeService();

            //Act
            foreach (var e in employees)
            {
                await service.AddNewEmployee(e);
            }          
         
            await service.AddNewEmployee(employee);
            var getEmployee = await service.GetEmployee(employee.EmployeeId);

            //Assert
            Assert.Equal(employee, getEmployee);
        }

        [Fact]
        public async Task DeleteEmployeeTest()
        {
            //Arrange
            var service = new EmployeeService();

            var employeeId = Guid.NewGuid();
            var employee = new Employee
            {
                FirstName = "Пауо",
                LastName = "Коэльо",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666666",
                Phone = "77956730",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 5,
                EmployeeId = employeeId,
                Salary = 9765,
                Position = "Программист",
                Contract = "Принят на работу"
            };

            //Act
            await service.AddNewEmployee(employee);

            var getEmployee = await service.GetEmployee(employeeId);
            Assert.True(getEmployee.SeriesOfPassport == employee.SeriesOfPassport);
            await service.DeleteEmployee(employeeId);

            //Assert
            await Assert.ThrowsAsync<PersonDoesntExistException>(async () => await service.GetEmployee(employeeId));
        }

        [Fact]
        public async Task UpdateEmployeeTest()
        {
            //Arrange
            var service = new EmployeeService();

            var employeeId = Guid.NewGuid();
            var employee = new Employee
            {
                FirstName = "Эдуард",
                LastName = "Гаврилов",
                NumberOfPassport = 7777,
                SeriesOfPassport = "666556",
                Phone = "77056730",
                DateOfBirth = new DateTime(1980, 8, 12).ToUniversalTime(),
                BonusDiscount = 15,
                EmployeeId = employeeId,
                Salary = 9760,
                Position = "Программист",
                Contract = "Принят на работу"
            };

            var updateEmployee = new Employee
            {
                FirstName = "Говард",
                LastName = "Лавкрафт",
                NumberOfPassport = 7777,
                SeriesOfPassport = "667766",
                Phone = "77799777",
                DateOfBirth = new DateTime(1996, 8, 12).ToUniversalTime(),
                BonusDiscount = 10,
                EmployeeId = employeeId,
                Salary = 9760,
                Position = "Программист",
                Contract = "Принят на работу"
            };

            //Act
            await service.AddNewEmployee(employee);
            var getEmployee = await service.GetEmployee(employeeId);
            Assert.Equal(employee, getEmployee);

            await service.UpdateEmployee(updateEmployee);

            var updatedEmpl = await service.GetEmployee(employeeId);

            //Assert
            Assert.Equal(updateEmployee.FirstName, updatedEmpl.FirstName);
        }
    }
}
