

using System.Text;

namespace Services.Filtres
{
    public  class ClientFilter
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public int? NumberOfPassport { get; set; }

        public DateTime? MinDateTime { get; set; }

        public DateTime? MaxDateTime { get; set; }

        public int? BonusDiscount { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; }
    }
}
