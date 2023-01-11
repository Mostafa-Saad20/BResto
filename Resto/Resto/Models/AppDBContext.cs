using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Resto.Models
{
    public class AppDBContext: DbContext
    {
        public AppDBContext() : base("name=RestoDBConnection") 
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Category - Food (One to Many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Foods)
                .WithRequired(f => f.Category)
                .HasForeignKey(f => f.CategoryId);

            // Customer - Carts(One to Many)
            modelBuilder.Entity<Customer>()
                .HasMany(cst => cst.CartItems)
                .WithRequired(crt => crt.Customer)
                .HasForeignKey(crt => crt.CustomerId);

            // Customer - Order (One to Many)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithRequired(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            // Customer - Reservation (One to Many)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithRequired(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);

            // set Reservation DateTime col as Date
            modelBuilder.Entity<Reservation>()
                    .Property(r => r.Date)
                    .HasColumnType("date");

        }

        public System.Data.Entity.DbSet<Resto.Models.Feedback> Feedbacks { get; set; }
    }
}