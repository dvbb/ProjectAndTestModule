﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyAttribute.Models
{
    internal class Dustman : Person
    {
        public Dustman()
        {
            Salary = 5000;
            AnnualLeaveOnHours = 40;
        }

        [DataRequired]
        public string Id { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }

        public int AnnualLeaveOnHours { get; set; }
    }
}
