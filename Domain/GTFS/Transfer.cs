using System.ComponentModel.DataAnnotations;

namespace TrainApp.Domain.GTFS;

public class Transfer
{
    public Transfer()
    {
        
    }
    [Key]
    public Stop FromStop { get; set; }
    [Key]
    public Stop ToStop { get; set; }
    [Required]
    public TransferType TransferType { get; set; }
    public uint? MinTransferTime { get; set; }

    public override string ToString()
    {
        return $"{FromStop.Name} to {ToStop.Name} with {TransferType} {MinTransferTime}";
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