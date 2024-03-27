using System.Globalization;

namespace TrainApp.Domain.GTFS;

public class Agency
{
    public Agency()
    {
        
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public TimeZoneInfo Timezone { get; set; }
    public CultureInfo Language { get; set; }

    public override string ToString()
    {
        return $"Agency: {Name}\nURL: {Url}\nTimezone: {Timezone}\nLanguage: {Language.DisplayName}";
    }
}