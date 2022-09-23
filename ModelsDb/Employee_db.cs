using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ModelsDb
{
    [Table(name: "employee")]
    public class Employee_db 
    {
        [Column(name: "first_name")]
        public string FirstName { get; set; }


        [Column(name: "last_name")]
        public string LastName { get; set; }


        [Column(name: "number_of_passport")]
        public int? NumberOfPassport { get; set; }


        [Column(name: "series_of_passport")]
        public string SeriesOfPassport { get; set; }


        [Column(name: "phone")]
        public string Phone { get; set; }


        [Column(name: "date_of_birth")]
        public DateTime DateOfBirth { get; set; }


        [Column(name: "bonus_discount")]
        public int BonusDiscount { get; set; }

        [Key]
        [Column(name: "employee_Id")]
        public Guid EmployeeId { get; set; }

        [Column(name: "contract")]
        public string Contract { get; set; }

        [Column(name: "salary")]
        public int Salary { get; set; }

        [Column(name: "position")]
        public string Position { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Employee_db))
                return false;

            var employee = (Employee_db)obj;
            return employee.FirstName == FirstName &&
                employee.LastName == LastName &&
                employee.DateOfBirth == DateOfBirth &&
                employee.Phone == Phone &&
                employee.SeriesOfPassport == SeriesOfPassport &&
                employee.Position == Position &&
                employee.Contract == Contract &&
                employee.Salary == Salary;
        }

        public override int GetHashCode()
        {
            var hashCode1 = HashCode.Combine(FirstName, LastName, DateOfBirth, Phone, SeriesOfPassport, Position, Contract);
            return hashCode1 + Salary;
        }

    }
}