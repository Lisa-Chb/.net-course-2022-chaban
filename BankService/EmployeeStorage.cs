using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeStorage
    {
        readonly List<Employee> employees = new List<Employee>();

        public void AddNewEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public List<Employee> GetEmployeeList()
        {
            return employees;
        }
    }
}
