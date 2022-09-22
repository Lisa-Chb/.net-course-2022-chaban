using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client : Person
    {
        public Guid ClientId { get; set; }

        public List<Account> Accounts { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Client))
                return false;

            var client = (Client)obj;
            return client.FirstName == FirstName &&
                client.LastName == LastName &&
                client.DateOfBirth == DateOfBirth &&
                client.Phone == Phone &&
                client.SeriesOfPassport == SeriesOfPassport &&
                client.NumberOfPassport == NumberOfPassport &&
                client.BonusDiscount == BonusDiscount;

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, DateOfBirth, Phone, SeriesOfPassport, NumberOfPassport, BonusDiscount);
        }
    }
}

