using ProjectManagerAPI.Infrastructure.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjectManagerAPI.Infrastructure
{
    public class ProjectManagerDBContext : DbContext, IProjectManagerDBContext
    {
        public ProjectManagerDBContext() : base("name=ProjectManagementDBConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProjectManagerDBContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<TaskEntity>()
                   .HasRequired(x => x.Project)
                   .WithMany(x => x.Tasks)
                   .WillCascadeOnDelete(false);
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<TaskEntity> Tasks { get; set; }

        public DbSet<ProjectEntity> Projects { get; set; }
    }
}