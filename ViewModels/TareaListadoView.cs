using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TareaListadoView{
        private List<TareaView> viewTareas;

        public List<TareaView> ViewTareas 
        { get => viewTareas; set => viewTareas = value; }

        public TareaListadoView(List<Tarea> tareas, List<Tablero> tableros, List<Usuario> usuarios){
            viewTareas = new List<TareaView>();
            foreach(var tarea in tareas){
                var usuario = usuarios.FirstOrDefault(u => u.Id == tarea.Id_usuario_asignado);
                var tablero = tableros.FirstOrDefault(t => t.Id == tarea.Id_tablero);
                viewTareas.Add(new TareaView(tarea,tablero,usuario));
            }
        }
    } 
}