using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class TableroCrearView
    {
        public string MensajeDeError;

        public bool TieneMensajeDeError => !string.IsNullOrEmpty(MensajeDeError);
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Id Usuario")] 
        public int Id_usuario_propietario {get;set;} 

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string Nombre {get;set;}         
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        public string? Descripcion {get;set;}

        private Usuario? usuario;
        public Usuario? Usuario {get;set;}
        
        public TableroCrearView() { }

        public TableroCrearView(Tablero tablero, Usuario usu)
        {
            Id_usuario_propietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            Usuario = usu;
        }
    }
}