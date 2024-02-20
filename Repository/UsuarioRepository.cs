using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

using EspacioModels;

namespace kanbanRepository
{
    public class UsuarioRepository: IUsuarioRepository{
        private string cadenaConexion;

        public UsuarioRepository(string CadenaDeConexion)
        {
            cadenaConexion = CadenaDeConexion;
        }

        //CREAR USUARIO
        public void Create(Usuario usuario)
        {
            var query = $"INSERT INTO usuario (nombre_de_usuario, pass, rol) VALUES (@nombre, @pass, @rol);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.Nombre_de_usuario));
                command.Parameters.Add(new SQLiteParameter("@pass", usuario.Pass));
                command.Parameters.Add(new SQLiteParameter("@rol", (int)usuario.Rol));
                command.ExecuteNonQuery(); //para INSERT, DELETE, UPDATE
                connection.Close();
            }
        }

        //MODIFICAR USUARIO
        public void Update(int id, Usuario usuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE usuario SET nombre_de_usuario = '{usuario.Nombre_de_usuario}',pass = '{usuario.Pass}', rol = '{(int)usuario.Rol}' WHERE id = '{id}';";
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
        //LISTAR TODOS LOS USUARIOS
        public List<Usuario> GetAll()
        {
            var query = $"SELECT * FROM usuario;";
            List<Usuario> usuarios = new List<Usuario>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                        usuario.Pass = reader["pass"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);

                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            if (usuarios == null) {
                throw new Exception("Usuarios no encontrados.");
            }
            return usuarios;
        }
        //OBTENER UN USUARIO
        public Usuario GetById(int id){
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();

            command.CommandText = $"SELECT * FROM usuario WHERE id = @idUsuario;";

            command.Parameters.Add(new SQLiteParameter("@idUsuario", id));

            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuario.Pass = reader["pass"].ToString();
                    usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);
                }
            }
            connection.Close();
            if (usuario == null) {
                throw new Exception("Usuario no encontrado.");
            }
            return usuario;
        }
        //BORRAR UN USUARIO
        public void Delete(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM usuario WHERE id = '{id}';";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}