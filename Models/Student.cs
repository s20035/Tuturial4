﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDTUT3.Models
{
    public class Student
    {
        public string Firstname { get; set; }

        public string LastName { get; set; }

        public string Studies { get; set; }

        public DateTime BirthDate { get; set; }
        public int Semester { get; set; }
    }
}
