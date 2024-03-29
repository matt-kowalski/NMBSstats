using Microsoft.AspNetCore.Mvc;
using TrainApp.BL;

namespace Website.Controllers;

public class MapController : Controller
{
    private readonly IManager _man;

    public MapController(IManager man)
    {
        _man = man;
    }

    public IActionResult Index()
    {
        ViewBag.stations = _man.GetAllStations();
        return View();
    }
}