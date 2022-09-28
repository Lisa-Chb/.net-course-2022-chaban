using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsDb
{
    [Table(name: "clients")]
    public class ClientDb
    {
        [Key]
        [Column(name: "client_Id")]
        public Guid ClientId { get; set; }


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


        [Column(name: "accounts")]
        public List<AccountDb> Accounts { get; set; }
    }
}
    
