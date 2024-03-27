using TrainApp.Domain;
using TrainApp.BL;
using TrainApp.DAL;
using TrainApp.DAL.IMR;


string rawdata = Environment.CurrentDirectory + "/rawdata";
InMemoryRepository repository = new InMemoryRepository(rawdata);
repository.Seed();