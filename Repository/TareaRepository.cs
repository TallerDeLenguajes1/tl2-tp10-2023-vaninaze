using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite; 
using EspacioTablero;

namespace kanbanRespository
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        //CREAR Tarea
        public void Create(int idTablero, Tarea tarea)
        {
            var query = $"INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tab, @nombre, @estado, @descrip, @color, @id_usu);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tab", tarea.Id_tablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descrip", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usu", tarea.Id_usuario_asignado));

                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void Update(int id, Tarea tarea){
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = @"UPDATE Tarea SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @id_usuario WHERE id = @idTarea;";
                command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.Id_tablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario", tarea.Id_usuario_asignado));
                command.Parameters.Add(new SQLiteParameter("@idTarea", id));
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        //Modificar Tarea
        public void UpdateName(int id, string nombre)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE tarea SET nombre = '{nombre}' WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateEstado(int id, Estado estado)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE tarea SET estado = '{estado}' WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public int GetCantEstado(Estado estado){
            int cont = 0;
            var queryString = $"SELECT * FROM tarea WHERE estado = '{estado}';";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cont++;
                    }
                    return cont;
                }
                connection.Close();
            }
            return cont;
        }
        //LISTAR Tablero
        public List<Tarea> ListarDeTablero(int idTablero)
        {
            var queryString = $"SELECT * FROM tarea WHERE id_tablero = '{idTablero}';";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (Estado)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public List<Tarea> ListarDeUsuario(int idUsuario)
        {
            var queryString = $"SELECT * FROM tarea WHERE id_usuario_asignado = '{idUsuario}';";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (Estado)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        public List<Tarea> GetAll(){
            var queryString = $"SELECT * FROM tarea;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (Estado)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
        //Obtener detalles de una tarea por su ID
        public Tarea GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tarea = new Tarea();
            SQLiteCommand command = connection.CreateCommand();
         //   command.CommandText = $"SELECT * FROM Tablero WHERE id_usuario_propietario = '{idUsuario}';";
            command.CommandText = $"SELECT * FROM tarea WHERE id = '{id}';";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (Estado)Convert.ToInt32(reader["estado"]);
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();

            return (tarea);
        }

        public void AsignarUsuario (int idUsuario, int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE tarea SET id_usuario_asignado = '{idUsuario}' WHERE id = '{idTarea}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        //Eliminar una tarea por ID
        public void Remove(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM tarea WHERE id = '{id}';";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}