/*Listar, Crear, Modificar y Eliminar Tareas. (Por el
momento asuma que el tablero al que pertenece la tarea es siempre la misma, y que no posee usuario asignado)*/

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;

namespace tl2_tp10_2023_vaninaze.Controllers;
using kanbanRespository;
using EspacioTablero;
public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepo;
    private ITableroRepository tableroRepo;
    private IUsuarioRepository usuarioRepo;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepo = new TareaRepository();
        tableroRepo = new TableroRepository();
        usuarioRepo = new UsuarioRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Tarea> tareas = tareaRepo.GetAll();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        return View(new Tarea());
    }
    [HttpPost]
    public IActionResult CrearTarea(Tarea tarea)
    {
        tareaRepo.Create(1,tarea); //tablero 1
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        Tarea tarea = tareaRepo.GetById(id);
        return View(tarea);
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, Tarea tarea)
    {
        tareaRepo.Update(id,tarea);
        return RedirectToAction("Index");
    }
    public IActionResult EliminarTarea(int id)
    {
        tareaRepo.Remove(id);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}