using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDM.DataModel
{
  public  class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                   : base(options)
        {

        }

        public DbSet<Assignment> Assignments { get; set; }
    }
}
