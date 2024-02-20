using EspacioModels;

namespace kanbanRepository
{
    public interface ITareaRepository
    {
        public void Create(Tarea tarea);
        public void Update(int id, Tarea tarea);
        public Tarea GetById(int id);
        public List<Tarea> GetAll();
        public List<Tarea> GetByUsuAsignado(int idUsu);
        public List<Tarea> GetByTablero(int idTablero);
        public void Delete(int id);
        public void AsignarTarea(int idUsu, int idTarea);
        public void DeleteByTablero(int idTab);
        public void DeleteByUsu(int idUsu);
    }
}