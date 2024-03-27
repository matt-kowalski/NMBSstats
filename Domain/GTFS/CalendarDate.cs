namespace TrainApp.Domain.GTFS;

public class CalendarDate
{
    public CalendarDate()
    {
        
    }

    public Guid ServiceId { get; set; }
    public DateOnly Date { get; set; }
    public DateExceptionType DateException { get; set; }

    public override string ToString()
    {
        return $"{Date}: {DateException} for {ServiceId}";
    }
}

public enum DateExceptionType : byte
{
    ServiceAdded = 1,
    ServiceRemoved = 2
}

