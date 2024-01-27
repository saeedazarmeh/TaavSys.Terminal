using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Bus;

namespace Terminal.EntityMab.BusEntityMap
{
    internal class BusEntityMap : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            builder.ToTable("Buses", "Bus");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasColumnType("VarChar(30)");
            builder.Property(b => b.Type).IsRequired();
            builder.HasMany(b => b.Trips).WithOne(t => t.Bus).HasForeignKey(t => t.BusId);
        }
    }
}
