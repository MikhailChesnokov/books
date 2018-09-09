namespace Users.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUserForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}