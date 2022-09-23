using Models;

namespace Services.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {
        public List<Employee> Data { get; }

        public EmployeeStorage()
        {
            Data = new List<Employee>();
        }

        public void Add(Employee employee)
        {        
            Data.Add(employee);
        }
        public void Delete(Employee employee)
        {         
             Data.Remove(employee);
        }
     
        public void Update(Employee employee)
        {         
            var employeeToUpdate = Data.FirstOrDefault(s => s.NumberOfPassport == employee.NumberOfPassport);

            Data.Remove(employeeToUpdate);
            Data.Add(employee);
        }
    }
}
