using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Models.Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
