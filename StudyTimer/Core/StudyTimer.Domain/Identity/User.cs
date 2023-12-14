using Microsoft.AspNetCore.Identity;
using StudyTimer.Domain.Common;
using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Enums;

namespace StudyTimer.Domain.Identity
{
    public class User : IdentityUser<Guid>, IEntityBase<Guid>, ICreatedOn, IModifiedOn
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string? ModifiedByUserId { get; set; }
        public ICollection<UserStudySession> Sessions { get; set; }
    }
}
