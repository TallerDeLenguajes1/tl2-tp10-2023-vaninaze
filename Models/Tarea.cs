using EspacioViewModels;
namespace EspacioModels;

public enum EstadoTarea {
    Idea = 0,
    Hacer = 1,
    Haciendo = 2,
    Revisar = 3,
    Terminada = 4
}
public class Tarea {
    private int id;
    private int id_tablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private int id_usuario_asignado;

    public int Id { get => id; set => id = value; }
    public int Id_tablero { get => id_tablero; set => id_tablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public int Id_usuario_asignado { get => id_usuario_asignado; set => id_usuario_asignado = value; }

    public Tarea(){ }

    public Tarea(TareaCrearView tarea){
        id_tablero = tarea.Id_tablero;
        nombre = tarea.Nombre;
        descripcion = tarea.Descripcion;
        color = tarea.Color;
        estado = tarea.Estado;
        id_usuario_asignado = tarea.Id_usuario_asignado;
    }
    public Tarea(TareaModificarView tarea){
        id = tarea.Id;
        id_tablero = tarea.Id_tablero;
        nombre = tarea.Nombre;
        descripcion = tarea.Descripcion;
        color = tarea.Color;
        estado = tarea.Estado;
        id_usuario_asignado = tarea.Id_usuario_asignado;
    }
    
}