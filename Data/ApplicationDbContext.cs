using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebDevProject.Models;

namespace WebDevProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor for dependency injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        // Add this parameterless constructor
        public ApplicationDbContext() : base() { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.Dep_ID);
                 modelBuilder.Entity<Employee>()
        .Property(e => e.Salary)
        .HasPrecision(18, 2);
        }
    }
}