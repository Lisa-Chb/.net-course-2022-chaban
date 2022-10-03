using Bogus.DataSets;
using Models;
using Services.Exceptions;
using Services.Filtres;
using Services.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService
    {
       private readonly IEmployeeStorage _employeeStorage;

        public EmployeeService (IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void AddNewEmployee(Employee employee)
        {
            if (_employeeStorage.Data.Contains(employee))
                throw new PersonAlreadyExistException("Данный работник уже существует");

            if ((DateTime.Now - employee.DateOfBirth).Days / 365 < 18)
                throw new PersonAgeValidationException("Лица до 18 лет не могут быть приняты на работу");

            if (string.IsNullOrEmpty(employee.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (employee.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            if (string.IsNullOrEmpty(employee.Position))
                throw new EmployeePositionValidationException("Необходимо указать занимаемую должность");

            _employeeStorage.Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            if (!_employeeStorage.Data.Contains(employee))
                throw new PersonDoesntExistException("Указанного работника не существует");

            _employeeStorage.Delete(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            if (!_employeeStorage.Data.Contains(employee))
                throw new PersonDoesntExistException("Указанного сотрудника не существует");

            _employeeStorage.Update(employee);
        }

        public List<Employee> GetEmployees(EmployeeFilter filter)
        {
            var employeeList = _employeeStorage.Data;

            var result = employeeList.ToArray();

            if (filter.FirstName != null)
                result = result.Where(s => s.FirstName == filter.FirstName).ToArray();

            if (filter.LastName != null)
                result = result.Where(s => s.LastName == filter.LastName).ToArray();

            if (filter.NumberOfPassport != null)
                result = result.Where(s => s.NumberOfPassport == filter.NumberOfPassport).ToArray();

            if (filter.MinDateTime != null)
                result = result.Where(s => s.DateOfBirth >= filter.MinDateTime).ToArray();

            if (filter.MaxDateTime != null)
                result = result.Where(s => s.DateOfBirth <= filter.MaxDateTime).ToArray();

            if (filter.Position != null)
                result = result.Where(x => x.Position == filter.Position).ToArray();

            return new List<Employee> (result);
        }   
    }
}
