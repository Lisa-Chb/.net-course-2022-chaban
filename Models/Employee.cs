using Models.ModelsValidationExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : Person
    {
        public string Contract { get; set; }
        public int Salary { get; set; }

        public string Position
        {
            get => Position;
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new EmployeePositionValidationException("Необходимо указать должность");
                else
                    Position = value;
            }
        }
       
        public override int Age
        {
            get => Age;
            set
            {
                if (value < 18)
                    throw new EmployeeAgeValidationException("Лица до 18 лет не могут быть приняты на работу");
                else
                    Age = value;
            }
        }

        public override string SeriesOfPassport
        {
            get => SeriesOfPassport;
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new EmployeeSeriesOfPassportValidationException("Необходимо ввести серию паспорта");
                else
                    SeriesOfPassport = value;
            }
        }

        public override int? NumberOfPassport
        {
            get => NumberOfPassport;
            set
            {
                if (value == null)
                    throw new EmployeeNumberOfPassportValidationException("Необходимо ввести номер паспорта");
                else
                    NumberOfPassport = value;
            }
        }

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
