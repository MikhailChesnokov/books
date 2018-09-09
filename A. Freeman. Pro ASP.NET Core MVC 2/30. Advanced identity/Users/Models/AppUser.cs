namespace Users.Models
{
    using Microsoft.AspNetCore.Identity;

    public enum Cities
    {
        None,
        London,
        Paris,
        Chicago
    }

    public enum QualificationLevel
    {
        None,
        Basic,
        Advanced
    }
    
    public class AppUser : IdentityUser
    {
        public Cities City { get; set; }

        public QualificationLevel QualificationLevel { get; set; }
    }
}