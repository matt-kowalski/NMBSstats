using System.Globalization;
using TrainApp.DAL;
using TrainApp.Domain.GTFS;
using Calendar = TrainApp.Domain.GTFS.Calendar;

namespace TrainApp.BL;

public class Manager : IManager
{
    private readonly IRepository _rep;
    
    public Manager(IRepository repository)
    {
        _rep = repository;
    }
    
    public Agency GetAgency(Guid id)
    {
        return _rep.ReadAgency(id);
    }

    public Agency AddAgency(string name, string url, TimeZoneInfo timezone, CultureInfo language)
    {
        Agency agency = new Agency()
        {
            Name = name,
            Url = url,
            Timezone = timezone,
            Language = language
        };
        return _rep.CreateAgency(agency);
    }

    public Calendar GetCalendar(Guid id)
    {
        return _rep.ReadCalendar(id);
    }

    public Calendar AddCalendar(Monday monday, Tuesday tuesday, Wednesday wednesday, Thursday thursday, Friday friday,
        Saturday saturday, Sunday sunday, DateOnly startDate, DateOnly endDate)
    {
        Calendar calendar = new Calendar()
        {
            Monday = monday,
            Tuesday = tuesday,
            Wednesday = wednesday,
            Thursday = thursday,
            Friday = friday,
            Saturday = saturday,
            Sunday = sunday,
            StartDate = startDate,
            EndDate = endDate
        };
        return _rep.CreateCalendar(calendar);
    }

    public CalendarDate GetCalendarDate(Guid serviceId, DateOnly date)
    {
        return _rep.ReadCalendarDate(serviceId, date);
    }

    public CalendarDate AddCalendarDate(DateOnly date, DateExceptionType dateException)
    {
        CalendarDate calendarDate = new CalendarDate()
        {
            Date = date,
            DateException = dateException
        };
        return _rep.CreateCalendarDate(calendarDate);
    }

    public Transfer GetTransfer(Guid fromStopId, Guid toStopId)
    {
        return _rep.ReadTransfer(fromStopId, toStopId);
    }

    public Transfer AddTransfer(Guid fromStopId, Guid toStopId, TransferType transferType, uint? minTransferTime = null)
    {
        Transfer transfer = new Transfer()
        {
            FromStopId = fromStopId,
            ToStopId = toStopId,
            TransferType = transferType,
            MinTransferTime = minTransferTime
        };
        return _rep.CreateTransfer(transfer);
    }

    public Translation GetTranslation(TableName tableName, string fieldName, CultureInfo language, string fieldValue)
    {
        return _rep.ReadTranslation(tableName, fieldName, language, fieldValue);
    }

    public Translation AddTranslation(TableName tableName, string fieldName, CultureInfo language, string translatedValue,
        string fieldValue)
    {
        Translation translation = new Translation()
        {
            TableName = tableName,
            FieldName = fieldName,
            Language = language,
            TranslatedValue = translatedValue,
            FieldValue = fieldValue
        };
        return _rep.CreateTranslation(translation);
    }

    public Route GetRoute(Guid id)
    {
        return _rep.ReadRoute(id);
    }

    public Route AddRoute(Guid agencyId, string shortName, string longName, RouteType routeType)
    {
        Route route = new Route()
        {
            AgencyId = agencyId,
            ShortName = shortName,
            LongName = longName,
            RouteType = routeType
        };
        return _rep.CreateRoute(route);
    }

    public StopTimeOverride GetStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence)
    {
        return _rep.ReadStopTimeOverride(tripId, serviceId, stopSequence);
    }

    public StopTimeOverride AddStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence, Guid stopId)
    {
        StopTimeOverride stopTimeOverride = new StopTimeOverride()
        {
            TripId = tripId,
            ServiceId = serviceId,
            StopSequence = stopSequence,
            StopId = stopId
        };
        return _rep.CreateStopTimeOverride(stopTimeOverride);
    }

    public StopTime GetStopTime(Guid tripId, uint stopSequence)
    {
        return _rep.ReadStopTime(tripId, stopSequence);
    }

    public StopTime AddStopTime(Guid tripId, TimeOnly arrivalTime, TimeOnly departureTime, Guid stopId, uint stopSequence,
        PickupType pickupType, DropoffType dropoffType)
    {
        StopTime stopTime = new StopTime()
        {
            TripId = tripId,
            ArrivalTime = arrivalTime,
            DepartureTime = departureTime,
            StopId = stopId,
            StopSequence = stopSequence,
            PickupType = pickupType,
            DropoffType = dropoffType
        };
        return _rep.CreateStopTime(stopTime);
    }

    public Stop GetStop(Guid id)
    {
        return _rep.ReadStop(id);
    }

    public Stop AddStop(string name, double latitude, double longitude, LocationType locationType, Guid? parentStationStopId = null,
        string? platformCode = null)
    {
        Stop stop = new Stop()
        {
            Name = name,
            Latitude = latitude,
            Longitude = longitude,
            LocationType = locationType,
            ParentStationStopId = parentStationStopId,
            PlatformCode = platformCode
        };
        return _rep.CreateStop(stop);
    }

    public Trip GetTrip(Guid id)
    {
        return _rep.ReadTrip(id);
    }

    public Trip AddTrip(Guid routeId, Guid serviceId, string shortName, string longName, Guid blockId, TripType tripType)
    {
        Trip trip = new Trip()
        {
            RouteId = routeId,
            ServiceId = serviceId,
            ShortName = shortName,
            LongName = longName,
            BlockId = blockId,
            TripType = tripType
        };
        return _rep.CreateTrip(trip);
    }
}