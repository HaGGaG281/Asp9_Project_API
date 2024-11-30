using Asp9_Project_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp9_Project_Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<Users,IdentityRole<int> , int>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemsUnits>()
                .HasKey(i => new { i.Unit_Id, i.Item_Id });

            builder.Entity<CustomerStores>()
                .HasKey(x => new { x.Store_Id, x.Cus_Id });
            builder.Entity<InvItemStores>()
                .HasKey(x => new { x.Store_Id, x.Item_Id });
            builder.Entity<ShoppingCartItems>()
                .HasKey(x => new { x.Store_Id, x.Item_Id, x.Cus_Id });

            builder.Entity<InvoiceDetails>()
                .HasKey(x=> new {x.Invoice_Id , x.Item_Id});

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Governments> Governments { get; set; }
        public DbSet<Zones> Zones { get; set; }
        public DbSet<Classifications> Classifications { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<MainGroup> MainGroup { get; set; }
        public DbSet<SubGroup> SubGroup { get; set; }
        public DbSet<SubGroup2> SubGroup2 { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }
        public DbSet<InvItemStores> InvItemStores { get; set; }
        public DbSet<ItemsUnits> ItemsUnits { get; set; }
        public DbSet<CustomerStores> CustomerStores { get; set; }
        public DbSet<Stores> Stores { get; set; }

    }
}
