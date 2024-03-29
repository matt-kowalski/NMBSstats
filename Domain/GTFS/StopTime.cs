using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainApp.Domain.GTFS;

public class StopTime
{
    public StopTime()
    {
        
    }
    
    [Key]
    public Trip Trip { get; set; }
    [Required]
    public TimeOnly ArrivalTime { get; set; }
    [Required]
    public TimeOnly DepartureTime { get; set; }
    [Required]
    public Stop Stop { get; set; }
    [Key]
    public uint StopSequence { get; set; }
    [Required]
    public PickupType PickupType { get; set; }
    [Required]
    public DropoffType DropoffType { get; set; }

    public override string ToString()
    {
        return $"Trip {Trip.Headsign} has stop at {Stop.Name} with sequence {StopSequence}.\n" +
               $"{ArrivalTime} - {DepartureTime}\n" +
               $"Pickup: {PickupType}\n" +
               $"Dropoff: {DropoffType}\n";
    }
}

public enum PickupType : byte
{
    Regular = 0,
    NoPickup = 1,
    ContactAgency = 2,
    ContactDriver = 3,
}

public enum DropoffType : byte
{
    Regular = 0,
    NoDropoff = 1,
    ContactAgency = 2,
    ContactDriver = 3,
}