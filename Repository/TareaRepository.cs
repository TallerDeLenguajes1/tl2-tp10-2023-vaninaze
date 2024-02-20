using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

using EspacioModels;

namespace kanbanRepository
{
    public class TareaRepository: ITareaRepository{
        private string cadenaConexion;

        public TareaRepository(string CadenaDeConexion)
        {
            cadenaConexion = CadenaDeConexion;
        }

        public void Create(Tarea tarea){
            var query = $"INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@idTablero, @nombre, @estado, @descripcion, @color, @idUsu);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.Id_tablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", (int)tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idUsu", tarea.Id_usuario_asignado));

                command.ExecuteNonQuery(); //para INSERT, DELETE, UPDATE
                connection.Close();
            }
        }
        public void Update(int id, Tarea tarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE tarea SET id_tablero = '{tarea.Id_tablero}', nombre = '{tarea.Nombre}', estado = '{(int)tarea.Estado}', descripcion = '{tarea.Descripcion}', color = '{tarea.Color}', id_usuario_asignado = '{tarea.Id_usuario_asignado}' WHERE id = '{id}';";
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<Tarea> GetAll()
        {
            var query = $"SELECT * FROM tarea;";
            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if (tareas == null) {
                throw new Exception("No se encontraron tareas.");
            }
            return tareas;
        }
        public Tarea GetById(int id)
        {
            var query = $"SELECT * FROM tarea WHERE id = '{id}';";
            Tarea tarea = new Tarea();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }
                connection.Close();
            }
            if (tarea == null) {
                throw new Exception("Tarea no encontrada.");
            }
            return tarea;
        }
        public List<Tarea> GetByUsuAsignado(int idUsu){
            var query = $"SELECT * FROM tarea WHERE id_usuario_asignado = '{idUsu}';";
            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if (tareas == null) {
                throw new Exception("Usuario sin tareas asignadas.");
            }
            return tareas;
        }
        public List<Tarea> GetByTablero(int idTablero)
        {
            var query = $"SELECT * FROM tarea WHERE id_tablero = '{idTablero}';";
            List<Tarea> tareas = new List<Tarea>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Color = reader["color"].ToString();
                        tarea.Id_usuario_asignado = Convert.ToInt32(reader["id_usuario_asignado"]);

                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            if (tareas == null) {
                throw new Exception("Tablero sin tareas.");
            }
            return tareas;
        }
        public void Delete(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM tarea WHERE id = '{id}';";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void AsignarTarea(int idUsu, int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE tarea SET id_usuario_asignado = '{idUsu}' WHERE id = '{idTarea}';";
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteByTablero(int idTab){
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM tarea WHERE id_tablero = '{idTab}';";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteByUsu(int idUsu){
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM tarea WHERE id_usuario_asignado = '{idUsu}';";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        
    }
}