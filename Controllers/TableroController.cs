using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;
using tl2_tp10_2023_vaninaze.ViewModels;
namespace tl2_tp10_2023_vaninaze.Controllers;
using EspacioTablero;
using kanbanRespository;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepo;
    private IUsuarioRepository usuarioRepo;

    public TableroController(ILogger<TableroController> logger, ITableroRepository _tableroRepo, IUsuarioRepository _usuarioRepo)
    {
        _logger = logger;
        tableroRepo = _tableroRepo;
        usuarioRepo = _usuarioRepo;
    }
    [HttpGet]
    public IActionResult Index()
    {   
        try{
            if(HttpContext.Session.GetString("rol") != null){
                if(isAdmin()){
                    TableroListadoView viewTablerosAdm = new TableroListadoView(tableroRepo.GetAll(), usuarioRepo.GetAll());
                    return View(viewTablerosAdm);

                } else if(isOperador()){
                    TableroListadoView viewTablerosOp = new TableroListadoView(tableroRepo.GetAll().FindAll(t => t.Id_usuario_propietario == HttpContext.Session.GetInt32("id")), usuarioRepo.GetAll());
                    return View(viewTablerosOp);
                }
            } 
            return RedirectToRoute(new{controller = "Login", action = "Index"});
        }catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult CrearTablero()
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else {
                Usuario usu = usuarioRepo.GetAll().FirstOrDefault(u => u.Id == HttpContext.Session.GetInt32("id"));
                return View(new TableroCrearView(usu));
            }
            return RedirectToAction("Index");
        }catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult CrearTablero(TableroCrearView viewTablero)
    {
        try{
            /*if(ModelState.IsValid){*/
                if(HttpContext.Session.GetString("rol") == null){
                    return RedirectToRoute(new{controller = "Login", action = "Index"});
                } else {
                    Tablero tablero = new Tablero(viewTablero);
                    tableroRepo.Create(tablero);
                }
            /*}*/
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpGet]
    public IActionResult ModificarTablero(int id) //id del tablero
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else {
                Tablero tablero = tableroRepo.GetById(id);
                if(isAdmin()){
                    TableroModificarView viewTablero = new TableroModificarView(tablero, usuarioRepo.GetAll());
                    return View(viewTablero);

                } else if(isOperador()){
                    if(HttpContext.Session.GetInt32("id")==tablero.Id_usuario_propietario){
                        TableroModificarView viewTablero = new TableroModificarView(tablero, usuarioRepo.GetAll());
                        return View(viewTablero);
                    }
                }
            }
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    [HttpPost]
    public IActionResult ModificarTablero(int id, TableroModificarView viewTablero)
    {
        try{
            /*if(ModelState.IsValid){*/
                if(HttpContext.Session.GetString("rol") == null){
                   return RedirectToRoute(new{controller = "Login", action = "Index"});
                } else {
                    Tablero tablero = new Tablero(viewTablero);
                    tableroRepo.Update(id,tablero);
                }
            /*}*/
            return RedirectToAction("Index");
        } catch (Exception exc){
            _logger.LogError(exc.ToString());
            return RedirectToAction("Error");
        }
    }
    public IActionResult EliminarTablero(int id)
    {
        try{
            if(HttpContext.Session.GetString("rol") == null){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            } else if(isAdmin()){
                tableroRepo.Remove(id);
            }else if(isOperador()){
                var tablero = tableroRepo.GetById(id);
                if(tablero!=null && tablero.Id_usuario_propietario == HttpContext.Session.GetInt32("id")){
                    tableroRepo.Remove(id);
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