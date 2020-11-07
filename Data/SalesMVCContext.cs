using Microsoft.EntityFrameworkCore;

using SalesMVC.Models;

namespace SalesMVC.Data
{
    public class SalesMVCContext : DbContext
    {
        public SalesMVCContext(DbContextOptions<SalesMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }

    }
}
