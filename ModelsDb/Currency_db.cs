
using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace ModelsDb
{
    [Table(name: "currences")]
    public class Currency_db
    {
        [Key]
        [Column(name: "currency_Id")]
        public Guid CurrencyId { get; set; }


        [Column(name: "account_Id")]    
        public Guid AccountId { get; set; }


        [Column(name: "account")]
        public Account_db Account { get; set; }


        [Column(name: "name")]
        public string Name { get; set; }


        [Column(name: "code")]
        public int Code { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Currency_db))
                return false;

            var currency = (Currency_db)obj;
            return currency.CurrencyId == CurrencyId &&
                currency.AccountId == AccountId &&
                currency.Account == Account &&
                currency.Code == Code &&
                currency.Name == Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CurrencyId, Account, AccountId, Code, Name);         
        }
    }
}
