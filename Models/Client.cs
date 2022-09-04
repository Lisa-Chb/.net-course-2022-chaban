using Models.ModelsValidationExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client : Person
    {
        public int AccountNumber { get; set; }

        public override int Age
        {
            get => Age;
            set
            {
                if (value < 18)
                    throw new ClientAgeValidationException("Лицам до 18 регистрация запрещена");
                else
                    Age = value;
            }
        }

        public override string SeriesOfPassport
        {
            get => SeriesOfPassport;
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ClientSeriesOfPassportValidationException("Необходимо ввести серию паспорта");
                else
                    SeriesOfPassport = value;
            }
        }

        public override int? NumberOfPassport
        {
            get => NumberOfPassport;
            set
            {
                if ( value == null)
                    throw new ClientNumberOfPassportValidationException("Необходимо ввести номер паспорта");
                else
                    NumberOfPassport = value;
            }           
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Client))
                return false;

            var client = (Client)obj;
            return client.FirstName == FirstName &&
                client.LastName == LastName &&
                client.Age == Age &&
                client.AccountNumber == AccountNumber &&
                client.DateOfBirth == DateOfBirth &&
                client.Phone == Phone &&
                client.SeriesOfPassport == SeriesOfPassport &&
                client.NumberOfPassport == NumberOfPassport;

        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, Age, AccountNumber, DateOfBirth, Phone, SeriesOfPassport, NumberOfPassport);
        }
    }
}

