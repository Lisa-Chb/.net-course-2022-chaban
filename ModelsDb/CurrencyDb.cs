
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsDb
{
    [Table(name: "currencies")]
    public class CurrencyDb
    {
        [Key]
        [Column(name: "currency_code")]
        public int Code { get; set; }


        [Column(name: "name")]
        public string Name { get; set; }


        [Column(name: "accounts")]
        public List<AccountDb> Account { get; set; }
    }
}
