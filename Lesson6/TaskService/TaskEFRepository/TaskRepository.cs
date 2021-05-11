using Contract;
using DatabaseEntity;
using EFRepositoryBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryBase;
using System;

namespace TaskEFRepository
{
    public class TaskRepository : EFContextBase<Task>, ITaskRepository
    {
        public TaskRepository(IOptions<DbOption> dbOptions) : base(dbOptions)
        {

        }

        public override void SetupDatabase(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbOptions.Value.ConnectionString, b => b.MigrationsAssembly("Migrations"));
        }
    }
}
