using System.ComponentModel.DataAnnotations;

namespace StudentsManagerMW.Models.Inputs
{
    public class RegisterModel
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }



        [Required]
        [MinLength(4)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
