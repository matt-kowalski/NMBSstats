using System.Globalization;

namespace TrainApp.DAL;

using Domain.GTFS;

public interface IRepository
{
    // Agency
    public Agency ReadAgency(Guid id);
    public Agency CreateAgency(Agency agency);
    //Calendar
    public Calendar ReadCalendar(Guid id);
    public Calendar CreateCalendar(Calendar calendar);
    
    //CalendarDate
    public CalendarDate ReadCalendarDate(Guid serviceId, DateOnly date);
    public CalendarDate CreateCalendarDate(CalendarDate calendarDate);
    
    //Transfer
    public Transfer ReadTransfer(Guid fromStopId, Guid toStopId);
    
    public Transfer CreateTransfer(Transfer transfer);
    
    //Translation
    public Translation ReadTranslation(TableType tableType, string fieldName, 
        string language, string fieldValue);
    
    public Translation CreateTranslation(Translation translation);
    
    //Route
    public Route ReadRoute(Guid id);
    public Route CreateRoute(Route route);
    
    //StopTimeOverride
    public StopTimeOverride ReadStopTimeOverride(Guid tripId, Guid serviceId, uint stopSequence);
    public StopTimeOverride CreateStopTimeOverride(StopTimeOverride stopTimeOverride);
    
    //StopTime
    public StopTime ReadStopTime(Guid tripId, uint stopSequence);
    public StopTime CreateStopTime(StopTime stopTime);
    
    //Stop
    public Stop ReadStop(Guid id);
    public Stop CreateStop(Stop stop);
    public void DeleteStop(Stop stop);
    public Stop ReadStopByName(string name, string language);
    
    //Trip
    public Trip ReadTrip(Guid id);
    public Trip CreateTrip(Trip trip);
    
    //Shape
    public Shape ReadShape(Guid id);
    public Shape CreateShape(Shape shape);
    
    //Meta
    public IEnumerable<Stop> ReadAllStations();
}