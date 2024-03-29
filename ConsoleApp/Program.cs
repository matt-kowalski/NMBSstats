using TrainApp.BL;
using TrainApp.DAL.IMR;
using TrainApp.Domain.GTFS;


InMemoryRepository repository = new InMemoryRepository();
Manager man = new Manager(repository);

Console.WriteLine("station,lat,lon");
foreach (Stop station in man.GetAllStations())
{
    string translatedName = man.GetTranslation("nl", station.Name).TranslatedValue;
    Console.Out.WriteLine($"{translatedName},{station.Latitude},{station.Longitude}");
}