namespace EspacioTablero;
using tl2_tp10_2023_vaninaze.ViewModels;

public class Tablero
{
    private int id;
    private int id_usuario_propietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int Id_usuario_propietario { get => id_usuario_propietario; set => id_usuario_propietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

     public Tablero()
    {
    }
    public Tablero(TableroCrearView tableroCrear)
    {
        id = tableroCrear.Id;
        id_usuario_propietario = tableroCrear.Id_usuario_propietario;
        nombre = tableroCrear.Nombre;
        descripcion = tableroCrear.Descripcion;
    }
    public Tablero(TableroModificarView tableroModificar)
    {
        id = tableroModificar.Id;
        id_usuario_propietario = tableroModificar.Id_usuario_propietario;
        nombre = tableroModificar.Nombre;
        descripcion = tableroModificar.Descripcion;
    }
}