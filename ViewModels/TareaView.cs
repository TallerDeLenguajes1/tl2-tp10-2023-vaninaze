using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TareaView{
        private int id;
        private string nombreTablero;
        private string nombre;
        private Estado estado;
        private string descripcion;
        private string color;
        private string usuarioAsignado;

        public int Id { get => id; set => id = value; }
        public string NombreTablero { get => nombreTablero; set => nombreTablero = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public Estado Estado { get => estado; set => estado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Color { get => color; set => color = value; }
        public string UsuarioAsignado { get => usuarioAsignado; set => usuarioAsignado = value; }

        public TareaView(Tarea tarea, Tablero tablero, Usuario usuario)
        {
            id = tarea.Id;
            if(tablero == null){
                nombreTablero = "Sin tablero";
            }else{
               nombreTablero = tablero.Nombre;
            }
            nombre = tarea.Nombre;
            estado = tarea.Estado;
            descripcion = tarea.Descripcion;
            color = tarea.Color;
            if(usuario == null){
                usuarioAsignado = "Sin usuario asignado";
            }else{
                usuarioAsignado = usuario.Nombre_de_usuario;
            }
        }
    }
}