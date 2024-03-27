using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace TrainApp.DAL.IMR;

using Domain.GTFS;

public class InMemoryRepository : IRepository
{
    private readonly string _rawData;
    public InMemoryRepository(string rawData)
    {
        _rawData = rawData;
        
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
    
    public Agency ReadAgency(Guid id)
    {
        return Agencies.Find(a => a.Id == id) ?? throw new KeyNotFoundException();
    }

    public Agency CreateAgency(Agency agency)
    {
        if (agency.Id == Guid.Empty) agency.Id = Guid.NewGuid();
        Agencies.Add(agency);
        return agency;
    }

    public Agency ReadAgencyByName(string name)
    {
        return Agencies.Find(a => a.Name == name) ?? throw new KeyNotFoundException();
    }

    public Calendar ReadCalendar(Guid id)
    {
        return Calendars.Find(c => c.Id == id) ?? throw new KeyNotFoundException();
    }

    public Calendar CreateCalendar(Calendar calendar)
    {
        if (calendar.Id == Guid.Empty) calendar.Id = Guid.NewGuid();
        Calendars.Add(calendar);
        return calendar;
    }

    public CalendarDate ReadCalendarDate(Guid serviceId, DateOnly date)
    {
        return CalendarDates.Find(cd => cd.ServiceId == serviceId && 
                                        cd.Date == date) ?? throw new KeyNotFoundException();
    }

    public CalendarDate CreateCalendarDate(CalendarDate calendarDate)
    {
        CalendarDates.Add(calendarDate);
        return calendarDate;
    }

    public Transfer ReadTransfer(Guid fromStopId, Guid toStopId)
    {
        return Transfers.Find(t => t.FromStopId == fromStopId && 
                                   t.ToStopId == toStopId) ?? throw new KeyNotFoundException();
    }

    public Transfer CreateTransfer(Transfer transfer)
    {
        Transfers.Add(transfer);
        return transfer;
    }

    public Translation ReadTranslation(TableName tableName, string fieldName, CultureInfo language, string fieldValue)
    {
        return Translations.Find(t =>
            t.TableName == tableName && 
            t.FieldName == fieldName && 
            t.Language.Equals(language) &&
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
        if (route.Id == Guid.Empty) route.Id = Guid.NewGuid();
        Routes.Add(route);
        return route;
    }

    public StopTimeOverride ReadStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence)
    {
        return StopTimeOverrides.Find(sto => sto.TripId == tripId &&
                                             sto.ServiceId == serviceId &&
                                             sto.StopSequence == stopSequence) ?? throw new KeyNotFoundException();
    }

    public StopTimeOverride CreateStopTimeOverride(StopTimeOverride stopTimeOverride)
    {
        StopTimeOverrides.Add(stopTimeOverride);
        return stopTimeOverride;
    }

    public StopTime ReadStopTime(Guid tripId, uint stopSequence)
    {
        return StopTimes.Find(st => st.TripId == tripId &&
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
        if (stop.Id == Guid.Empty) stop.Id = Guid.NewGuid();
        Stops.Add(stop);
        return stop;
    }

    public Trip ReadTrip(Guid id)
    {
        return Trips.Find(t => t.Id == id) ?? throw new KeyNotFoundException();
    }

    public Trip CreateTrip(Trip trip)
    {
        if (trip.Id == Guid.Empty) trip.Id = Guid.NewGuid();
        Trips.Add(trip);
        return trip;
    }

    private Guid GenerateGuid(string? rawDataId = null)
    {
        if (rawDataId == null) return Guid.NewGuid();

        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(rawDataId));
        byte[] guid = hash.Take(16).ToArray();
        return new Guid(guid);
    }

    public void Seed()
    {
        if (!Path.Exists(_rawData)) throw new DirectoryNotFoundException($"Could not find {_rawData}.");

        string file = _rawData + "/agency.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Agency agency = new Agency()
            {
                Id = GenerateGuid(items[0]),
                Name = items[1],
                Url = items[2],
                Timezone = TimeZoneInfo.FindSystemTimeZoneById(items[3]),
                Language = CultureInfo.GetCultures(CultureTypes.AllCultures).First(c => c.TwoLetterISOLanguageName == items[4])
            };
            CreateAgency(agency);
        }

        Console.Out.WriteLine($"Seeded {file}");

        file = _rawData + "/calendar.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Calendar calendar = new Calendar()
            {
                Id = GenerateGuid(items[0]),
                Monday = (Monday)Convert.ToByte(items[1]),
                Tuesday = (Tuesday)Convert.ToByte(items[2]),
                Wednesday = (Wednesday)Convert.ToByte(items[3]),
                Thursday = (Thursday)Convert.ToByte(items[4]),
                Friday = (Friday)Convert.ToByte(items[5]),
                Saturday = (Saturday)Convert.ToByte(items[6]),
                Sunday = (Sunday)Convert.ToByte(items[7]),
                StartDate = DateOnly.ParseExact(items[8], "yyyyMMdd", null),
                EndDate = DateOnly.ParseExact(items[9], "yyyyMMdd", null)
            };
            CreateCalendar(calendar);
        }
        Console.Out.WriteLine($"Seeded {file}");

        file = _rawData + "/calendar_dates.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            CalendarDate calendarDate = new CalendarDate()
            {
                ServiceId = GenerateGuid(items[0]),
                Date = DateOnly.ParseExact(items[1], "yyyyMMdd", null),
                DateException = (DateExceptionType)Convert.ToByte(items[2])
            };
            CreateCalendarDate(calendarDate);
        }
        Console.Out.WriteLine($"Seeded {file}");

        file = _rawData + "/routes.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Route route = new Route()
            {
                Id = GenerateGuid(items[0]),
                AgencyId = GenerateGuid(items[1]),
                ShortName = items[2],
                LongName = items[3],
                RouteType = (RouteType)Convert.ToInt32(items[5])
            };
            CreateRoute(route);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = _rawData + "/stop_time_overrides.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            StopTimeOverride stopTimeOverride = new StopTimeOverride()
            {
                TripId = GenerateGuid(items[0]),
                StopSequence = Convert.ToUInt32(items[1]),
                ServiceId = GenerateGuid(items[2]),
                StopId = GenerateGuid(items[3])
            };
            CreateStopTimeOverride(stopTimeOverride);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = _rawData + "/stop_times.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            
            //disgusting oneliners to fix hour 25 in the dataset
            if (Convert.ToInt32(items[1].Split(':')[0]) > 23)
            {
                items[1] = $"{Convert.ToInt32(items[1].Split(':')[0]) - 24:00}:{items[1].Split(':')[1]}:{items[1].Split(':')[2]}";
            }
            if (Convert.ToInt32(items[2].Split(':')[0]) > 23)
            {
                items[2] = $"{Convert.ToInt32(items[2].Split(':')[0]) - 24:00}:{items[2].Split(':')[1]}:{items[2].Split(':')[2]}";
            }

            StopTime stopTime = new StopTime()
            {
                TripId = GenerateGuid(items[0]),
                ArrivalTime = TimeOnly.ParseExact(items[1], "HH:mm:ss", null),
                DepartureTime = TimeOnly.ParseExact(items[2], "HH:mm:ss", null),
                StopId = GenerateGuid(items[3]),
                StopSequence = Convert.ToUInt32(items[4]),
                PickupType = (PickupType)Convert.ToByte(items[6]),
                DropoffType = (DropoffType)Convert.ToByte(items[7])
            };
            CreateStopTime(stopTime);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = _rawData + "/stops.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Stop stop = new Stop()
            {
                Id = GenerateGuid(items[0]),
                Name = items[2],
                Latitude = Convert.ToDouble(items[4]),
                Longitude = Convert.ToDouble(items[5]),
                LocationType = (LocationType)Convert.ToByte(items[8]),
                ParentStationStopId = String.IsNullOrEmpty(items[9]) ? null : GenerateGuid(items[9]),
                PlatformCode = String.IsNullOrEmpty(items[10]) ? null : items[10]
            };
            CreateStop(stop);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = _rawData + "/transfers.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Transfer transfer = new Transfer()
            {
                FromStopId = GenerateGuid(items[0]),
                ToStopId = GenerateGuid(items[1]),
                TransferType = (TransferType)Convert.ToByte(items[2]),
                MinTransferTime = Convert.ToUInt32(items[3])
            };
            CreateTransfer(transfer);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = _rawData + "/translations.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            
            Enum.TryParse(items[0], true, out TableName tableName);

            Translation translation = new Translation()
            {
                TableName = tableName,
                FieldName = items[1],
                Language = CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .First(c => c.TwoLetterISOLanguageName == items[2]),
                TranslatedValue = items[3],
                FieldValue = items[6]
            };
            CreateTranslation(translation);
        }
        Console.Out.WriteLine($"Seeded {file}");
        Console.Out.WriteLine("Seeding completed.");
    }
}