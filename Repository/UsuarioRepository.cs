using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

using EspacioTablero;

namespace kanbanRespository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        //CREAR Usuario
        public void Create(Usuario usuario)
        {
            var query = $"INSERT INTO usuario (nombre_de_usuario,pass,rol) VALUES (@nombre_de_usuario,@pass,@rol);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.Nombre_de_usuario));
                command.Parameters.Add(new SQLiteParameter("@pass", usuario.Pass));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        //Modificar Usuario
        public void Update(int id, Usuario usuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE usuario SET nombre_de_usuario = '{usuario.Nombre_de_usuario}', pass = '{usuario.Pass}', rol = '{usuario.Rol}' WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        //LISTAR Usuarios
        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                        usuario.Pass = reader["pass"].ToString();
                        usuario.Rol = reader["rol"].ToString();
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }

        //Obtener detalles de un usuario por su ID
        public Usuario GetById(int idUsuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM usuario WHERE id = '{idUsuario}';";
            /*command.Parameters.Add(new SQLiteParameter("@idUsu", idUsuario));*/
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre_de_usuario = reader["nombre_de_usuario"].ToString();
                    usuario.Pass = reader["pass"].ToString();
                    usuario.Rol = reader["rol"].ToString();
                }
            }
            connection.Close();
            if(usuario == null){
                throw new Exception("Usuario no encontrado.");
            }
            return (usuario);
        }

        //Eliminar un usuario por ID
        public void Remove(int id)
        {
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM usuario WHERE id = @idUsuario;";
                command.Parameters.Add(new SQLiteParameter("@idUsuario",id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}