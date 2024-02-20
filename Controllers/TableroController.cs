using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;

using kanbanRepository;
using EspacioModels;
using EspacioViewModels;
namespace tl2_tp10_2023_vaninaze.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepo;
    private IUsuarioRepository usuarioRepo;
    private ITareaRepository tareaRepo;

    public TableroController(ILogger<TableroController> logger, ITableroRepository _tableroRepo, IUsuarioRepository _usuarioRepo, ITareaRepository _tareaRepo)
    {
        _logger = logger;
        tableroRepo = _tableroRepo;
        usuarioRepo = _usuarioRepo;
        tareaRepo = _tareaRepo;
    }

    [HttpGet]
    public IActionResult Index() //Listar tableros
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try { 
            int id = HttpContext.Session.GetInt32("id") ?? -1;
            if(isAdmin() ){
                //Ve todos los tableros
                List<Tablero> tableros = tableroRepo.GetAll();
                TableroListadoView tablerosView = new TableroListadoView(tableros, usuarioRepo.GetAll(), id);
                return View("Index",tablerosView);
            } else if(isOperador()){ 
                //Obtengo los tableros que le pertenecen 
                List<Tablero> tableros = tableroRepo.GetAllByUsu(id);

                //Obtengo los tableros donde tiene tareas asignadas
                List<Tarea> tareas = tareaRepo.GetByUsuAsignado(id);
                foreach (var t in tareas)
                {
                    if(tableros.FirstOrDefault(tab => tab.Id == t.Id_tablero) == null){ //si el tablero no esta
                        tableros.Add(tableroRepo.GetById(t.Id_tablero));
                    }
                }
                TableroListadoView tablerosView = new TableroListadoView(tableros, usuarioRepo.GetAll(), id);
                return View("Index",tablerosView);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" }); 
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            //Crea tableros propios
            int id = HttpContext.Session.GetInt32("id") ?? -1;
            return View(new TableroCrearView(new Tablero(), usuarioRepo.GetById(id)));
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearTablero(TableroCrearView tab)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(!ModelState.IsValid) { 
                tab.MensajeDeError = "Error al crear Tablero.";
                return View("CrearTablero", tab);
            } else {
                Tablero tablero = new Tablero(tab);
                tableroRepo.Create(tablero);
                return RedirectToAction("Index");
            }
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try { 
            if(isAdmin()){
                //puede modificar el usuario propietario
                Tablero tab = tableroRepo.GetById(id);
                TableroModificarView tablero = new TableroModificarView(tab, usuarioRepo.GetAll());
                return View("ModificarTableroAdmin", tablero);
            } else if(isOperador()){
                //solo modifica el estado
                Tablero tab = tableroRepo.GetById(id);
                TableroModificarView tablero = new TableroModificarView(tab);
                return View("ModificarTableroOpe", tablero);
            }         
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, TableroModificarView tab)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(!ModelState.IsValid) { 
                tab.MensajeDeError = "Error al modificar Tablero.";
                if(isAdmin()){
                    return View("ModificarTableroAdmin", tab);
                } else {
                    return View("ModificarTableroOpe", tab);
                }
            } else {
                Tablero tablero = new Tablero(tab);
                tableroRepo.Update(id, tablero);
                return RedirectToAction("Index"); 
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult EliminarTablero(int id)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            tareaRepo.DeleteByTablero(id);
            tableroRepo.Delete(id);
            return RedirectToAction("Index"); 
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
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