using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsValidationExceptions
{
    public class ClientSeriesOfPassportValidationException : Exception
    {
        public ClientSeriesOfPassportValidationException(string message)
        : base(message) { }
    }
}
