using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainApp.Domain.GTFS;

public class Trip
{
    public Trip()
    {
        
    }

    [Key]
    public Guid Id { get; set; }
    [Required]
    public Route Route { get; set; }
    [Required]
    public Calendar Calendar { get; set; }

    [Required]
    public string Headsign { get; set; }

    [Required]
    public string TrainName { get; set; }
    public Guid ShapeId { get; set; }
    public Guid BlockId { get; set; }

    public override string ToString()
    {
        return $"Trip {Id} to {Headsign} on train {TrainName}";
    }
}