namespace TrainApp.Domain.GTFS;

public class Transfer
{
    public Transfer()
    {
        
    }

    public Guid FromStopId { get; set; }
    public Guid ToStopId { get; set; }
    public TransferType TransferType { get; set; }
    public uint? MinTransferTime { get; set; }

    public override string ToString()
    {
        return $"{FromStopId} to {ToStopId} with {TransferType} {MinTransferTime}";
    }
}

public enum TransferType : byte
{
    RecommendedTransfer = 0,
    TimedTransfer = 1,
    MinimumTimedTransfer = 2,
    NoTransfer = 3,
    InSeatTransfer = 4,
    ReboardTransfer = 5
}