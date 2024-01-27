using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Trip;

namespace Terminal.EntityMat. TripEntityMap
{
    internal class TripEntityMap  : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("Trips", "Trip");
            builder.HasKey(t  => t. Id);
            builder.HasMany(t  => t. Tickets).WithOne(t => t.Trip).HasForeignKey(t => t.TripId);
            ///-- Value Object---///

            //builder.OwnsOne(t => t.Time, option =>
            //{
            //    option.Property(s => s.TripTime)
            //    .IsRequired();
            //    option.Property(s => s.TripDate)
            //    .IsRequired();
            //});
            builder.OwnsOne(t => t.Route, option =>
            {
                option.Property(s => s.Origin)
                .IsRequired();
                option.Property(s => s.Destination)
                .IsRequired();
            });



        }
    }

}
