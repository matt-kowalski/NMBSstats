using System.Security.Cryptography;
using System.Text;
using TrainApp.Domain.GTFS;

namespace TrainApp.DAL;

public static class GtfsSeeder
{ 
    private static Guid GenerateGuid(string? rawDataId = null)
    {
        if (rawDataId == null) return Guid.NewGuid();

        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(rawDataId));
        byte[] guid = hash.Take(16).ToArray();
        return new Guid(guid);
    }
    
    public static void Seed(IRepository rep)
    {
        string rawData = Environment.CurrentDirectory + "/gtfs";
        if (!Path.Exists(rawData)) throw new DirectoryNotFoundException($"Could not find {rawData}.");

        string file = rawData + "/agency.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Agency agency = new Agency()
            {
                Id = GenerateGuid(items[0]),
                Name = items[1],
                Url = items[2],
                Timezone = items[3],
                Language = items[4]
            };
            rep.CreateAgency(agency);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = rawData + "/routes.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Route route = new Route()
            {
                Id = GenerateGuid(items[0]),
                Agency = rep.ReadAgency(GenerateGuid(items[1])),
                RouteTypeName = items[2],
                LongName = items[3],
                RouteType = (RouteType)Convert.ToInt32(items[5])
            };
            rep.CreateRoute(route);
        }
        Console.Out.WriteLine($"Seeded {file}");

        file = rawData + "/calendar.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Calendar calendar = new Calendar()
            {
                Id = GenerateGuid(items[0]),
                Monday = (DateAvailability)Convert.ToByte(items[1]),
                Tuesday = (DateAvailability)Convert.ToByte(items[2]),
                Wednesday = (DateAvailability)Convert.ToByte(items[3]),
                Thursday = (DateAvailability)Convert.ToByte(items[4]),
                Friday = (DateAvailability)Convert.ToByte(items[5]),
                Saturday = (DateAvailability)Convert.ToByte(items[6]),
                Sunday = (DateAvailability)Convert.ToByte(items[7]),
                StartDate = DateOnly.ParseExact(items[8], "yyyyMMdd", null),
                EndDate = DateOnly.ParseExact(items[9], "yyyyMMdd", null)
            };
            rep.CreateCalendar(calendar);
        }
        Console.Out.WriteLine($"Seeded {file}");

        file = rawData + "/calendar_dates.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            CalendarDate calendarDate = new CalendarDate()
            {
                Calendar = rep.ReadCalendar(GenerateGuid(items[0])),
                Date = DateOnly.ParseExact(items[1], "yyyyMMdd", null),
                DateException = (DateExceptionType)Convert.ToByte(items[2])
            };
            rep.CreateCalendarDate(calendarDate);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = rawData + "/shapes.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Shape shape = new Shape()
            {
                Id = GenerateGuid(items[0]),
                Latitude = Convert.ToDouble(items[1]),
                Longitude = Convert.ToDouble(items[2]),
                PointSequence = Convert.ToUInt32(items[3]),
                DistanceTraveled = Convert.ToDouble(items[4])
            };
            rep.CreateShape(shape);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = rawData + "/trips.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Trip trip = new Trip()
            {
                Id = GenerateGuid(items[2]),
                Route = rep.ReadRoute(GenerateGuid(items[0])),
                Calendar = rep.ReadCalendar(GenerateGuid(items[1])),
                Headsign = items[3],
                TrainName = items[4],
                Shape = rep.ReadShape(GenerateGuid(items[7])),
                BlockId = GenerateGuid(items[6])
            };
            rep.CreateTrip(trip);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = rawData + "/stops.csv";
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
                ParentStop = null,
                Platform = null
            };
            rep.CreateStop(stop);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        //fix for missing parent stops at time of initialisation in previous loop
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            if (String.IsNullOrEmpty(items[9])) continue;
            
            Stop oldStop = rep.ReadStop(GenerateGuid(items[0]));
            Stop newStop = oldStop;
            newStop.ParentStop = rep.ReadStop(GenerateGuid(items[9]));
            newStop.Platform = String.IsNullOrEmpty(items[10]) ? null : items[10];
            
            rep.DeleteStop(oldStop);
            rep.CreateStop(newStop);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = rawData + "/stop_times.csv";
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
                Trip = rep.ReadTrip(GenerateGuid(items[0])),
                ArrivalTime = TimeOnly.ParseExact(items[1], "HH:mm:ss", null),
                DepartureTime = TimeOnly.ParseExact(items[2], "HH:mm:ss", null),
                Stop = rep.ReadStop(GenerateGuid(items[3])),
                StopSequence = Convert.ToUInt32(items[4]),
                PickupType = (PickupType)Convert.ToByte(items[6]),
                DropoffType = (DropoffType)Convert.ToByte(items[7])
            };
            rep.CreateStopTime(stopTime);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = rawData + "/stop_time_overrides.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            StopTimeOverride stopTimeOverride = new StopTimeOverride()
            {
                Trip = rep.ReadTrip(GenerateGuid(items[0])),
                StopSequence = Convert.ToUInt32(items[1]),
                Calendar = rep.ReadCalendar(GenerateGuid(items[2])),
                Stop = rep.ReadStop(GenerateGuid(items[3]))
            };
            rep.CreateStopTimeOverride(stopTimeOverride);
        }
        Console.Out.WriteLine($"Seeded {file}");

        file = rawData + "/transfers.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            Transfer transfer = new Transfer()
            {
                FromStop = rep.ReadStop(GenerateGuid(items[0])),
                ToStop = rep.ReadStop(GenerateGuid(items[1])),
                TransferType = (TransferType)Convert.ToByte(items[2]),
                MinTransferTime = Convert.ToUInt32(items[3])
            };
            rep.CreateTransfer(transfer);
        }
        Console.Out.WriteLine($"Seeded {file}");
        
        file = rawData + "/translations.csv";
        if (!File.Exists(file)) throw new FileNotFoundException($"Could not find {file}.");
        foreach (string line in File.ReadLines(file).Skip(1))
        {
            string[] items = line.Split(',');
            
            Enum.TryParse(items[0], true, out TableType tableName);

            Translation translation = new Translation()
            {
                TableType = tableName,
                FieldName = items[1],
                Language = items[2],
                TranslatedValue = items[3],
                FieldValue = items[6]
            };
            rep.CreateTranslation(translation);
        }
        Console.Out.WriteLine($"Seeded {file}");
        Console.Out.WriteLine("Seeding completed.");
    }
}