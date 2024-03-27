using System.Globalization;

namespace TrainApp.DAL;

using Domain.GTFS;

public interface IRepository
{
    // Agency
    public Agency ReadAgency(Guid id);
    public Agency CreateAgency(Agency agency);
    public Agency ReadAgencyByName(string name);
    
    //Calendar
    public Calendar ReadCalendar(Guid serviceId);
    public Calendar CreateCalendar(Calendar calendar);
    
    //CalendarDate
    public CalendarDate ReadCalendarDate(Guid serviceId, DateOnly date);
    public CalendarDate CreateCalendarDate(CalendarDate calendarDate);
    
    //Transfer
    public Transfer ReadTransfer(Guid fromStopId, Guid toStopId);
    
    public Transfer CreateTransfer(Transfer transfer);
    
    //Translation
    public Translation ReadTranslation(TableName tableName, string fieldName, 
        CultureInfo language, string fieldValue);
    
    public Translation CreateTranslation(Translation translation);
    
    //Route
    public Route ReadRoute(Guid routeId);
    public Route CreateRoute(Route route);
    
    //StopTimeOverride
    public StopTimeOverride ReadStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence);
    public StopTimeOverride CreateStopTimeOverride(StopTimeOverride stopTimeOverride);
    
    //StopTime
    public StopTime ReadStopTime(Guid tripId, uint stopSequence);
    public StopTime CreateStopTime(StopTime stopTime);
    
    //Stop
    public Stop ReadStop(Guid stopId);
    public Stop CreateStop(Stop stop);
    
    //Trip
    public Trip ReadTrip(Guid tripId);
    public Trip CreateTrip(Trip trip);
}