using Models;
using Services.Exceptions;
using Services.Filtres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService
    {
       private readonly EmployeeStorage _employeeStorage;

        public EmployeeService (EmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        public void AddNewEmployee(Employee employee)
        {
            if ((DateTime.Now - employee.DateOfBirth).Days / 365 < 18)
                throw new PersonAgeValidationException("Лица до 18 лет не могут быть приняты на работу");

            if (string.IsNullOrEmpty(employee.SeriesOfPassport))
                throw new PersonSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (employee.NumberOfPassport == null)
                throw new PersonNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            if (string.IsNullOrEmpty(employee.Position))
                throw new EmployeePositionValidationException("Необходимо указать занимаемую должность");

            _employeeStorage.AddNewEmployee(employee);
        }

        public List<Employee> GetEmployees(EmployeeFilter filter)
        {
            var employeeList = _employeeStorage.GetEmployeeList();

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
