using StudyTimer.Domain.Common;

namespace StudyTimer.Domain.Entities
{
    public class Duty : EntityBase<Guid>
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public bool isFinished { get; set; }
        public TimeSpan TaskTime { get; set; }
        public StudySession Session { get; set; }
        public Guid SessionId { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
