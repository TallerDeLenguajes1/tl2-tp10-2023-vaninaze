namespace EspacioTablero;
using tl2_tp10_2023_vaninaze.ViewModels;
using System.ComponentModel.DataAnnotations;

public class Usuario
{
    private int id;
    private string nombre_de_usuario;
    private string pass;
    private string rol;
    public int Id { get => id; set => id = value; }
    public string Nombre_de_usuario { get => nombre_de_usuario; set => nombre_de_usuario = value; }
    public string Pass { get => pass; set => pass = value; }
    public string Rol { get => rol; set => rol = value; }

    public Usuario()
    {
    }

    public Usuario(UsuarioCrearView usuarioCrear)
    {
        nombre_de_usuario = usuarioCrear.Nombre_de_usuario;
        rol = usuarioCrear.Rol;
        pass = usuarioCrear.Pass;

    }
    public Usuario(UsuarioModificarView usuarioModificar)
    {
        nombre_de_usuario = usuarioModificar.Nombre_de_usuario;
        rol = usuarioModificar.Rol;
        pass = usuarioModificar.Pass;
    }
}