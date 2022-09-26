using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;


namespace ModelsDb
{
    [Table(name:"accounts")]
    public class AccountDb
    {
        [Key]
        [Column(name: "account_id")]
        public Guid AccountId { get; set; }


        [Column(name: "client_id")]
        public Guid Clientid { get; set; }


        [Column(name: "client")]
        public ClientDb Client { get; set; }


        [Column(name: "currency")]
        public List<CurrencyDb> Currencys { get; set; }


        [Column(name: "amount")]
        public int Amount { get; set; }
    }
}
