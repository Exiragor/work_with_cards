using System.Data.Entity;

namespace TestTask
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
        }
        public DbSet<Card> Cards { get; set; }
    }
}
