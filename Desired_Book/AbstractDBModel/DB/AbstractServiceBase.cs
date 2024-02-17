using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AbstractDBModel
{
    public abstract class AppDBModel : DbContext
    {

        public AppDBModel(DbContextOptions<AppDBModel> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
