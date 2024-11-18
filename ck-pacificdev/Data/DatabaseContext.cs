using ck_pacificdev.Models;
using Microsoft.EntityFrameworkCore;

namespace ck_pacificdev.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Image> Images { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }
    }
}
