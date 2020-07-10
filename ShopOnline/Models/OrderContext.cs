using System.Data.Entity;

namespace ShopOnline.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("OrderConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}