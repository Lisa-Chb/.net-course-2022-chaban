using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
    }
}
