using Models.ModelsValidationExceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService
    {
        private List<Employee> _employees;
        public void AddNewEmployee(Employee employee)
        {
            if (employee.Age < 18)
                throw new EmployeeAgeValidationException("Лица до 18 лет не могут быть приняты на работу");

            if (string.IsNullOrEmpty(employee.SeriesOfPassport))
                throw new EmployeeSeriesOfPassportValidationException("Необходимо ввести серию паспорта");

            if (employee.NumberOfPassport == null)
                throw new EmployeeNumberOfPassportValidationException("Необходимо ввести номер паспорта");

            if (string.IsNullOrEmpty(employee.Position))
                throw new EmployeePositionValidationException("Необходимо указать занимаемую должность");

            _employees.Add(employee);
        }
    }
}
