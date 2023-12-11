/*Listar, Crear, Modificar y Eliminar Tareas. (Por el
momento asuma que el tablero al que pertenece la tarea es siempre la misma, y que no posee usuario asignado)*/

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;
using tl2_tp10_2023_vaninaze.ViewModels;
namespace tl2_tp10_2023_vaninaze.Controllers;
using kanbanRespository;
using EspacioTablero;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepo;
    private ITableroRepository tableroRepo;
    private IUsuarioRepository usuarioRepo;

    public TareaController(ILogger<TareaController> logger, ITareaRepository _tareaRepo, ITableroRepository _tableroRepo, IUsuarioRepository _usuarioRepo)
    {
        _logger = logger;
        tareaRepo = _tareaRepo; //inyeccion de dependencias
        tableroRepo = _tableroRepo;
        usuarioRepo = _usuarioRepo;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else {
                if(isAdmin()){
                    TareaListadoView tareas = new TareaListadoView(tareaRepo.GetAll(), tableroRepo.GetAll(), usuarioRepo.GetAll());
                    return View(tareas);
                } else if(isOperador()){
                    TareaListadoView tareas = new TareaListadoView(tareaRepo.GetAll().FindAll(t => t.Id_usuario_asignado ==  HttpContext.Session.GetInt32("id")), tableroRepo.GetAll(), usuarioRepo.GetAll());
                    return View(tareas);
                }
            }
            return RedirectToRoute(new{Controller = "Login", action = "Index"});
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else {
                Usuario usu = usuarioRepo.GetAll().FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("id"));
                return View(new TareaCrearView(new Tarea(), tableroRepo.GetAll(), usu));
            }
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearTarea(TareaCrearView tareaCrear)
    {
        try{
            /*if(ModelState.IsValid){*/
                if(HttpContext.Session.GetString("rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                } else {
                    var tarea = new Tarea(tareaCrear);
                    tareaRepo.Create(tarea);
                }
            /*}*/
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarTarea(int id) //recibe el id de la tarea
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else {
                var tarea = tareaRepo.GetAll().FirstOrDefault(t => t.Id == id);
                if(isAdmin()){
                    return View(new TareaModificarView(tarea, tableroRepo.GetAll(), usuarioRepo.GetAll()));
                } else if(isOperador()){
                    if(HttpContext.Session.GetInt32("id") == tarea.Id_usuario_asignado){
                        return View(new TareaModificarView(tarea, tableroRepo.GetAll(), usuarioRepo.GetAll()));
                    }
                }
                return RedirectToAction("Index");
            }
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult ModificarTarea(int id, TareaModificarView tareaModificar)
    {
        try{
            /*if(ModelState.IsValid){*/
                if(HttpContext.Session.GetString("rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                } else {
                    Tarea tarea = new Tarea(tareaModificar);
                    if(isAdmin()){
                        tareaRepo.Update(id,tarea);
                    } else if(isOperador() && HttpContext.Session.GetInt32("id") == tarea.Id_usuario_asignado){
                        tareaRepo.Update(id,tarea);
                    }
                }
            /*}*/
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult EliminarTarea(int id)
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else if(isAdmin()){
                tareaRepo.Remove(id);
            } else {
                var tarea = tareaRepo.GetById(id);
                if(tarea!=null && HttpContext.Session.GetInt32("id") == tarea.Id_usuario_asignado){
                    tareaRepo.Remove(id);
                }
            }
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private bool isAdmin(){
        if(HttpContext.Session != null && HttpContext.Session.GetString("rol") == "Administrador"){
            return true;
        }
        return false;
    }
    private bool isOperador(){
        if(HttpContext.Session != null && HttpContext.Session.GetString("rol") == "Operador"){
            return true;
        }
        return false;
    }
}