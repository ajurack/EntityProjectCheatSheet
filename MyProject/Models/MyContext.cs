using Microsoft.EntityFrameworkCore;

namespace MyProject.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<MyModel> MyModels {get;set;}
    }
}