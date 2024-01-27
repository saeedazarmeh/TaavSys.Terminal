using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Passenger;

namespace Terminal.EntityMap.PassengerEntityMap
{
    internal class PassengerEntityMap : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.ToTable("Passengers", "Passenger");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasColumnType("VarChar(30)"); ;
            builder.Property(p => p.PhoneNumber).IsRequired().HasColumnType("Char(11)"); ;
            builder.HasMany(p=>p.Tickets).WithOne(t=>t.Passenger).HasForeignKey(t=>t.PassengerId);
        }
    }
}
