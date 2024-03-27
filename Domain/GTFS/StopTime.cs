namespace TrainApp.Domain.GTFS;

public class StopTime
{
    public StopTime()
    {
        
    }

    public Guid TripId { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public Guid StopId { get; set; }
    public uint StopSequence { get; set; }
    public PickupType PickupType { get; set; }
    public DropoffType DropoffType { get; set; }

    public override string ToString()
    {
        return $"Trip {TripId} has stop at {StopId} with sequence {StopSequence}.\n" +
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