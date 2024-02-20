using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class TareaView
    {
        [Display(Name = "Id")]
        public int Id {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id tablero")] 
        public int Id_tablero {get;set;} 

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        [StringLength(80)]
        public string Nombre {get;set;}  

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        [StringLength(100)]
        public string? Descripcion {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Color")]
        public string? Color {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID Usuario")]
        public int Id_usuario_asignado {get;set;} 

        private string? usuario;
        public string? Usuario {get; set;}
        
        private string? tablero;
        public string? Tablero {get; set;}

        public TareaView() { }

        public TareaView(Tarea tarea, Usuario usu, Tablero tab)
        {
            Id = tarea.Id;
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Id_usuario_asignado = usu.Id;
            Usuario = usu.Nombre_de_usuario;
            Tablero = tab.Nombre;
        }
    }
}