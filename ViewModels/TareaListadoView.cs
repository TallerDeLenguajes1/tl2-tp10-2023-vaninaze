using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioModels;

namespace EspacioViewModels
{
    public class TareaListadoView
    { 
        public int Id_sesion {get;set;}

        public List<TareaView> TareasView {get;set;}        
        
        public TareaListadoView(List<Tarea> tareas, List<Usuario> usuarios, List<Tablero> tableros, int idUsu){
            TareasView = new List<TareaView>();
            Id_sesion = idUsu;
            foreach (var tarea in tareas)
            {
                Usuario usuario = usuarios.FirstOrDefault(u => u.Id == tarea.Id_usuario_asignado);
                Tablero tablero = tableros.FirstOrDefault(t => t.Id == tarea.Id_tablero);
                var tareaView = new TareaView(tarea, usuario, tablero);
                
                TareasView.Add(tareaView);
            }
        }
    }
}