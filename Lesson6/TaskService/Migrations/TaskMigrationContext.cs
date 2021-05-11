using DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryBase;
using TaskEFRepository;

namespace Migrations
{
    public class TaskMigrationContext : TaskRepository
    {
        public DbSet<TaskText> TasksTexts;

        public DbSet<Word> Words;

        public TaskMigrationContext(IOptions<DbOption> dbOptions) : base(dbOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskText>().HasIndex(i => new { i.TaskId, i.TextId, i.WordId}).IsUnique(true);
            builder.Entity<Task>()
                .HasMany(i => i.TasksTexts)
                .WithOne(i => i.Task)
                .HasForeignKey(i => i.TaskId)
                .IsRequired(true);
            builder.Entity<Word>()
                .HasMany(i => i.TasksTexts)
                .WithOne(i => i.Word)
                .HasForeignKey(i => i.WordId)
                .IsRequired(true);
        }
    }
}
