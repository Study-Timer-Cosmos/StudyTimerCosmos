using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyTimer.Domain.Entities;

namespace StudyTimer.Persistence.Configurations.Identity
{
    public class DutyConfiguration : IEntityTypeConfiguration<Duty>
    {
     
        public void Configure(EntityTypeBuilder<Duty> builder)
        {
            builder.HasOne<Duty>(x=>x.Session).WithMany<Duty>(x=>x.Duties);
        }
    }
}
