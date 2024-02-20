using EspacioViewModels;
namespace EspacioModels;
public enum Roles 
{
    Operador = 0,
    Administrador = 1
}
public class Usuario{
    private int id;
    private string nombre_de_usuario;
    private string pass;
    private Roles rol;

    public int Id { get => id; set => id = value; }
    public string Nombre_de_usuario { get => nombre_de_usuario; set => nombre_de_usuario = value; }
    public string Pass { get => pass; set => pass = value; }
    public Roles Rol { get => rol; set => rol = value; }

    public Usuario(){ }

    public Usuario(UsuarioCrearView usu){
        nombre_de_usuario = usu.Nombre_de_usuario;
        pass = usu.Pass;
        rol = usu.Rol;
    }

    public Usuario(UsuarioModificarView usu){
        id = usu.Id;
        nombre_de_usuario = usu.Nombre_de_usuario;
        pass = usu.Pass;
        rol = usu.Rol;
    }
}