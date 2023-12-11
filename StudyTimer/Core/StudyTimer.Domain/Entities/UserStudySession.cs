using StudyTimer.Domain.Common;
using StudyTimer.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.Domain.Entities
{
    public class UserStudySession : ICreatedOn
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid StudySessionId { get; set; }
        public StudySession StudySession { get; set; }

        public string CreatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
