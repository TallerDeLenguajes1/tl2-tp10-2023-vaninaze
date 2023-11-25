//En el controlador de usuarios : Listar, Crear, Modificar y Eliminar Usuarios
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_vaninaze.Models;

namespace tl2_tp10_2023_vaninaze.Controllers;
using EspacioTablero;
using kanbanRespository;
public class UsuarioController : Controller
{
    private static IUsuarioRepository usuarioRepo = new UsuarioRepository();
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        List<Usuario> usuarios = usuarioRepo.GetAll();
        return View(usuarios);
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
    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        usuarioRepo.Create(usuario);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        Usuario usu = usuarioRepo.GetById(id);
        if(usu != null){
            usuarioRepo.Update(usu);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuario)
    {
        usuarioRepo.Update(usuario);
        return RedirectToAction("Index");
    }
    [HttpDelete]
    public IActionResult EliminarUsuario(int id){
        usuarioRepo.Remove(id);
        return RedirectToAction("Index");
    }   
}