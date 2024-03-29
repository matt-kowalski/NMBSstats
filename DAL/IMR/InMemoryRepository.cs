namespace TrainApp.DAL.IMR;

using Domain.GTFS;

public class InMemoryRepository : IRepository
{
    public InMemoryRepository()
    {
        Agencies = new List<Agency>();
        Calendars = new List<Calendar>();
        CalendarDates = new List<CalendarDate>();
        Transfers = new List<Transfer>();
        Translations = new List<Translation>();
        Routes = new List<Route>();
        StopTimeOverrides = new List<StopTimeOverride>();
        StopTimes = new List<StopTime>();
        Stops = new List<Stop>();
        Trips = new List<Trip>();
        Shapes = new List<Shape>();
        
        GtfsSeeder.Seed(this);
    }

    public List<Agency> Agencies { get; set; }
    public List<Calendar> Calendars { get; set; }
    public List<CalendarDate> CalendarDates { get; set; }
    public List<Transfer> Transfers { get; set; }
    public List<Translation> Translations { get; set; }
    public List<Route> Routes { get; set; }
    public List<StopTimeOverride> StopTimeOverrides { get; set; }
    public List<StopTime> StopTimes { get; set; }
    public List<Stop> Stops { get; set; }
    public List<Trip> Trips { get; set; }
    public List<Shape> Shapes { get; set; }
    
    private Guid AssertId(Guid id)
    {
        if (id == Guid.Empty) return Guid.NewGuid();
        return id;
    }
    
    public Agency ReadAgency(Guid id)
    {
        return Agencies.Find(a => a.Id == id) ?? throw new KeyNotFoundException();
    }

    public Agency CreateAgency(Agency agency)
    {
        agency.Id = AssertId(agency.Id);
        Agencies.Add(agency);
        return agency;
    }

    public Calendar ReadCalendar(Guid id)
    {
        return Calendars.Find(c => c.Id == id) ?? throw new KeyNotFoundException();
    }

    public Calendar CreateCalendar(Calendar calendar)
    {
        calendar.Id = AssertId(calendar.Id);
        Calendars.Add(calendar);
        return calendar;
    }

    public CalendarDate ReadCalendarDate(Guid serviceId, DateOnly date)
    {
        return CalendarDates.Find(cd => cd.Calendar.Id == serviceId && 
                                        cd.Date == date) ?? throw new KeyNotFoundException();
    }

    public CalendarDate CreateCalendarDate(CalendarDate calendarDate)
    {
        CalendarDates.Add(calendarDate);
        return calendarDate;
    }

    public Transfer ReadTransfer(Guid fromStopId, Guid toStopId)
    {
        return Transfers.Find(t => t.FromStop.Id == fromStopId && 
                                   t.ToStop.Id == toStopId) ?? throw new KeyNotFoundException();
    }

    public Transfer CreateTransfer(Transfer transfer)
    {
        Transfers.Add(transfer);
        return transfer;
    }

    public Translation ReadTranslation(TableType tableType, string fieldName, string language, string fieldValue)
    {
        return Translations.Single(t =>
            t.TableType == tableType && 
            t.FieldName == fieldName && 
            t.Language == language &&
            t.FieldValue == fieldValue) ?? throw new KeyNotFoundException();
    }

    public Translation CreateTranslation(Translation translation)
    {
        Translations.Add(translation);
        return translation;
    }

    public Route ReadRoute(Guid id)
    {
        return Routes.Find(r => r.Id == id) ?? throw new KeyNotFoundException();
    }

    public Route CreateRoute(Route route)
    {
        route.Id = AssertId(route.Id);
        Routes.Add(route);
        return route;
    }

    public StopTimeOverride ReadStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence)
    {
        return StopTimeOverrides.Find(sto => sto.Trip.Id == tripId &&
                                             sto.Calendar.Id == serviceId &&
                                             sto.StopSequence == stopSequence) ?? throw new KeyNotFoundException();
    }

    public StopTimeOverride CreateStopTimeOverride(StopTimeOverride stopTimeOverride)
    {
        StopTimeOverrides.Add(stopTimeOverride);
        return stopTimeOverride;
    }

    public StopTime ReadStopTime(Guid tripId, uint stopSequence)
    {
        return StopTimes.Find(st => st.Trip.Id == tripId &&
                                    st.StopSequence == stopSequence) ?? throw new KeyNotFoundException();
    }

    public StopTime CreateStopTime(StopTime stopTime)
    {
        StopTimes.Add(stopTime);
        return stopTime;
    }

    public Stop ReadStop(Guid id)
    {
        return Stops.Find(s => s.Id == id) ?? throw new KeyNotFoundException();
    }

    public Stop CreateStop(Stop stop)
    {
        stop.Id = AssertId(stop.Id);
        Stops.Add(stop);
        return stop;
    }

    public void DeleteStop(Stop stop)
    {
        Stops.Remove(stop);
    }

    public Stop ReadStopByName(string name, string language)
    {
        Translation translated = Translations.Find(t => t.TableType == TableType.Stops &&
                                                        t.FieldName == "stop_name" &&
                                                        t.Language == language &&
                                                        String.Equals(t.TranslatedValue, name, StringComparison.OrdinalIgnoreCase)) ?? throw new KeyNotFoundException();
        return Stops.Find(s => s.LocationType == LocationType.Station &&
                               String.Equals(s.Name, translated.FieldValue, StringComparison.OrdinalIgnoreCase)) ?? throw new KeyNotFoundException();
    }

    public Trip ReadTrip(Guid id)
    {
        return Trips.Find(t => t.Id == id) ?? throw new KeyNotFoundException();
    }

    public Trip CreateTrip(Trip trip)
    {
        trip.Id = AssertId(trip.Id);
        Trips.Add(trip);
        return trip;
    }

    public Shape ReadShape(Guid id)
    {
        return Shapes.Find(s => s.Id == id) ?? throw new KeyNotFoundException();
    }

    public Shape CreateShape(Shape shape)
    {
        shape.Id = AssertId(shape.Id);
        Shapes.Add(shape);
        return shape;
    }

    public IEnumerable<Stop> ReadAllStations()
    {
        return Stops.FindAll(s => s.LocationType == LocationType.Station);
    }
}