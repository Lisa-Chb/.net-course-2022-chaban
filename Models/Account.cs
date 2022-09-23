

namespace Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public int Amount { get; set; }
        public Guid Clientid { get; set; }
        public Client Client { get; set; }
        public Currency Currency { get; set; }
    }
}
