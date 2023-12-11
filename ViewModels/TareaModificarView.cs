using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TareaModificarView{
        public int Id {get;set;}
        
        [Required (ErrorMessage ="este campo es requerido")]
        public int Id_tablero {get;set;}

        [Required (ErrorMessage ="este campo es requerido")]
        [StringLength(100)]
        public string Nombre {get;set;}
        
        [StringLength(2000)]
        public string Descripcion {get;set;}
        
        [Required (ErrorMessage ="este campo es requerido")]
        [StringLength(100)]
        public string Color {get;set;}
        
        [Required (ErrorMessage ="este campo es requerido")]
        public Estado Estado {get;set;}
        
        public int Id_usuario_asignado {get;set;}

        private List<Tablero> tableros;
        private List<Usuario> usuarios;

        public List<Tablero> Tableros {get => tableros; set => tableros = value;}
        public List<Usuario> Usuarios {get => usuarios; set => usuarios = value;}

        public TareaModificarView(){}

        public TareaModificarView(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios){
            Id = tarea.Id;
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            Id_usuario_asignado = tarea.Id_usuario_asignado;
            Tableros = tableros;
            Usuarios = usuarios;
        }
    }
}