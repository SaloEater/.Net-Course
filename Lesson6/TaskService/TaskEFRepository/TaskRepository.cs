using Contract;
using DatabaseEntity;
using EFRepositoryBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using RepositoryBase;
using System;
using System.Threading.Tasks;

namespace TaskEFRepository
{
    public class TaskRepository : EFContextBase<DatabaseEntity.Task>, ITaskRepository
    {
        public TaskRepository(IOptions<DbOption> dbOptions) : base(dbOptions)
        {

        }

        public override void SetupDatabase(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbOptions.Value.ConnectionString, b => b.MigrationsAssembly("Migrations"));
        }
        public override async Task<bool> Update(DatabaseEntity.Task entity)
        {
            Entities.Include(i => i.Words);
            Entities.Include(i => i.TasksTexts);
            return await base.Update(entity);
        }
    }
}
