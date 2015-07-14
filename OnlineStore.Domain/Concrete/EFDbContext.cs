using OnlineStore.Domain.Entities;
using System.Data.Entity;

namespace OnlineStore.Domain.Concrete
{
    class EFDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
