using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;

using kanbanRepository;
using EspacioModels;
using EspacioViewModels;
namespace tl2_tp10_2023_vaninaze.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository usuarioRepo;
    private ITareaRepository tareaRepo;
    private ITableroRepository tableroRepo;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository _usuarioRepo, ITareaRepository _tareaRepo, ITableroRepository _tableroRepo)
    {
        _logger = logger;
        usuarioRepo = _usuarioRepo;
        tareaRepo = _tareaRepo;
        tableroRepo = _tableroRepo;
    }

    [HttpGet]
    public IActionResult Index() //Listar usuarios
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            int id = HttpContext.Session.GetInt32("id") ?? -1;
            if(isAdmin()){
                UsuarioListadoView usuarios = new UsuarioListadoView(usuarioRepo.GetAll(), id);
                return View("IndexAdmin", usuarios);
            } else if(isOperador()){
                UsuarioListadoView usuarios = new UsuarioListadoView(usuarioRepo.GetAll(), id);
                return View("IndexOpe", usuarios);
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        } catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(isAdmin()){
                return View(new UsuarioCrearView());
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult CrearUsuario(UsuarioCrearView usu)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(!ModelState.IsValid) { 
                usu.MensajeDeError = "Error al crear Usuario";
                return View("CrearUsuario",usu);
            } else { 
                Usuario usuario = new Usuario(usu);
                usuarioRepo.Create(usuario);
                return RedirectToAction("Index");
            }
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(id != 999){
                int idSesion = HttpContext.Session.GetInt32("id") ?? -1;
                if(isAdmin()){
                    Usuario usuario = usuarioRepo.GetById(id);
                    UsuarioModificarView usu = new UsuarioModificarView(usuario);
                    if(idSesion == id){
                        return View("ModificarUsuarioAdmin", usu);
                    }
                    return View("ModificarRol", usu);
                } else if(isOperador()){
                    Usuario usuario = usuarioRepo.GetById(id);
                    UsuarioModificarView usu = new UsuarioModificarView(usuario);
                    return View("ModificarUsuarioOpe", usu);
                }
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult ModificarUsuario(int id, UsuarioModificarView usu)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(!ModelState.IsValid) { 
                usu.MensajeDeError = "Error al modificar Usuario.";
                if(isAdmin()) {
                    return View("ModificarUsuarioAdmin", usu);
                } else {
                    return View("ModificarUsuarioOpe", usu);
                }
            } else if(id != 999){
                Usuario usuario = new Usuario(usu);
                usuarioRepo.Update(id, usuario);
            }
            return RedirectToAction("Index");
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    public IActionResult EliminarUsuario(int id)
    {
        if(!isLogueado()) {
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
        } 
        try {
            if(isAdmin()){
                tareaRepo.DeleteByUsu(id);
                tableroRepo.DeleteByUsu(id);
                usuarioRepo.Delete(id);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" }); 
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