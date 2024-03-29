using System.Globalization;

namespace TrainApp.BL;

using Domain.GTFS;

public interface IManager
{
    public Agency GetAgency(Guid id);
    public Calendar GetCalendar(Guid id);
    public CalendarDate GetCalendarDate(Guid calendarId, DateOnly date);
    public Transfer GetTransfer(Guid fromStopId, Guid toStopId);
    public Translation GetTranslation(TableType tableType, string fieldName, 
        string language, string fieldValue);
    public Route GetRoute(Guid id);
    public StopTimeOverride GetStopTimeOverride(Guid tripId, Guid calendarId, uint stopSequence);
    public StopTime GetStopTime(Guid tripId, uint stopSequence);
    public Stop GetStop(Guid id);
    public Stop GetStopByName(string name, string language = "nl");
    public string GetTranslatedStopName(string name, string language = "nl");
    public Trip GetTrip(Guid id);
    public Shape GetShape(Guid id, uint pointSequence);
    public IEnumerable<Stop> GetAllStations();
}