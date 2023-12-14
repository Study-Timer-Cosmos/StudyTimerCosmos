using StudyTimer.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyTimer.MVC.Models.Auth
{
    public class AuthViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(2)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        //public ICollection<UserStudySession> Sessions { get; set; }
    }
}
