using System;
using System.Collections.Generic;

namespace WebDevProject.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Dep_name { get; set; }
        public DateTime establish_date { get; set; }
        public List<Employee> Employees { get; set; }
    }
}