using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Setsis_Fullstack_Case.Models.Entity;

namespace Setsis_Fullstack_Case.Models
{
    public class SetsisFullstackCaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public SetsisFullstackCaseContext(DbContextOptions<SetsisFullstackCaseContext> options) : base(options)
        {


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
