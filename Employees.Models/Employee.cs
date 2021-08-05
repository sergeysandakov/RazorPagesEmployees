using System.ComponentModel.DataAnnotations;

namespace EmployeesCore.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name field cannot be null")]
        [MaxLength(50), MinLength(2)]
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "The email field cannot be null")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Please, enter a valid email (format: example@exem.com)")]
        [MaxLength(50), MinLength(2)]
        public string Email { get; set; }
        public Dept Dept { get; set; }
    }
}
