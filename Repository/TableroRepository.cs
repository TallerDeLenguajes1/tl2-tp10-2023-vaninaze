using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using EspacioTablero;

namespace kanbanRespository
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        //CREAR Tablero
        public void Create(Tablero tablero)
        {
            var query = $"INSERT INTO tablero (id_usuario_propietario, nombre, descripcion) VALUES (@id_propietario, @nombre, @descrip);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_propietario", tablero.Id_usuario_propietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descrip", tablero.Descripcion));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        //Modificar Tablero
        public void Update(int id, Tablero tablero)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE tablero SET id_usuario_propietario = '{tablero.Id_usuario_propietario}', nombre = '{tablero.Nombre}', descripcion = '{tablero.Descripcion}' WHERE id = '{id}';";
            connection.Open();
            
            command.ExecuteNonQuery();
            connection.Close();
        }
        //LISTAR Tablero
        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }

        //Listar tablero especifico
        public Tablero GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tablero = new Tablero();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM tablero WHERE id = '{id}';";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();
            if(tablero == null){
                throw new Exception("Tablero no encontrado.");
            }
            return (tablero);
        }
        public List<Tablero> GetByIdUsuario(int idUsuario)
        {
            var queryString = $"SELECT * FROM tablero WHERE id_usuario_propietario = '{idUsuario}';";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }

        //Eliminar un tablero por ID
        public void Remove(int id)
        {
            Tablero tab = GetById(id);
            if(tab == null){
                throw new Exception("Tablero no borrado.");
            } else {
                SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM tablero WHERE id = '{id}';";
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}