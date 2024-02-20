using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EspacioViewModels
{
    public class LoginViewModel
    {
        public string MensajeDeError;

        public bool TieneMensajeDeError => !string.IsNullOrEmpty(MensajeDeError);
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(8)]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre_de_usuario {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Pass {get;set;}
    }
}