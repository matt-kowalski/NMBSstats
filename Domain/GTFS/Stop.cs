using System.ComponentModel.DataAnnotations;

namespace TrainApp.Domain.GTFS;

public class Stop
{
    public Stop()
    {
        
    }

    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    public LocationType LocationType { get; set; }
    public Stop? ParentStop { get; set; }
    public string? Platform { get; set; }

    public override string ToString()
    {
        if (LocationType == LocationType.Platform)
            return $"{Id} {Name} at {Latitude} {Longitude}\n" +
                   $"With parent station {ParentStop!.Name} {Platform}";
        return $"{Id} {Name} at {Latitude} {Longitude}";
    }
}

public enum LocationType : byte
{
    Platform = 0,
    Station = 1,
    EntranceExit = 2,
    GenericNode = 3,
    BoardingArea = 4,
}