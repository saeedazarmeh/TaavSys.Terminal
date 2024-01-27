using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Trip;

namespace Terminal.EntityMap.SeatEntityMap
{
    internal class SeatEntityMap : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats", "Trip");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.SeatType).IsRequired();
            builder.Property(s => s.SeatNumber).IsRequired().HasColumnType("int"); ;
        }
    }
}
