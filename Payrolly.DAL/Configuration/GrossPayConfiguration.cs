using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payrolly.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payrolly.DAL.Configuration
{
    public class GrossPayConfiguration : IEntityTypeConfiguration<GrossPay>
    {
        public void Configure(EntityTypeBuilder<GrossPay> builder)
        {
            builder.Property(g => g.OverTime)
                .HasDefaultValue(0.0);

            builder.Property(g => g.HolidayPay)
                .HasDefaultValue(0.0);

            builder.Property(g => g.Bonus)
                .HasDefaultValue(0.0m);

            builder.Property(g => g.Commision)
                .HasDefaultValue(0.0m);
        }
    }
}
