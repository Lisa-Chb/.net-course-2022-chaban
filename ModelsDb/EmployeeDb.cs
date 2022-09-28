using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ModelsDb
{
    [Table(name: "employee")]
    public class EmployeeDb 
    {
        [Key]
        [Column(name: "employee_Id")]
        public Guid EmployeeId { get; set; }


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
  

        [Column(name: "contract")]
        public string Contract { get; set; }

        [Column(name: "salary")]
        public int Salary { get; set; }

        [Column(name: "position")]
        public string Position { get; set; }
    }
}