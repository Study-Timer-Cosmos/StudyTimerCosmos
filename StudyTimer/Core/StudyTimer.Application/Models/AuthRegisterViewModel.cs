using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
