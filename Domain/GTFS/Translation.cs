using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TrainApp.Domain.GTFS;

public class Translation
{
    public Translation()
    {
        
    }
    
    [Key]
    public TableType TableType { get; set; }
    [Key]
    public string FieldName { get; set; }
    [Key]
    public string Language { get; set; }
    [Key]
    public string FieldValue { get; set; }
    public string TranslatedValue { get; set; }

    public override string ToString()
    {
        return $"[{TableType} {FieldName}] {FieldValue} in {Language} is: {TranslatedValue}";
    }
}

public enum TableType : byte
{
    Agency = 0,
    Stops = 1,
    Routes = 2,
    Trips = 3,
    StopTimes = 4,
    Pathways = 5,
    Levels = 6,
    FeedInfo = 7,
    Attributions = 8
}