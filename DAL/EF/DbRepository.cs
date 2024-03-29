using TrainApp.Domain.GTFS;

namespace TrainApp.DAL.EF;

public class DbRepository : IRepository
{
    private readonly TrainAppDbContext _dbContext;
    
    public DbRepository(TrainAppDbContext dbContext)
    {
        _dbContext = dbContext;
        if (!dbContext.Agencies.Any())
        {
            GtfsSeeder.Seed(this);
        }
    }

    private Guid AssertId(Guid id)
    {
        if (id == Guid.Empty) return Guid.NewGuid();
        return id;
    }
    
    public Agency ReadAgency(Guid id)
    {
        return _dbContext.Agencies.Find(id) ?? throw new KeyNotFoundException();
    }

    public Agency CreateAgency(Agency agency)
    {
        agency.Id = AssertId(agency.Id);
        _dbContext.Add(agency);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return agency;
    }

    public Calendar ReadCalendar(Guid id)
    {
        return _dbContext.Calendars.Find(id) ?? throw new KeyNotFoundException();
    }

    public Calendar CreateCalendar(Calendar calendar)
    {
        calendar.Id = AssertId(calendar.Id);
        _dbContext.Add(calendar);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return calendar;
    }

    public CalendarDate ReadCalendarDate(Guid calendarId, DateOnly date)
    {
        return _dbContext.CalendarDates.Find(calendarId, date) ?? throw new KeyNotFoundException();
    }

    public CalendarDate CreateCalendarDate(CalendarDate calendarDate)
    {
        _dbContext.Add(calendarDate);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return calendarDate;
    }

    public Transfer ReadTransfer(Guid fromStopId, Guid toStopId)
    {
        return _dbContext.Transfers.Single(t => t.FromStop.Id == fromStopId &&
                                                t.ToStop.Id == toStopId) ?? throw new KeyNotFoundException();
    }

    public Transfer CreateTransfer(Transfer transfer)
    {
        _dbContext.Add(transfer);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return transfer;
    }

    public Translation ReadTranslation(TableType tableType, string fieldName, string language, string fieldValue)
    {
        return _dbContext.Translations.Single(t => t.TableType == tableType &&
                                                   t.FieldName == fieldName &&
                                                   t.Language == language &&
                                                   t.FieldValue == fieldValue) ?? throw new KeyNotFoundException();
    }

    public Translation CreateTranslation(Translation translation)
    {
        _dbContext.Add(translation);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return translation;
    }

    public Route ReadRoute(Guid id)
    {
        return _dbContext.Routes.Find(id) ?? throw new KeyNotFoundException();
    }

    public Route CreateRoute(Route route)
    {
        route.Id = AssertId(route.Id);
        _dbContext.Add(route);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return route;
    }

    public StopTimeOverride ReadStopTimeOverride(Guid tripId, Guid calendarId, uint stopSequence)
    {
        return _dbContext.StopTimeOverrides.Single(sto => sto.Trip.Id == tripId &&
                                                          sto.Calendar.Id == calendarId &&
                                                          sto.StopSequence == stopSequence) ?? throw new KeyNotFoundException();
    }

    public StopTimeOverride CreateStopTimeOverride(StopTimeOverride stopTimeOverride)
    {
        _dbContext.Add(stopTimeOverride);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return stopTimeOverride;
    }

    public StopTime ReadStopTime(Guid tripId, uint stopSequence)
    {
        return _dbContext.StopTimes.Find(tripId, stopSequence) ?? throw new KeyNotFoundException();
    }

    public StopTime CreateStopTime(StopTime stopTime)
    {
        _dbContext.Add(stopTime);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return stopTime;
    }

    public Stop ReadStop(Guid id)
    {
        return _dbContext.Stops.Find(id) ?? throw new KeyNotFoundException();
    }

    public Stop CreateStop(Stop stop)
    {
        stop.Id = AssertId(stop.Id);
        _dbContext.Add(stop);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return stop;
    }

    public void DeleteStop(Stop stop)
    {
        _dbContext.Remove(stop);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
    }

    public Stop ReadStopByName(string name, string language)
    {
        Translation translated = _dbContext.Translations.Single(t => t.TableType == TableType.Stops &&
                                                        t.FieldName == "stop_name" &&
                                                        t.Language == language &&
                                                        String.Equals(t.TranslatedValue, name, StringComparison.OrdinalIgnoreCase)) ?? throw new KeyNotFoundException();
        return _dbContext.Stops.Single(s => s.LocationType == LocationType.Station &&
                               String.Equals(s.Name, translated.FieldValue, StringComparison.OrdinalIgnoreCase)) ?? throw new KeyNotFoundException();
    }

    public string ReadTranslatedStopName(string name, string language)
    {
        string? translation = _dbContext.Translations.Find(TableType.Stops, "stop_name", language, name)?.TranslatedValue;
        if (String.IsNullOrEmpty(translation)) throw new KeyNotFoundException();
        return translation;
    }

    public Trip ReadTrip(Guid id)
    {
        return _dbContext.Trips.Find(id) ?? throw new KeyNotFoundException();
    }

    public Trip CreateTrip(Trip trip)
    {
        trip.Id = AssertId(trip.Id);
        _dbContext.Add(trip);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return trip;
    }

    public Shape ReadShape(Guid id, uint pointSequence)
    {
        return _dbContext.Shapes.Find(id, pointSequence) ?? throw new KeyNotFoundException();
    }

    public Shape CreateShape(Shape shape)
    {
        shape.Id = AssertId(shape.Id);
        _dbContext.Add(shape);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        return shape;
    }

    public IEnumerable<Stop> ReadAllStations()
    {
        return _dbContext.Stops.Where(s => s.LocationType == LocationType.Station);
    }
}