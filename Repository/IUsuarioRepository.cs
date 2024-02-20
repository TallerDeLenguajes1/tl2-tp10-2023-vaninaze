using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

using EspacioModels;

namespace kanbanRepository
{
    public interface IUsuarioRepository
    {
        public void Create(Usuario usuario);
        public void Update(int id, Usuario usu);
        public List<Usuario> GetAll();
        public Usuario GetById(int id);
        public void Delete(int id);
    }
}