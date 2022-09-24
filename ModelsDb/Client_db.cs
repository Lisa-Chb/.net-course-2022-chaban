using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsDb
{
    [Table(name: "clients")]
    public class Client_db
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
        [Column(name: "client_Id")]
        public Guid ClientId { get; set; }

        [Column(name: "accounts")]
        public List<Account_db> Accounts { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Client_db))
                return false;

            var client = (Client_db)obj;
            return client.FirstName == FirstName &&
                client.LastName == LastName &&
                client.DateOfBirth == DateOfBirth &&
                client.Phone == Phone &&
                client.SeriesOfPassport == SeriesOfPassport &&
                client.NumberOfPassport == NumberOfPassport &&
                client.BonusDiscount == BonusDiscount &&
                client.ClientId == ClientId &&
                client.Accounts == Accounts;

        }
        public override int GetHashCode()
        {
            var hashCode =  HashCode.Combine(FirstName, LastName, DateOfBirth, Phone, SeriesOfPassport, NumberOfPassport, Accounts, ClientId);
            return hashCode + BonusDiscount;
        }
    }

}
    
