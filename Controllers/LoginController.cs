using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;
using tl2_tp10_2023_vaninaze.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace tl2_tp10_2023_vaninaze.Controllers;
using EspacioTablero;
using kanbanRespository;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository usuarioRepo;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository _usuarioRepo)
    {
        _logger = logger;
        usuarioRepo = _usuarioRepo;
    }
    public IActionResult Index()
    {   
        return View(new UsuarioView());
    }        
    [HttpPost]
    public IActionResult Login(UsuarioView usuario)
    {
        try{
            try{
                var usu = usuarioRepo.GetAll().FirstOrDefault(u => u.Nombre_de_usuario == usuario.Nombre_de_usuario && u.Pass == usuario.Pass);

                if(usu == null){ // si el usuario no existe devuelvo al index
                    _logger.LogWarning("Intento de acceso invalido - Usuario: "+usuario.Nombre_de_usuario+" Clave: "+usuario.Pass); //logueo de tipo warning
                    return RedirectToAction("Index");
                } else {
                    LoguearUsuario(usu);
                    _logger.LogInformation("El usuario "+usu.Nombre_de_usuario+" ingreso correctamente"); //logueo de tipo Info
                    return RedirectToRoute(new{controller = "Tablero", action = "Index"});
                }
                return RedirectToAction("Index");
            }catch(Exception ex){
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error");
            }
        } catch (Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }
    private void LoguearUsuario(Usuario usuario){
        HttpContext.Session.SetInt32("id",usuario.Id);
        HttpContext.Session.SetString("usuario",usuario.Nombre_de_usuario);
        HttpContext.Session.SetString("rol",usuario.Rol);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}