using System.ComponentModel.DataAnnotations;

namespace TrainApp.Domain.GTFS;

public class Calendar
{
    public Calendar()
    {
        
    }
    
    [Key]
    public Guid Id { get; set; }
    [Required]
    public DateAvailability Monday { get; set; }
    [Required]
    public DateAvailability Tuesday { get; set; }
    [Required]
    public DateAvailability Wednesday { get; set; }
    [Required]
    public DateAvailability Thursday { get; set; }
    [Required]
    public DateAvailability Friday { get; set; }
    [Required]
    public DateAvailability Saturday { get; set; }
    [Required]
    public DateAvailability Sunday { get; set; }
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }

    public override string ToString()
    {
        return $"Mon\t\tTue\t\tWed\t\tThu\t\tFri\t\tSat\t\tSun\n" +
               $"{Monday}\t{Tuesday}\t{Wednesday}\t{Thursday}\t{Friday}\t{Saturday}\t{Sunday}\n" +
               $"{StartDate} - {EndDate}";
    }
}

public enum DateAvailability : byte
{
    NotAvailable = 0,
    Available = 1
}