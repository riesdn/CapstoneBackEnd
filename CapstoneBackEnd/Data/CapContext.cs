using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapstoneBackEnd.Models;

namespace CapstoneBackEnd.Data
{
    public class CapContext : DbContext
    {
        public CapContext (DbContextOptions<CapContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder model) {

            model.Entity<User>(e => {

                e.ToTable("Users");
                e.HasKey(e => e.Id);
                e.Property(e => e.Username).HasMaxLength(30).IsRequired();
                e.HasIndex(e => e.Username).IsUnique();
                e.Property(e => e.Password).HasMaxLength(30).IsRequired();
                e.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(30).IsRequired();
                e.Property(e => e.Phone).HasMaxLength(12);
                e.Property(e => e.Email).HasMaxLength(255);
                e.Property(e => e.IsReviewer).HasDefaultValue(false);
                e.Property(e => e.IsAdmin).HasDefaultValue(false);

            });

            model.Entity<Vendor>(e => {

                e.ToTable("Vendors");
                e.HasKey(e => e.Id);
                e.Property(e => e.Code).HasMaxLength(30).IsRequired();
                e.HasIndex(e => e.Code).IsUnique();
                e.Property(e => e.Name).HasMaxLength(30).IsRequired();
                e.Property(e => e.Address).HasMaxLength(30).IsRequired();
                e.Property(e => e.City).HasMaxLength(30).IsRequired();
                e.Property(e => e.State).HasMaxLength(2).IsRequired();
                e.Property(e => e.Zip).HasMaxLength(5).IsRequired();
                e.Property(e => e.Phone).HasMaxLength(12);
                e.Property(e => e.Email).HasMaxLength(255);

            });

            model.Entity<Product>(e => {

                e.ToTable("Products");
                e.HasKey(e => e.Id);
                e.Property(e => e.PartNbr).HasMaxLength(30).IsRequired();
                e.HasIndex(e => e.PartNbr).IsUnique();
                e.Property(e => e.Name).HasMaxLength(30).IsRequired();
                e.Property(e => e.Price).HasColumnType("decimal(11,2)");
                e.Property(e => e.Unit).HasMaxLength(30).IsRequired();
                e.Property(e => e.PhotoPath).HasMaxLength(255);

            });

            model.Entity<Request>(e => {
                
                e.ToTable("Requests");
                e.HasKey(e => e.Id);
                e.Property(e => e.Description).HasMaxLength(80).IsRequired();
                e.Property(e => e.Justification).HasMaxLength(80).IsRequired();
                e.Property(e => e.RejectionReason).HasMaxLength(80);
                e.Property(e => e.DeliveryMode).HasMaxLength(20).IsRequired().HasDefaultValue("Pickup");
                e.Property(e => e.Status).HasMaxLength(10).IsRequired().HasDefaultValue("NEW");
                e.Property(e => e.Total).HasColumnType("decimal(11,2)").HasDefaultValue(0);

            });

        }

        public DbSet<CapstoneBackEnd.Models.User> Users { get; set; }

        public DbSet<CapstoneBackEnd.Models.Vendor> Vendors { get; set; }

        public DbSet<CapstoneBackEnd.Models.Product> Products { get; set; }

        public DbSet<CapstoneBackEnd.Models.Request> Requests { get; set; }

        public DbSet<CapstoneBackEnd.Models.RequestLine> RequestLine { get; set; }
    }
}
