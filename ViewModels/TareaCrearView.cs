using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TareaCrearView{
        [Required (ErrorMessage ="Este campo es requerido")]
        public int Id_tablero {get;set;}
    
        [Required (ErrorMessage ="Este campo es requerido")]
        [StringLength(100)]
        public string Nombre {get;set;}
        
        [StringLength(200)]
        public string Descripcion {get;set;}
        
        [Required (ErrorMessage ="Este campo es requerido")]
        [StringLength(100)]
        public string Color {get;set;}
        
        [Required (ErrorMessage ="Este campo es requerido")]
        public Estado Estado {get;set;}
        
        public int Id_usuario_asignado {get;set;}
        
        private List<Tablero> tableros;
        private Usuario usuario;

        public List<Tablero> Tableros {get => tableros; set => tableros = value;}
        public Usuario Usuario {get => usuario; set => usuario = value;}

        public TareaCrearView(){}

        public TareaCrearView(Tarea tarea, List<Tablero> tableros, Usuario usuario){
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            Id_usuario_asignado = tarea.Id;
            Tableros = tableros;
            Usuario = usuario;
        }
    }
}