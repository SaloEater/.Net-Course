using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskEFRepository;

namespace Migrations
{
    public class MigrationContext : TaskRepository
    {
        public MigrationContext(IOptions<DbOption> dbOptions) : base(dbOptions)
        {
        }
    }
}
