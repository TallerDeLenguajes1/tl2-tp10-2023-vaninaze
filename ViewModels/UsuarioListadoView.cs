using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class UsuarioListadoView{
        private List<UsuarioView> viewUsuarios;
        
        public List<UsuarioView> ViewUsuarios 
        { get => viewUsuarios; set => viewUsuarios = value; }

        public UsuarioListadoView(List<Usuario> usuarios)
        {
            viewUsuarios = new List<UsuarioView>();
            foreach (var usu in usuarios)
            {
                var viewUsuario = new UsuarioView(usu);
                viewUsuarios.Add(viewUsuario);
            }
        }
    }   
}