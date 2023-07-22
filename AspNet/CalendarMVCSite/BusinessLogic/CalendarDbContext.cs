using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace BusinessLogic
{
    public class CalendarDbContext : DbContext
    {
        public DbSet<Meeting> Meetings { get; init; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {    
        }
    }
}