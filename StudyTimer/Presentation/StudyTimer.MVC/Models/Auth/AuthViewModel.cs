using StudyTimer.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyTimer.MVC.Models.Auth
{
    public class AuthViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        //public ICollection<UserStudySession> Sessions { get; set; }
    }
}
