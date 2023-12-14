using StudyTimer.Domain.Enums;

namespace StudyTimer.Application.Models
{
    public class AuthRegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        
    }
}
