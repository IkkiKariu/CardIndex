﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardIndex.Core.Entities;


namespace CardIndex.Business.Models
{
    internal class EmployeeStorage
    {
        public static int MaxID { get; set; }

        public List<Employee> Employees { get; } = new List<Employee>();
    }
}
