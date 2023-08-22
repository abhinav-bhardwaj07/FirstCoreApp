using System.ComponentModel.DataAnnotations;

namespace FirstCoreApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string  Name { get; set; }


        [Required]
        public int Age { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
    }
}
