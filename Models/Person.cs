﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }      
        public int? NumberOfPassport { get; set; }
        public string SeriesOfPassport { get; set; }
        public string Phone  { get; set; }
        public DateTime DateOfBirth { get; set; }       

    }
}
