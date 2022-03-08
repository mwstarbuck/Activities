using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
  public class DataContext : DbContext
  {
    /* DbSet will tell Entity framework to create to create
    a Db table names Activities with the properties from the 
    Activity class in Domain as the columns.  EF knows to use
    this b/c it inherits from DbContext from EF */
    public DbSet<Activity> Activities { get; set; }
    public DataContext(DbContextOptions options) : base(options)
    {
    }
  }
}