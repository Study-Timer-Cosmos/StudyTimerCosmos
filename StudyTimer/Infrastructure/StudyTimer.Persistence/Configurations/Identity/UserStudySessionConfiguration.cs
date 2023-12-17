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

            // Common Fields

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired();
            builder.Property(x => x.CreatedByUserId).HasMaxLength(75);


            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // ModifiedByUserId
            builder.Property(x => x.ModifiedByUserId).IsRequired(false);
            builder.Property(x => x.ModifiedByUserId).HasMaxLength(75);

            // LastModifiedOn
            builder.Property(x => x.ModifiedOn).IsRequired(false);

            // DeletedByUserId
            builder.Property(x => x.DeletedByUserId).IsRequired(false);
            builder.Property(x => x.DeletedByUserId).HasMaxLength(75);

            // DeletedOn
            builder.Property(x => x.DeletedOn).IsRequired(false);

            // IsDeleted
            builder.Property(x => x.IsDeleted).IsRequired();

            builder.ToTable("UserStudySessions");


        }
    }
}
