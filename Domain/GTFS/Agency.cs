using System.ComponentModel.DataAnnotations;

namespace TrainApp.Domain.GTFS;

public class Agency
{
    public Agency()
    {
        
    }
    
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Url { get; set; }
    [Required]
    public string Timezone { get; set; }
    public string Language { get; set; }

    public override string ToString()
    {
        return $"Agency: {Name}\nURL: {Url}\nTimezone: {Timezone}\nLanguage: {Language}";
    }
}