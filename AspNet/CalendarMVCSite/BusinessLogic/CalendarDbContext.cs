using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace BusinessLogic
{
    public class CalendarDbContext : DbContext
    {
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RecurrencySetting> RecurrencySettings { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {    
        }
    }
}