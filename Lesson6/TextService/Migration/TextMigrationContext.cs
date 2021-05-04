using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRepository;

namespace Migrations
{
    public class TextMigrationContext : TextRepository.TextRepository
    {
        public TextMigrationContext(IOptions<TextDbOption> textDbOptions) : base(textDbOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(b => b.MigrationsAssembly("Migrations"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
