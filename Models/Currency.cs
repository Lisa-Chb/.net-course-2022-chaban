using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public struct Currency
    {
        public string Name { get; set; }
        public int Code { get; set; }
       public Currency(string Name, int Code)
        {
            this.Name = Name;
            this.Code = Code;
        }
    }
}
