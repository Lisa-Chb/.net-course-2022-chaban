using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
