using Models;

namespace Services.Storages
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
       public List<Employee> Data { get; }
    }
}
