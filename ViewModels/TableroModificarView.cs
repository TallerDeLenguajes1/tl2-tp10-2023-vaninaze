using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TableroModificarView{
        public int Id {get;set;}

        [Required (ErrorMessage ="este campo es requerido")]
        public int Id_usuario_propietario {get;set;}

        [Required (ErrorMessage ="este campo es requerido")]
        [StringLength(100)]
        public string Nombre {get;set;}

        [StringLength(200)]
        public string Descripcion {get;set;}

        public List<Usuario> Usuarios {get;set;}
        
        public TableroModificarView()
        {
        }
        public TableroModificarView(Tablero tablero, List<Usuario> usuarios)
        {
            Id = tablero.Id;
            Id_usuario_propietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            Usuarios = usuarios;
        }
    } 
}