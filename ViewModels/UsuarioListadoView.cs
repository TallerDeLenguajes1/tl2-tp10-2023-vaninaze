using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class UsuarioListadoView
    { 
        public int Id_sesion {get;set;}
        public List<UsuarioView> UsuariosView {get;set;}        
        
        public UsuarioListadoView(List<Usuario> usuarios, int idUsu){
            Id_sesion = idUsu;
            UsuariosView = new List<UsuarioView>();
            foreach (var usu in usuarios)
            {
                var usuarioView = new UsuarioView(usu);
                UsuariosView.Add(usuarioView);
            }
        }

        public UsuarioListadoView(List<Usuario> usuarios){
            UsuariosView = new List<UsuarioView>();
            foreach (var usu in usuarios)
            {
                var usuarioView = new UsuarioView(usu);
                UsuariosView.Add(usuarioView);
            }
        }
    }
}