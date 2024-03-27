namespace TrainApp.Domain.GTFS;

public class Stop
{
    public Stop()
    {
        
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public LocationType LocationType { get; set; }
    public Guid? ParentStationStopId { get; set; }
    public string? PlatformCode { get; set; }

    public override string ToString()
    {
        if (ParentStationStopId != null)
            return $"{Id} {Name} at {Latitude} {Longitude}\n" +
                   $"With parent station {ParentStationStopId} {PlatformCode}";
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