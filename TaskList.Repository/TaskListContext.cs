using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskList.Domain;
using TaskList.Domain.Identity;

namespace TaskList.Repository
{
    public class TaskListContext : IdentityDbContext<User,
                                    Role, int, 
                                    IdentityUserClaim<int>, 
                                    UserRole, 
                                    IdentityUserLogin<int>,
                                    IdentityRoleClaim<int>,
                                    IdentityUserToken<int>>
    {
        public TaskListContext(DbContextOptions<TaskListContext> options) : base(options) {}

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskRemarks> TaskRemarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
                    
                    userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();
                }
            );
        }
        
    }
}