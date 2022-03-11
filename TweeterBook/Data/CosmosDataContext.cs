using Microsoft.EntityFrameworkCore;
using TweeterBook.Domain;

namespace TweeterBook.Data
{
    public class CosmosDataContext : DbContext
    {
        public DbSet<CosmosPost> Posts { get; set; }
        public CosmosDataContext(DbContextOptions<CosmosDataContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CosmosPost>()
                .ToContainer("Posts")
                .HasPartitionKey(post => post.Id);
        }
    }
}
