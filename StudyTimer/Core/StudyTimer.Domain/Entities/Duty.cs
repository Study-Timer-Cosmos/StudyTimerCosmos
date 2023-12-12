using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudyTimer.Domain.Entities
{
    public class Duty : IEntityBase<Guid>
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
