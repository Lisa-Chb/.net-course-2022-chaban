using Models;

namespace Services
{
    public class BankService<TPerson> where TPerson : Person
    {
        private readonly List<TPerson> personBlackList;

        public BankService(List<TPerson> personList)
        {
            this.personBlackList = personList;
        }

        public void AddBonus(TPerson person)
        {
            person.BonusDiscount++;
        }

        public void AddToBlackList(TPerson person) 
        {
           personBlackList.Add(person);
        }

        public bool IsPersonInBlackList<TPerosn>(TPerson person)
        {
            if (personBlackList.Contains(person))
                return true;
            else
                return false;
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