using ProjectManagerAPI.Infrastructure.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjectManagerAPI.Infrastructure
{
    public interface IProjectManagerDBContext
    {
        DbSet<UserEntity> Users { get; set; }

        DbSet<TaskEntity> Tasks { get; set; }

        DbSet<ProjectEntity> Projects { get; set; }

        int SaveChanges();
    }
}