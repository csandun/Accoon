using Accoon.Api.DataServices.Entities.CustomEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.Api.DataServices.Entities
{
    public class AccoonDbContext: DbContext
    {
        public AccoonDbContext(DbContextOptions<AccoonDbContext> options)
           : base(options)
        { }

        public DbSet<ValueEntity> Values { get; set; }
    }
}


// Add-Migration NewMigration -Project Accoon.Api.DataServices.Entities -StartupProject Accoon.Api
// update-database -verbose