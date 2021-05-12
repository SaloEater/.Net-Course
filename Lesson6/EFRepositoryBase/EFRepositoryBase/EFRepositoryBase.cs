using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFRepositoryBase
{
    public class EFContextBase<T> : DbContext, IRepositoryBase<T>
        where T : EntityBase
    {
        protected readonly IOptions<DbOption> _dbOptions;

        [NotMapped]
        public DbSet<T> Entities { get; set; }

        public EFContextBase(IOptions<DbOption> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SetupDatabase(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        public virtual void SetupDatabase(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbOptions.Value.ConnectionString);
        }

        public virtual async Task<T> Create(T entity)
        {
            await Entities.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> CreateMany(IEnumerable<T> entities)
        {
            foreach (var entity in entities) {
                await Entities.AddAsync(entity);
            }
            await SaveChangesAsync();
            return entities;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = Entities.FirstOrDefault(i => i.Id == id);
            if (entity is not null) {
                entity.IsDeleted = true;
                Entities.Update(entity);
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMany(IEnumerable<Guid> id)
        {
            var entities = Entities.Where(i => id.Contains(i.Id)).ToArray();
            foreach (var entity in entities) {
                entity.IsDeleted = true;
            }
            Entities.UpdateRange(entities);
            await SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Entities.ToArrayAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await Entities.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> Restore(Guid id)
        {
            var entity = Entities.FirstOrDefault(i => i.Id == id);
            if (entity is not null) {
                entity.IsDeleted = false;
                Entities.Update(entity);
                await SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RestoreMany(IEnumerable<Guid> id)
        {
            var entities = Entities.Where(i => id.Contains(i.Id)).ToArray();
            foreach (var entity in entities) {
                entity.IsDeleted = false;
            }
            Entities.UpdateRange(entities);
            await SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> Update(T entity)
        {
            Entities.Update(entity);
            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMany(IEnumerable<T> entities)
        {
            Entities.UpdateRange(entities);
            await SaveChangesAsync();
            return true;
        }
    }
}
