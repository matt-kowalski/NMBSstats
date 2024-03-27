namespace TrainApp.Domain.GTFS;

public class StopTimeOverride
{
    public StopTimeOverride()
    {
        
    }

    public Guid TripId { get; set; }
    public Guid ServiceId { get; set; }
    public uint StopSequence { get; set; }
    public Guid StopId { get; set; }
}