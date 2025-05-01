using System.ComponentModel.DataAnnotations.Schema;

namespace WebDevProject.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        // Foreign key to Department
        public int Dep_ID { get; set; }

        [ForeignKey("Dep_ID")] // Add this attribute
        public Department Department { get; set; }

        // Foreign key to Identity User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}