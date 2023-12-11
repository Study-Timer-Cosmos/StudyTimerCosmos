using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTimer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace StudyTimer.Persistence.Configurations.Identity
{
    public class StudySessionConfiguration : IEntityTypeConfiguration<StudySession>
    {
        public void Configure(EntityTypeBuilder<StudySession> builder)
        {
            // ID - Primary Key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.StartTime).IsRequired();
            builder.Property(x => x.EndTime).IsRequired();

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

            // Relationships
            builder.HasOne<Task>(x => x.Session)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);


            builder.ToTable("StudySessions");
        }
    }
}
