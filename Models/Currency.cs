
namespace Models
{
    public class Currency
    {
        public int CurrencyCode { get; set; }    
        public List<Account> Account { get; set; }
        public string Name { get; set; }      
    }
}
