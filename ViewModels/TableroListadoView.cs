using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TableroListadoView{
        private List<TableroView> viewTableros;

        public List<TableroView> ViewTableros 
        { get => viewTableros; set => viewTableros = value; }

        public TableroListadoView(List<Tablero> tableros, List<Usuario> usuarios){
            viewTableros = new List<TableroView>();
            foreach (var t in tableros){
                foreach (var u in usuarios){
                    if(t.Id_usuario_propietario == u.Id){
                        var viewTablero = new TableroView(t,u);
                        viewTableros.Add(viewTablero);
                    }
                }
            }
        }
    } 
}