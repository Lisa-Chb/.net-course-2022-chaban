using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;


namespace ModelsDb
{
    [Table(name:"accounts")]
    public class Account_db
    {
        [Key]
        [Column(name: "account_id")]
        public Guid AccountId { get; set; }


        [Column(name: "amount")]
        public int Amount { get; set; }


        [Column(name: "client_id")]
        public Guid Clientid { get; set; }



        [Column(name: "client")]
        public Client_db Client { get; set; }


        [Column(name: "currency")]
        public Currency_db Currency { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Account_db))
                return false;

            var account = (Account_db)obj;
            return account.Client == Client && 
                account.Amount == Amount && 
                account.Clientid == Clientid &&
                account.AccountId == AccountId &&
                account.Currency == Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Client, Amount, Clientid, AccountId, Currency);      
        }
    }
}
