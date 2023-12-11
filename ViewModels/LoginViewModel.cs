using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Pass {get;set;}

        public LoginViewModel(Usuario usuario)
        {
            Nombre = usuario.Nombre_de_usuario;
            Pass = usuario.Pass;
        }

        public LoginViewModel(){}
    }
}