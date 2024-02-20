using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

using EspacioModels;

namespace kanbanRepository
{
    public class TableroRepository: ITableroRepository{
        private string cadenaConexion;

        public TableroRepository(string CadenaDeConexion)
        {
            cadenaConexion = CadenaDeConexion;
        }

        public void Create(Tablero tablero){
            var query = $"INSERT INTO tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idUsuProp, @nombre, @descripcion);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@idUsuProp", tablero.Id_usuario_propietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

                command.ExecuteNonQuery(); //para INSERT, DELETE, UPDATE
                connection.Close();
            }
        }
        public void Update(int id, Tablero tablero){
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE tablero SET id_usuario_propietario = '{tablero.Id_usuario_propietario}', nombre = '{tablero.Nombre}', descripcion = '{tablero.Descripcion}' WHERE id = '{id}';";
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public Tablero GetById(int id){
            var query = $"SELECT * FROM tablero WHERE id = '{id}';";
            Tablero tablero = new Tablero();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();

                    }
                }
                connection.Close();
            }
            if (tablero == null) {
                throw new Exception("Tablero no encontrado.");
            }
            return tablero;
        }
        public List<Tablero> GetAll(){
            var query = $"SELECT * FROM tablero;";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
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
        public List<Tablero> GetAllByUsu(int idUsu)
        {
            var query = $"SELECT * FROM tablero WHERE id_usuario_propietario = '{idUsu}';";
            List<Tablero> tableros = new List<Tablero>();

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                SQLiteCommand command = new SQLiteCommand(query, connection);

                connection.Open();

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read()){
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
            if (tableros == null) {
                throw new Exception("Usuario sin tableros.");
            }
            return tableros;
        }
        public void Delete(int id){
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM tablero WHERE id = '{id}';";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteByUsu(int idUsu){
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM tablero WHERE id_usuario_propietario = '{idUsu}';";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}