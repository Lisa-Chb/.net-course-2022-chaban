
using Microsoft.EntityFrameworkCore;
using Models;
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

        public Employee GetEmployee(Guid employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);

            if (employee == null)
                throw new PersonDoesntExistException("Указанного сотрудника не сущетсвует");

            return EmployeeMapping(employee);
        }    

        public void AddNewEmployee(Employee employee)
        {
            var employeeDb = EmployeeMapping(employee);

            if (_dbContext.Employees.Contains(employeeDb))
                throw new PersonAlreadyExistException("Данный работник уже существует");

            if ((DateTime.Now - employeeDb.DateOfBirth).Days / 365 < 18)
                throw new PersonAgeValidationException("Лица до 18 лет не могут быть приняты на работу");

            if (string.IsNullOrEmpty(employeeDb.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (employeeDb.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            if (string.IsNullOrEmpty(employeeDb.Position))
                throw new EmployeePositionValidationException("Необходимо указать занимаемую должность");

            _dbContext.Add(employeeDb);

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

        public void UpdateEmployee(Employee employee)
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


        public List<Employee> GetEmployees(EmployeeFilter filter)
        {
            var employeesDb = _dbContext.Employees.AsQueryable();

            if (filter.FirstName != null)
                employeesDb = employeesDb.Where(s => s.FirstName == filter.FirstName);

            if (filter.LastName != null)
                employeesDb = employeesDb.Where(s => s.LastName == filter.LastName);

            if (filter.NumberOfPassport != null)
                employeesDb = employeesDb.Where(s => s.NumberOfPassport == filter.NumberOfPassport);

            if (filter.MinDateTime != null)
                employeesDb = employeesDb.Where(s => s.DateOfBirth >= filter.MinDateTime);

            if (filter.MaxDateTime != null)
                employeesDb = employeesDb.Where(s => s.DateOfBirth <= filter.MaxDateTime);

            if (filter.Position != null)
                employeesDb = employeesDb.Where(x => x.Position == filter.Position);

            var paginatedEmployees = employeesDb.Skip((filter.Page - 1)* filter.PageSize).Take(filter.PageSize).ToList();

            var employees = new List<Employee>();
            foreach (var employee in paginatedEmployees)
            {
                employees.Add(EmployeeMapping(employee));
            }

            return employees;
        }

        private EmployeeDb EmployeeMapping(Employee employee)
        {       
               var employeeDb = new EmployeeDb
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    NumberOfPassport = employee.NumberOfPassport,
                    SeriesOfPassport = employee.SeriesOfPassport,
                    Phone = employee.Phone,
                    DateOfBirth = employee.DateOfBirth,
                    BonusDiscount = employee.BonusDiscount,
                    Salary = employee.Salary,
                    Position = employee.Position,
                    Contract = employee.Contract,
                    EmployeeId = employee.EmployeeId
                };
            
            return employeeDb;
        }

        private Employee EmployeeMapping(EmployeeDb employeeDb)
        {
            var employee = new Employee
            {
                FirstName = employeeDb.FirstName,
                LastName = employeeDb.LastName,
                NumberOfPassport = employeeDb.NumberOfPassport,
                SeriesOfPassport = employeeDb.SeriesOfPassport,
                Phone = employeeDb.Phone,
                DateOfBirth = employeeDb.DateOfBirth,
                BonusDiscount = employeeDb.BonusDiscount,
                Salary = employeeDb.Salary,
                Position = employeeDb.Position,
                Contract = employeeDb.Contract,
                EmployeeId = employeeDb.EmployeeId
            };

            return employee;
        }
    }
}