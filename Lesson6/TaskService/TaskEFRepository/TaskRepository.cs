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
        public DbSet<TaskText> TasksTexts { get; set; }

        public DbSet<Word> Words { get; set; }

        public TaskRepository(IOptions<DbOption> dbOptions) : base(dbOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().ToTable("word");
            /*modelBuilder.Entity<Word>()
                .HasMany(i => i.TasksTexts)
                .WithOne(i => i.Word)
                .HasForeignKey(i => i.WordId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Word>()
                .HasOne(i => i.Task)
                .WithMany(i => i.Words)
                .HasForeignKey(i => i.TaskId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<TaskText>().ToTable("tasks_texts");
            modelBuilder.Entity<TaskText>().HasIndex(i => new { i.TaskId, i.TextId, i.WordId }).IsUnique(true);

            modelBuilder.Entity<DatabaseEntity.Task>().ToTable("task");            
            /*modelBuilder.Entity<DatabaseEntity.Task>()
                .HasMany(i => i.TasksTexts)
                .WithOne(i => i.Task)
                .HasForeignKey(i => i.TaskId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);*/

            base.OnModelCreating(modelBuilder);
        }

        public override void SetupDatabase(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbOptions.Value.ConnectionString, b => b.MigrationsAssembly("Migrations"));
        }

        public override async Task<DatabaseEntity.Task> GetById(Guid Id)
        {
            Entities.Include(i => i.Words);
            Entities.Include(i => i.TasksTexts);
            return await base.GetById(Id);
        }

        public override async Task<DatabaseEntity.Task> Create(DatabaseEntity.Task task)
        {
            Entry(task).State = EntityState.Modified;
            if (task.Words is not null) {
                foreach (var word in task.Words) {
                    Entry(word).State = word.Id == Guid.Empty ? EntityState.Added : EntityState.Modified;
                }
            }
            if (task.TasksTexts is not null) {
                foreach (var taskText in task.TasksTexts) {
                    Entry(taskText).State = taskText.Id == Guid.Empty ? EntityState.Added : EntityState.Modified;
                }
            }
            return await base.Create(task);
        }
    }
}
