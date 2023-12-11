using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class UsuarioModificarView{
        public int Id {get;set;}

        [Required (ErrorMessage ="Este campo es requerido")]
        [StringLength(100)]
        public string Nombre_de_usuario {get;set;}

        [Required (ErrorMessage ="Este campo es requerido")]
        [StringLength(50)]
        public string Rol {get;set;}
        
        [Required (ErrorMessage ="Este campo es requerido")]
        [StringLength(20)]
        public string Pass {get;set;}

        public UsuarioModificarView()
        {
        }

        public UsuarioModificarView(Usuario usuario)
        {
            Id = usuario.Id;
            Nombre_de_usuario = usuario.Nombre_de_usuario;
            Rol = usuario.Rol;
            Pass = usuario.Pass;
        }
    }    
}