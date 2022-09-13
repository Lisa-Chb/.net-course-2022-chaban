using Models;
using Services.Exceptions;
using Services.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeStorage : IEmployeeStorage
    {
        public List<Employee> Data { get; }

        public EmployeeStorage( List<Employee> initData)
        {
            Data = initData;
        }

        public void Add(Employee employee)
        {
            if (Data.Contains(employee))
                throw new PersonAlreadyExistException("Данный работник уже существует");

            Data.Add(employee);
        }
        public void Delete(Employee employee)
        {
            if (!Data.Contains(employee))
                throw new PersonDoesntExistException("Указанного работника не существует");

             Data.Remove(employee);
        }
     
        public void Update(Employee employee)
        {         
            var employeeToUpdate = Data.FirstOrDefault(s => s.NumberOfPassport == employee.NumberOfPassport);

            if (!Data.Contains(employee))
                throw new PersonDoesntExistException("Указанного сотрудника не существует");

            Data.Remove(employeeToUpdate);
            Data.Add(employee);
        }
    }
}
