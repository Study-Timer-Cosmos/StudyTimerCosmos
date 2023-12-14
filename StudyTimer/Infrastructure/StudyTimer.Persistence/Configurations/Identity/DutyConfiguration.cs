using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTimer.Domain.Entities;

namespace StudyTimer.Persistence.Configurations.Identity
{
    public class DutyConfiguration : IEntityTypeConfiguration<Duty>
    {
     
        public void Configure(EntityTypeBuilder<Duty> builder)
        {
            builder.HasOne<StudySession>(x => x.Session)
                .WithMany(x => x.Duties)
                .HasForeignKey(x=>x.SessionId);

            builder.HasKey(x => x.Id);

            // Topic is required and has a maximum length of 255 characters
            builder.Property(x => x.Topic).IsRequired().HasMaxLength(255);

            // TaskTime is required
            builder.Property(x => x.TaskTime).IsRequired();

            builder.Property(x => x.isFinished).IsRequired();

            builder.ToTable("Duties");
        }
    }
}
