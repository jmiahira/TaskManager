using Microsoft.EntityFrameworkCore;
using TaskList.Domain;

namespace TaskList.Repository
{
    public class TaskListContext : DbContext
    {
        public TaskListContext(DbContextOptions<TaskListContext> options) : base(options) {}

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskRemarks> TaskRemarks { get; set; }
        
    }
}