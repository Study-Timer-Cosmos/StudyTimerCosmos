using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.MVC.Models
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
        public string SurName { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public ICollection<UserStudySession> Sessions { get; set; }
    }
}
