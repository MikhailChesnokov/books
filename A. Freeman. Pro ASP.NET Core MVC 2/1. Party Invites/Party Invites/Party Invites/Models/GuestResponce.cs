namespace Party_Invites.Models
{
    using System.ComponentModel.DataAnnotations;



    public class GuestResponce
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get;set; }

        [Required(ErrorMessage = "Please enter a valid email")]
        [RegularExpression(".+\\@.+\\..+")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please select")]
        public bool? WillAttend { get; set; }
    }
}