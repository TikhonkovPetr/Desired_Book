using Microsoft.EntityFrameworkCore;
using model;
using System.Reflection;

namespace AbstractDBModel
{
    public class AppDBModel : DbContext
    {
        public DbSet<DesiredBook> desiredBooks { get; set; } = default;
        public AppDBModel(DbContextOptions<AppDBModel> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
