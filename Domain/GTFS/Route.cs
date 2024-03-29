using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainApp.Domain.GTFS;

public class Route
{
    public Route()
    {
        
    }

    [Key]
    public Guid Id { get; set; }
    [Required]
    public Agency Agency { get; set; }
    public string RouteTypeName { get; set; }
    public string LongName { get; set; }
    [Required]
    public RouteType RouteType { get; set; }

    public override string ToString()
    {
        return $"{RouteType}: {RouteTypeName} {LongName}";
    }
}

public enum RouteType : int
{
    // NMBS Specific values
    Stoptrein = 100,
    NJ = 101,
    Intercity = 103,
    Bus = 700,
}