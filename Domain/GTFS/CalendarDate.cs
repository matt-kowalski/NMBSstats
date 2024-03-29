using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainApp.Domain.GTFS;

public class CalendarDate
{
    public CalendarDate()
    {
        
    }
    
    [Key]
    public Calendar Calendar { get; set; }
    [Key]
    public DateOnly Date { get; set; }
    public DateExceptionType DateException { get; set; }

    public override string ToString()
    {
        return $"{Date}: {DateException} for {Calendar.Id}";
    }
}

public enum DateExceptionType : byte
{
    ServiceAdded = 1,
    ServiceRemoved = 2
}

