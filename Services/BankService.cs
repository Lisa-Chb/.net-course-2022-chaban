using Models;

namespace Services
{
    public class BankService<TPerson> where TPerson : Person
    {
        private List<TPerson> _personBlackList = new List<TPerson>();   

        public void AddBonus(TPerson person)
        {
            person.BonusDiscount++;
        }

        public void AddToBlackList(TPerson person) 
        {
           _personBlackList.Add(person);
        }

        public bool IsPersonInBlackList<TPerosn>(TPerson person)
        {
            return _personBlackList.Contains(person);
               
        }

        public int CalculateOwnerSalary(int ownerCount, float bankProfit, float bankExpenses)
        {
            return Convert.ToInt32((bankProfit - bankExpenses) / ownerCount);
        }

        public  Employee ClientToEmployee(Client client)
        {

            Employee employee = new Employee()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                SeriesOfPassport = client.SeriesOfPassport,
                Phone = client.Phone,
                DateOfBirth = client.DateOfBirth
            };

            return employee;
        }
    }
}