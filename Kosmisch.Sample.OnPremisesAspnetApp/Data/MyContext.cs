using Microsoft.EntityFrameworkCore;

namespace Kosmisch.Sample.OnPremisesAspnetApp.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Kosmisch.Sample.OnPremisesAspnetApp.Models.User> Users { get; set; }
    }
}
