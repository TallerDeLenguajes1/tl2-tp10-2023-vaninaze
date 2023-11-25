using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EspacioTablero;

namespace kanbanRespository
{
    public interface ITableroRepository
    {
        public void Create(Tablero tablero);
        public void Update(int id, Tablero tablero);
        public Tablero GetById(int id);
        public List<Tablero> GetByIdUsuario(int idUsuario);
        public List<Tablero> GetAll();
        public void Remove(int id);
    }
}