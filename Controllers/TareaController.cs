using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;

using kanbanRepository;
using EspacioModels;
using EspacioViewModels;
namespace tl2_tp10_2023_vaninaze.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepo;
    private IUsuarioRepository usuarioRepo;
    private ITableroRepository tableroRepo;

    public TareaController(ILogger<TareaController> logger, ITareaRepository _tareaRepo, IUsuarioRepository _usuarioRepo, ITableroRepository _tableroRepo)
    {
        _logger = logger;
        tareaRepo = _tareaRepo;
        usuarioRepo = _usuarioRepo;
        tableroRepo = _tableroRepo;
    }

    [HttpGet]
    public IActionResult Index() //Listar tareas
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        }       
        try {
            int id = HttpContext.Session.GetInt32("id") ?? -1;
            List<Tarea> tareas = tareaRepo.GetAll();
            TareaListadoView tareasView = new TareaListadoView(tareas, usuarioRepo.GetAll(), tableroRepo.GetAll(), id);
            return View(tareasView);
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            int id = HttpContext.Session.GetInt32("id") ?? -1;
            if(isAdmin()){
                return View("CrearTareaAdmin", new TareaCrearView(new Tarea(), id, usuarioRepo.GetAll(), tableroRepo.GetAll()));
            } else if(isOperador()){
                //crea tareas propias
                return View("CrearTareaOpe", new TareaCrearView(new Tarea(), id, tableroRepo.GetAll()));
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(TareaCrearView tarea)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(!ModelState.IsValid) { 
                tarea.MensajeDeError = "Error al crear Tarea.";
                if(isAdmin()){
                    return View("CrearTareaAdmin", tarea);
                } else {
                    return View("CrearTareaOpe", tarea);
                }
            } else {
                Tarea tareaNueva = new Tarea(tarea);
                tareaRepo.Create(tareaNueva);
                return RedirectToAction("Index");
            }
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            Tarea tarea = tareaRepo.GetById(id);
            if(isAdmin()){
                TareaModificarView tareaView = new TareaModificarView(tarea, usuarioRepo.GetAll());
                return View("ModificarTareaAdmin", tareaView);
            } else if(isOperador()){
                TareaModificarView tareaView = new TareaModificarView(tarea);
                return View("ModificarTareaOpe", tareaView);
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, TareaModificarView tareaView)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(!ModelState.IsValid) { 
                tareaView.MensajeDeError = "Error al modificar Tarea.";
                if(isAdmin()){
                    return View("ModificarTareaAdmin", tareaView);
                } else {
                    return View("ModificarTareaOpe", tareaView);
                }
            } else {
                Tarea tarea = new Tarea(tareaView);
                tareaRepo.Update(id, tarea);
                return RedirectToAction("Index");
            }
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    public IActionResult EliminarTarea(int id)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            tareaRepo.Delete(id);
            return RedirectToAction("Index");
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private bool isLogueado()
    {
        if (HttpContext.Session != null && !string.IsNullOrEmpty(HttpContext.Session.GetString("usuario"))) 
        {
            return true;
        }                
        return false;
    }
    private bool isAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("rol") == "Administrador") 
        {
            return true;
        }                
        return false;
    }
    private bool isOperador()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("rol") == "Operador") 
        {
            return true;
        }                
        return false;
    }
}