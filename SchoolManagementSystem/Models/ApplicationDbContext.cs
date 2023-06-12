using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Models {
  
    public class ApplicationDbContext : IdentityDbContext<AppUser> {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<AppUserStudent> AppUserStudents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            
           :base(options){      

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUserStudent>()
                .HasKey(aus => new { aus.AppUserId, aus.StudentId });

            modelBuilder.Entity<AppUserStudent>()
                .HasOne(aus => aus.AppUser)
                .WithMany(u => u.AppUserStudents)
                .HasForeignKey(aus => aus.AppUserId);

            modelBuilder.Entity<AppUserStudent>()
                .HasOne(aus => aus.Student)
                .WithMany(s => s.AppUserStudents)
                .HasForeignKey(aus => aus.StudentId);

			modelBuilder.Entity<Grade>()
		        .HasOne(g => g.Student)
		        .WithMany(s => s.Grades)
		        .HasForeignKey(g => g.StudentId);
		}

    }
}
