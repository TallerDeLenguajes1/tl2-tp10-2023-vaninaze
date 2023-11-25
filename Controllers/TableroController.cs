using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;

namespace tl2_tp10_2023_vaninaze.Controllers;
using EspacioTablero;
using kanbanRespository;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepo;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepo = new TableroRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {   
        List<Tablero> tableros = tableroRepo.GetAll();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new Tablero());
    }
    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero)
    {
        tableroRepo.Create(tablero);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        Tablero tablero = tableroRepo.GetById(id);
        return View(tablero);
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, Tablero tablero)
    {
        tableroRepo.Update(id,tablero);
        return RedirectToAction("Index");
    }
    public IActionResult EliminarTablero(int id)
    {
        tableroRepo.Remove(id);
        return RedirectToAction("index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}