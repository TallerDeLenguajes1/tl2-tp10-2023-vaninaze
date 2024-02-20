using EspacioModels;

namespace kanbanRepository
{
    public interface ITableroRepository
    {
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public Tablero GetById(int id);
        public List<Tablero> GetAll();
        public List<Tablero> GetAllByUsu(int idUsu);
        public void Delete(int id);
        public void DeleteByUsu(int idUsu);
    }
}