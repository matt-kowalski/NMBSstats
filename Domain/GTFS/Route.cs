namespace TrainApp.Domain.GTFS;

public class Route
{
    public Route()
    {
        
    }

    public Guid Id { get; set; }
    public Guid AgencyId { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public RouteType RouteType { get; set; }

    public override string ToString()
    {
        return $"{RouteType}: {ShortName} {LongName}";
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