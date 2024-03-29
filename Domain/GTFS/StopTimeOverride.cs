using System.ComponentModel.DataAnnotations;

namespace TrainApp.Domain.GTFS;

public class StopTimeOverride
{
    public StopTimeOverride()
    {
        
    }

    [Key]
    public Trip Trip { get; set; }
    [Key]
    public Calendar Calendar { get; set; }
    [Key]
    public uint StopSequence { get; set; }
    [Required]
    public Stop Stop { get; set; }
}