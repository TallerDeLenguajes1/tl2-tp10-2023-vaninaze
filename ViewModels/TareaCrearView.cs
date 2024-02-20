using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class TareaCrearView
    {
        public string MensajeDeError;

        public bool TieneMensajeDeError => !string.IsNullOrEmpty(MensajeDeError);
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id tablero")] 
        public int Id_tablero {get;set;} 

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "La longitud de la cadena debe ser entre 4 y 40 caracteres")]
        [Display(Name = "Nombre")] 
        public string Nombre {get;set;}  

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(80, MinimumLength = 4, ErrorMessage = "La longitud de la cadena debe ser entre 4 y 80 caracteres")]
        [Display(Name = "Descripcion")]
        public string? Descripcion {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(8, ErrorMessage = "La longitud de la cadena debe ser de 8 caracteres")]
        [Display(Name = "Color")]
        public string? Color {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID Usuario")]
        public int Id_usuario_asignado {get;set;}
        
        public int Id_sesion {get;set;}

        public List<Usuario>? Usuarios {get;set;}

        public List<Tablero>? Tableros {get;set;}

        public TareaCrearView() { }

        public TareaCrearView(Tarea tarea, int idUsu, List<Usuario> usuarios, List<Tablero> tableros)
        {
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Id_usuario_asignado = tarea.Id_usuario_asignado;
            Id_sesion = idUsu;
            Usuarios = usuarios;
            Tableros = tableros;
        }

        public TareaCrearView(Tarea tarea, int idUsu, List<Tablero> tableros)
        {
            Id_tablero = tarea.Id_tablero;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Id_usuario_asignado = tarea.Id_usuario_asignado;
            Id_sesion = idUsu;
            Tableros = tableros;
        }
    }
}