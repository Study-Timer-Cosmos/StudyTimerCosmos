using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Identity;

namespace StudyTimer.Persistence.Configurations.Identity
{
    public class UserStudySessionConfiguration : IEntityTypeConfiguration<UserStudySession>
    {
        public void Configure(EntityTypeBuilder<UserStudySession> builder)
        {
            builder.HasKey(x => new { x.UserId, x.StudySessionId });

            // Relationships
            builder.HasOne<User>(x => x.User)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.UserId);

            builder.HasOne<StudySession>(x=>x.StudySession)
                .WithMany(x=>x.Sessions)
                .HasForeignKey(x=>x.StudySessionId);

            // CreatedOn
            builder.Property(x=>x.CreatedOn).IsRequired();

            // CreatedByUserId
            builder.Property(x =>x.CreatedByUserId).IsRequired();
            builder.Property(x => x.CreatedByUserId).HasMaxLength(75);
           
            builder.ToTable("UserStudySessions");


        }
    }
}
