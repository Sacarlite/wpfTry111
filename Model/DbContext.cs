using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;

namespace wpfTry.Model
{

    public class ProductContext : DbContext
    {
        public ProductContext()
            : base()
        {
            //C:\poe\Kr4.db
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\foo_db\KursDb.db");
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ListOfCharacteristics> ListsOfCharacteristics { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        public DbSet<CharacteristicsName> CharacteristicsNames { get; set; } = null!;
        public DbSet<Specifications> Specification { get; set; } = null!;
        public DbSet<UOfM> UOfMs { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderTable> OrderTable { get; set; } = null!;
    }
}
