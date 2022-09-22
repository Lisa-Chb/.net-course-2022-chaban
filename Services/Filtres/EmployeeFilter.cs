

namespace Services.Filtres
{
    public class EmployeeFilter
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public int? NumberOfPassport { get; set; }

        public DateTime? MinDateTime { get; set; }

        public DateTime? MaxDateTime { get; set; }

        public string Position { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
