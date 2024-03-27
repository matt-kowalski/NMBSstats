using System.Globalization;

namespace TrainApp.BL;

using Domain.GTFS;

public interface IManager
{
    // Agency
    public Agency GetAgency(Guid id);
    public Agency AddAgency(string name, string url, TimeZoneInfo timezone, CultureInfo language);
    
    //Calendar
    public Calendar GetCalendar(Guid id);
    public Calendar AddCalendar(Monday monday, Tuesday tuesday, Wednesday wednesday, Thursday thursday, Friday friday, 
        Saturday saturday, Sunday sunday, DateOnly startDate, DateOnly endDate);
    
    //CalendarDate
    public CalendarDate GetCalendarDate(Guid serviceId, DateOnly date);
    public CalendarDate AddCalendarDate(DateOnly date, DateExceptionType dateException);
    
    //Transfer
    public Transfer GetTransfer(Guid fromStopId, Guid toStopId);
    
    public Transfer AddTransfer(Guid fromStopId, Guid toStopId, TransferType transferType, 
        uint? minTransferTime = null);
    
    //Translation
    public Translation GetTranslation(TableName tableName, string fieldName, 
        CultureInfo language, string fieldValue);
    
    public Translation AddTranslation(TableName tableName, string fieldName, CultureInfo language, string translatedValue, 
        string fieldValue);
    
    //Route
    public Route GetRoute(Guid id);
    public Route AddRoute(Guid agencyId, string shortName, string longName, RouteType type);
    
    //StopTimeOverride
    public StopTimeOverride GetStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence);
    public StopTimeOverride AddStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence, Guid stopId);
    
    //StopTime
    public StopTime GetStopTime(Guid tripId, uint stopSequence);
    public StopTime AddStopTime(Guid tripId, TimeOnly arrivalTime, TimeOnly departureTime, Guid stopId, 
        uint stopSequence, PickupType pickupType, DropoffType dropoffType);
    
    //Stop
    public Stop GetStop(Guid id);
    public Stop AddStop(string name, double latitude, double longitude, LocationType locationType, 
        Guid? parentStationStopId = null, string? platformCode = null);
    
    //Trip
    public Trip GetTrip(Guid id);
    public Trip AddTrip(Guid routeId, Guid serviceId, string shortName, string longName, Guid blockId, 
        TripType tripType);
}