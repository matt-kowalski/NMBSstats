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

    public Calendar GetCalendar(Guid id)
    {
        return _rep.ReadCalendar(id);
    }

    public CalendarDate GetCalendarDate(Guid calendarId, DateOnly date)
    {
        return _rep.ReadCalendarDate(calendarId, date);
    }

    public Transfer GetTransfer(Guid fromStopId, Guid toStopId)
    {
        return _rep.ReadTransfer(fromStopId, toStopId);
    }

    public Translation GetTranslation(TableType tableType, string fieldName, string language, string fieldValue)
    {
        return _rep.ReadTranslation(tableType, fieldName, language, fieldValue);
    }

    public Route GetRoute(Guid id)
    {
        return _rep.ReadRoute(id);
    }

    public StopTimeOverride GetStopTimeOverride(Guid tripId, Guid calendarId, uint stopSequence)
    {
        return _rep.ReadStopTimeOverride(tripId, calendarId, stopSequence);
    }

    public StopTime GetStopTime(Guid tripId, uint stopSequence)
    {
        return _rep.ReadStopTime(tripId, stopSequence);
    }

    public Stop GetStop(Guid id)
    {
        return _rep.ReadStop(id);
    }

    public Stop GetStopByName(string name, string language)
    {
        return _rep.ReadStopByName(name, language);
    }

    public string GetTranslatedStopName(string name, string language = "nl")
    {
        return _rep.ReadTranslatedStopName(name, language);
    }

    public Trip GetTrip(Guid id)
    {
        return _rep.ReadTrip(id);
    }

    public Shape GetShape(Guid id, uint pointSequence)
    {
        return _rep.ReadShape(id, pointSequence);
    }

    public IEnumerable<Stop> GetAllStations()
    {
        return _rep.ReadAllStations();
    }
}