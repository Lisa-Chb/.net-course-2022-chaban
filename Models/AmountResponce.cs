using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AmountResponce
    {
        public int error { get; set; }
        public string error_message { get; set; }
        public decimal amount { get; set; }
    }
}
