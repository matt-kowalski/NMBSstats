using System.ComponentModel.DataAnnotations;

namespace TrainApp.Domain.GTFS;

public class Shape
{
    public Shape()
    {
        
    }
    
    [Key]
    public Guid Id { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    public uint PointSequence { get; set; }
    public double DistanceTraveled { get; set; }
}