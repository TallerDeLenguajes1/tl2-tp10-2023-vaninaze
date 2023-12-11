using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TableroCrearView{
        public int Id {get;set;}

        [Required (ErrorMessage ="Este campo es requerido")]
        public int Id_usuario_propietario {get;set;}

        [Required (ErrorMessage ="Este campo es requerido")]
        [StringLength(100)]
        public string Nombre {get;set;}

        [Required (ErrorMessage ="Este campo es requerido")]
        [StringLength(200)]
        public string Descripcion {get;set;}
        
        public Usuario Usuario {get;set;}
        
        public TableroCrearView()
        {}
        public TableroCrearView(Usuario usuario)
        {
            Usuario = usuario;
        }
    } 
}