using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Trip;

namespace Terminal.EntityMap.TicketEntityMap
{
    internal class TicketEntityMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets", "Trip");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Payment).IsRequired().HasPrecision(12, 2);
            builder.Property(t => t.TotalPrice).IsRequired().HasPrecision(12,2) ;
            builder.Property(t => t.PassengerId).IsRequired().HasColumnType("int");
            builder.HasMany(t => t.Seats).WithOne(t => t.Ticket).HasForeignKey(t => t.TicketId);
        }
    }
}
