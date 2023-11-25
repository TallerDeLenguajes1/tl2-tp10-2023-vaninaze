using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EspacioTablero;

namespace kanbanRespository
{
    public interface ITareaRepository
    {
        public void Create(int idTablero, Tarea tarea);
        public void Update(int id, Tarea tarea);
        /*public void UpdateName(int id, string nombre);
        public void UpdateEstado(int id, Estado estado);*/
        public Tarea GetById(int id);
        public List<Tarea> ListarDeUsuario(int idUsuario);
        public List<Tarea> ListarDeTablero(int idTablero);
        public List<Tarea> GetAll();
        public int GetCantEstado(Estado estado);
        public void Remove(int id);
        public void AsignarUsuario (int idUsuario, int idTarea);
    }
}