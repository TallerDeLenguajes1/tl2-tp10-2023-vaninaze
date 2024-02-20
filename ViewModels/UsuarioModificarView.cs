using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class UsuarioModificarView
    {
        public string MensajeDeError;

        public bool TieneMensajeDeError => !string.IsNullOrEmpty(MensajeDeError);
        
        [Display(Name = "Id")]
        public int Id {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre_de_usuario {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [Display(Name = "Pass")]
        public string Pass {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Rol")]
        public Roles Rol {get;set;}

        public UsuarioModificarView() { }

        public UsuarioModificarView(Usuario usu)
        {
            Id = usu.Id;
            Nombre_de_usuario = usu.Nombre_de_usuario;
            Pass = usu.Pass;
            Rol = usu.Rol;
        }
    }
}