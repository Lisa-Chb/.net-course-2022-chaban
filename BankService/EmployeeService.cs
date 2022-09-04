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
        private List<Employee> _employes;
        public void AddNewEmployee(List<Employee> _employess, Employee employee)
        {
            try
            {
                _employess.Add(employee);
            }
            catch (EmployeeAgeValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EmployeeSeriesOfPassportValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EmployeeNumberOfPassportValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EmployeePositionValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
