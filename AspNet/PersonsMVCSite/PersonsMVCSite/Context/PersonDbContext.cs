using Microsoft.EntityFrameworkCore;
using PersonsMVCSite.Models;

namespace PersonsMVCSite.Context
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> People { get; init; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }
    }
}
