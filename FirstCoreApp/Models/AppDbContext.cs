using Microsoft.EntityFrameworkCore;

namespace FirstCoreApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
                
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name ="Tom",
                    Age =25,
                    Email="Tom@gmail.com"
                });


        }
    }
}
