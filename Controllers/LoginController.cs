using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;

using kanbanRepository;
using EspacioModels;
using EspacioViewModels;
namespace tl2_tp10_2023_vaninaze.Controllers;

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
        try {
            return View(new LoginViewModel());
        } catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    public IActionResult Login(LoginViewModel usuario)
    {
        try {
            if(ModelState.IsValid){
                try {
                    //Existe el usuario?
                    var usuarioLogueado = usuarioRepo.GetAll().FirstOrDefault(u => u.Nombre_de_usuario == usuario.Nombre_de_usuario && u.Pass == usuario.Pass);

                    //Si el usuario no existe devuelvo al index
                    if (usuarioLogueado == null) {
                        _logger.LogWarning("Intento de acceso invalido - Usuario: "+usuario.Nombre_de_usuario+" Clave ingresada: " + usuario.Pass);

                        ModelState.AddModelError(nameof(LoginViewModel.Nombre_de_usuario), "Nombre de usuario o clave ingresada incorrecta.");
                        ModelState.AddModelError(nameof(LoginViewModel.Pass), "Nombre de usuario o clave ingresada incorrecta.");

                        usuario.MensajeDeError = "Usuario no encontrado";
                        return View("Index", usuario);
                    } else if (usuarioLogueado.Id != 999){
                        //Registro el usuario
                        _logger.LogInformation("El usuario: "+usuarioLogueado.Nombre_de_usuario+" ingreso correctamente");
                        LoguearUsuario(usuarioLogueado);
                    }
                    //Devuelvo el usuario al Home
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                } catch (Exception ex) {
                   _logger.LogError(ex.ToString());
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Index");
        }
    }
    public IActionResult Logout()
    {
        try
        {
            DesloguearUsuario();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar cerrar sesión del usuario. {ex.ToString()}");
        }
        return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    private void LoguearUsuario(Usuario usuario)
    {
        HttpContext.Session.SetInt32("id",usuario.Id);
        HttpContext.Session.SetString("usuario",usuario.Nombre_de_usuario);
        HttpContext.Session.SetString("rol",usuario.Rol.ToString());
    }
    private void DesloguearUsuario()
    {
        HttpContext.Session.Clear();
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
}