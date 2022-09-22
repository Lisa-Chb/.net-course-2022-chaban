
namespace Models
{
    public class Currency
    {
        public Guid CurrencyId { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
