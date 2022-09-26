
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsDb
{
    [Table(name: "currences")]
    public class CurrencyDb
    {
        [Key]
        [Column(name: "currency_Id")]
        public Guid CurrencyId { get; set; }


        [Column(name: "account_Id")]    
        public Guid AccountId { get; set; }


        [Column(name: "account")]
        public AccountDb Account { get; set; }


        [Column(name: "name")]
        public string Name { get; set; }


        [Column(name: "code")]
        public int Code { get; set; }
    }
}
