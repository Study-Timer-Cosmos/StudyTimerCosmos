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

            builder.ToTable("Duties");
        }
    }
}
