using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public class PersonDoesntExistException : Exception
    {
        public PersonDoesntExistException(string message)
        : base(message) { }
    }
}
