using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyTimer.Domain.Entities;

namespace StudyTimer.Persistence.Configurations.Identity
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasOne<Duty>(x => x.Duty)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.DutyId);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(65);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(80);

            builder.ToTable("Categories");
        }
    }
}
