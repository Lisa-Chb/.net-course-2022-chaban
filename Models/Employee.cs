using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : Person
    {
        public string Position { get; set; }
        public string Contract { get; set; }
        public int Salary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Employee))
                return false;

            var employee = (Employee)obj;
            return employee.FirstName == FirstName &&
                employee.LastName == LastName &&
                employee.Age == Age &&
                employee.DateOfBirth == DateOfBirth &&
                employee.Phone == Phone &&
                employee.SeriesOfPassport == SeriesOfPassport &&
                employee.Position == Position &&
                employee.Contract == Contract &&
                employee.Salary == Salary;

        }

        public override int GetHashCode()
        {            
          var hashCode1 = HashCode.Combine(FirstName, LastName, Age, DateOfBirth, Phone, SeriesOfPassport, Position, Contract);
            return hashCode1 + Salary;
        }

    }
}
