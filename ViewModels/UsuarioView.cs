using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class UsuarioView{
        private int id;
        private string nombre_de_usuario;
        private string rol;
        private string pass;

        public int Id { get => id; set => id = value; }
        public string Nombre_de_usuario { get => nombre_de_usuario; set => nombre_de_usuario = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Pass { get => pass; set => pass = value; }

        public UsuarioView(){}
        
        public UsuarioView(Usuario usuario)
        {
            id = usuario.Id;
            nombre_de_usuario = usuario.Nombre_de_usuario;
            rol = usuario.Rol;
            pass = usuario.Pass;
        }
    }
}