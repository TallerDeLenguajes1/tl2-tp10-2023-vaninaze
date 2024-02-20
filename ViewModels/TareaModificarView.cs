using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class TareaModificarView
    {
        public string MensajeDeError;

        public bool TieneMensajeDeError => !string.IsNullOrEmpty(MensajeDeError);
        
        [Display(Name = "Id")]
        public int Id {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id tablero")] 
        public int Id_tablero {get;set;} 

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string Nombre {get;set;}  

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        public string? Descripcion {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Color")]
        public string? Color {get;set;}
 
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID Usuario")]
        public int Id_usuario_asignado {get;set;} 

        public List<Usuario>? Usuarios {get;set;}

        public TareaModificarView() { }

        public TareaModificarView(Tarea tarea)
        {
            Id = tarea.Id;
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Id_usuario_asignado = tarea.Id_usuario_asignado;
        }

        public TareaModificarView(Tarea tarea, List<Usuario> usuarios)
        {
            Id = tarea.Id;
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Id_usuario_asignado = tarea.Id_usuario_asignado;
            Usuarios = usuarios;
        }
    }
}