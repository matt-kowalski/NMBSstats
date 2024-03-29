using Microsoft.EntityFrameworkCore;
using TrainApp.Domain.GTFS;

namespace TrainApp.DAL.EF;

public class TrainAppDbContext : DbContext
{
    public TrainAppDbContext(DbContextOptions options) : base(options)
    {
        TrainAppDbInitializer.Initialize(this, true);
    }

    public bool IsInitialized { get; set; }

    public DbSet<Agency> Agencies { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<CalendarDate> CalendarDates { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<Translation> Translations { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<StopTimeOverride> StopTimeOverrides { get; set; }
    public DbSet<StopTime> StopTimes { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Shape> Shapes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}