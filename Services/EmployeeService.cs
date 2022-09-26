
using Microsoft.EntityFrameworkCore;
using ModelsDb;
using ModelsDb.Data;
using Services.Exceptions;
using Services.Filtres;

namespace Services
{
    public class EmployeeService
    {
        readonly ApplicationContext _dbContext;

        public EmployeeService()
        {
            _dbContext = new ApplicationContext();
        }

        public EmployeeDb GetEmployee(Guid employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);

            if (employee == null)
                throw new PersonDoesntExistException("Указанного сотрудника не сущетсвует");

            return employee;
        }
        public void AddNewEmployee(EmployeeDb employee)
        {
            if (_dbContext.Employees.Contains(employee))
                throw new PersonAlreadyExistException("Данный работник уже существует");

            if ((DateTime.Now - employee.DateOfBirth).Days / 365 < 18)
                throw new PersonAgeValidationException("Лица до 18 лет не могут быть приняты на работу");

            if (string.IsNullOrEmpty(employee.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (employee.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            if (string.IsNullOrEmpty(employee.Position))
                throw new EmployeePositionValidationException("Необходимо указать занимаемую должность");

            _dbContext.Add(employee);
            _dbContext.SaveChanges();
        }

        public void AddNewEmployee(List<EmployeeDb> employees)
        {
            foreach (var employee in employees)
            {
                if (_dbContext.Employees.Contains(employee))
                    throw new PersonAlreadyExistException("Данный работник уже существует");

                if ((DateTime.Now - employee.DateOfBirth).Days / 365 < 18)
                    throw new PersonAgeValidationException("Лица до 18 лет не могут быть приняты на работу");

                if (string.IsNullOrEmpty(employee.SeriesOfPassport))
                    throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

                if (employee.NumberOfPassport == null)
                    throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

                if (string.IsNullOrEmpty(employee.Position))
                    throw new EmployeePositionValidationException("Необходимо указать занимаемую должность");

                _dbContext.Add(employee);
            }

            _dbContext.SaveChanges();
        }

        public void DeleteEmployee(Guid employeeId)
        {
            var requiredEmployee = _dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);

            if (requiredEmployee == null)
                throw new PersonDoesntExistException("Указанного сотрудника не сущетсвует");
            else
                _dbContext.Employees.Remove(requiredEmployee);

            _dbContext.SaveChanges();
        }

        public void UpdateEmployee(EmployeeDb employee)
        {
            var priorEmployee = _dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employee.EmployeeId);

            if (!_dbContext.Employees.Contains(priorEmployee))
                throw new PersonAlreadyExistException("Данного сотрудника не существует");

            priorEmployee.FirstName = employee.FirstName;
            priorEmployee.LastName = employee.LastName;
            priorEmployee.NumberOfPassport = employee.NumberOfPassport;
            priorEmployee.SeriesOfPassport = employee.SeriesOfPassport;
            priorEmployee.Phone = employee.Phone;
            priorEmployee.Position = employee.Position;
            priorEmployee.Salary = employee.Salary;
            priorEmployee.DateOfBirth = employee.DateOfBirth;
            priorEmployee.Contract = employee.Contract;
            priorEmployee.BonusDiscount = employee.BonusDiscount;
            
            _dbContext.SaveChanges();
        }


        public List<EmployeeDb> GetEmployees(EmployeeFilter filter)
        {
            var employees = _dbContext.Employees.AsQueryable();

            if (filter.FirstName != null)
                employees = employees.Where(s => s.FirstName == filter.FirstName);

            if (filter.LastName != null)
                employees = employees.Where(s => s.LastName == filter.LastName);

            if (filter.NumberOfPassport != null)
                employees = employees.Where(s => s.NumberOfPassport == filter.NumberOfPassport);

            if (filter.MinDateTime != null)
                employees = employees.Where(s => s.DateOfBirth >= filter.MinDateTime);

            if (filter.MaxDateTime != null)
                employees = employees.Where(s => s.DateOfBirth <= filter.MaxDateTime);

            if (filter.Position != null)
                employees = employees.Where(x => x.Position == filter.Position);

            var paginatedEmployees = employees.Skip((filter.Page - 1)* filter.PageSize).Take(filter.PageSize).ToList();

            return paginatedEmployees;
        }
    }
}