using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class TableroListadoView
    { 
        public int Id_sesion {get;set;}
        public List<TableroView> TablerosView {get;set;}        
        
        public TableroListadoView() { }
        
        public TableroListadoView(List<Tablero> tableros, List<Usuario> usuarios){
            TablerosView = new List<TableroView>();
            if(tableros != null) {
                foreach (var tab in tableros)
                {
                    var usu = usuarios.FirstOrDefault(u => u.Id == tab.Id_usuario_propietario);
                    TableroView tablero = new TableroView(tab, usu);
                    TablerosView.Add(tablero);
                }
            }
        }

        public TableroListadoView(List<Tablero> tableros, List<Usuario> usuarios, int idUsu){
            Id_sesion = idUsu;
            TablerosView = new List<TableroView>();
            if(tableros != null) {
                foreach (var tab in tableros)
                {
                    var usu = usuarios.FirstOrDefault(u => u.Id == tab.Id_usuario_propietario);
                    TableroView tablero = new TableroView(tab, usu);
                    TablerosView.Add(tablero);
                }
            }
        }
    }
}