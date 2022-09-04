using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsValidationExceptions
{
    public class EmployeePositionValidationException : Exception
    {
        public EmployeePositionValidationException(string message)
        : base(message) { }
    }
}

