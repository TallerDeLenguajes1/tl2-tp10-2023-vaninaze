namespace EspacioTablero;
using tl2_tp10_2023_vaninaze.ViewModels;

public enum Estado
{
    Ideas,
    ToDo,
    Doing,
    Review,
    Done
}
public class Tarea
{
    private int id;
    private int id_tablero;
    private string nombre;
    private Estado estado;
    private string descripcion;
    private string color;
    private int id_usuario_asignado;

    public int Id { get => id; set => id = value; }
    public int Id_tablero { get => id_tablero; set => id_tablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public Estado Estado { get => estado; set => estado = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public int Id_usuario_asignado { get => id_usuario_asignado; set => id_usuario_asignado = value; }

    public Tarea()
    {
    }

    public Tarea(TareaCrearView tareaCrear)
    {
        id_tablero = tareaCrear.Id_tablero;
        nombre = tareaCrear.Nombre;
        descripcion = tareaCrear.Descripcion;
        color = tareaCrear.Color;
        estado = tareaCrear.Estado;
        id_usuario_asignado = tareaCrear.Id_usuario_asignado;
    }
    public Tarea(TareaModificarView tareaModificar)
    {
        id_tablero = tareaModificar.Id_tablero;
        nombre = tareaModificar.Nombre;
        descripcion = tareaModificar.Descripcion;
        color = tareaModificar.Color;
        estado = tareaModificar.Estado;
        id_usuario_asignado = tareaModificar.Id_usuario_asignado;
    }
}