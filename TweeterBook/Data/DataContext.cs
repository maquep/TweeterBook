using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TweeterBook.Domain;

namespace TweeterBook.Data
{
    public class DataContext : IdentityDbContext //DbContext
    {
        public DbSet<Post> Posts { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
       

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("FileName=TweeterBookSqlLite", option =>
        //    {
        //        option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        //    });
        //    base.OnConfiguring(optionsBuilder);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Post>().ToTable("Posts", "PostSchema");
        //    modelBuilder.Entity<Post>(entity => {
        //        entity.HasKey(k => k.Id);
        //        entity.HasIndex(i => i.Title).IsUnique();
        //    });

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
