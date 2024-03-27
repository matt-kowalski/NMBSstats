namespace TrainApp.Domain.GTFS;

public class Trip
{
    public Trip()
    {
        
    }

    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public Guid BlockId { get; set; }
    public TripType TripType { get; set; }
}

public enum TripType : byte
{
    Unknown = 0,
    Type1 = 1,
}