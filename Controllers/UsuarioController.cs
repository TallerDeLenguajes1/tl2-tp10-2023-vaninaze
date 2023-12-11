//En el controlador de usuarios : Listar, Crear, Modificar y Eliminar Usuarios
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;
using tl2_tp10_2023_vaninaze.ViewModels;
namespace tl2_tp10_2023_vaninaze.Controllers;
using EspacioTablero;
using kanbanRespository;
public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository usuarioRepo;
    
    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository _usuarioRepo)
    {
        _logger = logger;
        usuarioRepo = _usuarioRepo;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else if(isAdmin()) {
                UsuarioListadoView usuarios = new UsuarioListadoView(usuarioRepo.GetAll());
                return View(usuarios);
            } else {
                UsuarioListadoView usuarios = new UsuarioListadoView(usuarioRepo.GetAll().FindAll(u => u.Id ==  HttpContext.Session.GetInt32("id")));
                return View(usuarios);
            }
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            /*return BadRequest();*/
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else if(isAdmin()) {
                return View(new UsuarioCrearView());
            } else {
                return RedirectToAction("Index");
            }
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearUsuario(UsuarioCrearView usuarioCrear)
    {
        try{
            if(ModelState.IsValid){
                if(HttpContext.Session.GetString("rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                } else if(isAdmin()) {
                    var usuario = new Usuario(usuarioCrear);
                    usuarioRepo.Create(usuario);
                    return RedirectToAction("Index");
                }
            } 
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id) //recibe el id del usuario
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else if(isAdmin()) {
                var usuarioModificarView = new UsuarioModificarView(usuarioRepo.GetById(id));
                return View("ModificarUsuario",usuarioModificarView);

            } else if(isOperador() && HttpContext.Session.GetInt32("id") == id){
                var usuarioModificarView = new UsuarioModificarView(usuarioRepo.GetById(id));
                return View("ModificarUsuarioOperador",usuarioModificarView);
            }
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, UsuarioModificarView usuarioModificar)
    {
        try{
            if(ModelState.IsValid){
                if(HttpContext.Session.GetString("rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                } else if(isAdmin()) {
                    var usuario = new Usuario(usuarioModificar);
                    usuarioRepo.Update(id, usuario);
                }else if(HttpContext.Session.GetInt32("id") == usuarioModificar.Id){
                    var usuario = new Usuario(usuarioModificar);
                    usuarioRepo.Update(id, usuario);
                }
            }
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult EliminarUsuario(int id){
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else if(isAdmin()) {
                usuarioRepo.Remove(id);
            }else if(HttpContext.Session.GetInt32("id") == id){
                    usuarioRepo.Remove(id);
            }
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
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