using LibraryManagementSystemMVC_Project.Models.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemMVC_Project.DatabaseConnection
{
    public class DBConnectionFactory:DbContext
    {
        public DBConnectionFactory(DbContextOptions<DBConnectionFactory> options):base(options)
        {
        
        }

        public DbSet<ApplicationUser> tblApplicationUsers { get; set; }
        public DbSet<Book> tblBooks { get; set; }
        public DbSet<Issue> tblIssues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Data seeding for Admin User

            modelBuilder.Entity<ApplicationUser>().HasData( 
                new ApplicationUser
                {
                    UserId=Guid.NewGuid(),
                    UserName="admin@gmail.com",
                    Email="admin@gmail.com",
                    PasswordHash=BCrypt.Net.BCrypt.HashPassword("admin@12345"),
                    Role="Admin"
                    
                }
                );
        }
        
    }
}
