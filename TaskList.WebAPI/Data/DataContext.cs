using Microsoft.EntityFrameworkCore;
using TaskList.Api.Model;

namespace TaskList.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Tasks> Tasks { get; set; }
        
    }
}