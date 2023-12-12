using StudyTimer.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.Domain.Entities
{
    public class StudySession : IEntityBase<Guid>, ICreatedOn, IDeletedOn, IModifiedOn
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public ICollection<Duty> Duties { get; set; }
        public ICollection<UserStudySession> Sessions { get; set; }


        public DateTimeOffset? ModifiedOn { get; set; }
        public string? ModifiedByUserId { get; set; }
        public string? DeletedByUserId { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByUserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
