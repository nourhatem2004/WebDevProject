namespace WebDevProject.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateEmployeeDto
    {
        public int ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int Dep_ID { get; set; }



    }
}
