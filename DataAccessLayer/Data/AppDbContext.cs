
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Data Source=SF-CPU-332\SQLEXPRESS;Initial Catalog=Practical22;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Employee>().Property<DateTime>("UpdatedDate");
        }
        public DbSet<Employee> Employees { get; set;}
        public DbSet<Department> Departments { get; set; }
    }
}
