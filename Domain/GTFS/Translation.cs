using System.Globalization;

namespace TrainApp.Domain.GTFS;

public class Translation
{
    public Translation()
    {
        
    }

    public TableName TableName { get; set; }
    public string FieldName { get; set; }
    public CultureInfo Language { get; set; }
    public string TranslatedValue { get; set; }
    public string FieldValue { get; set; }

    public override string ToString()
    {
        return $"[{TableName} {FieldName}] {FieldValue} in {Language.DisplayName} is: {TranslatedValue}";
    }
}

public enum TableName : byte
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