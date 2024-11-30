using System.Data.SqlClient;
using System.Data;
using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repository.Interfaces;

namespace TopSecretNicaAPICore.Repository
{
    public class MarcasRepository : IMarcasRepository
    {
        private readonly string _connectionString;

        public MarcasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }
        public bool Desactivar(int id)
        {
            bool filasAfectadas = false;

            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_eliminar_marca", conexion))
                {
                    cmd.Parameters.AddWithValue("marcaID", id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    filasAfectadas =cmd.ExecuteNonQuery() > 0;

                }
                conexion.Close();
            }
            return filasAfectadas;
        }

        public bool Editar(Marcas marcas)
        {
            bool filasAfectadas = false;
            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_editar_marca", conexion))
                {
                    cmd.Parameters.AddWithValue("marcaID", marcas.MarcaID);
                    cmd.Parameters.AddWithValue("nombre", marcas.Nombre);
                    cmd.Parameters.AddWithValue("descripcion", marcas.Descripcion);
                    cmd.Parameters.AddWithValue("estado", marcas.Estado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    filasAfectadas = cmd.ExecuteNonQuery() > 0;

                }
                conexion.Close();
            }
            return filasAfectadas;
        }

        public bool Guardar(Marcas marcas)
        {
            bool filasAfectadas = false;
            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_guardar_marca", conexion))
                {
                    cmd.Parameters.AddWithValue("nombre", marcas.Nombre);
                    cmd.Parameters.AddWithValue("descripcion", marcas.Descripcion);
                    cmd.Parameters.AddWithValue("estado", marcas.Estado);

                    cmd.CommandType = CommandType.StoredProcedure;
                    filasAfectadas = cmd.ExecuteNonQuery() > 0;

                }
                conexion.Close();
            }
            return filasAfectadas;
        }

        public Marcas ObtenerMarcarPorId(int id)
        {
            List<Marcas> marcas = new List<Marcas>();
            Marcas marca = new Marcas();
            using (var conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_lista_marcas", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            marcas.Add(new Marcas
                            {
                                MarcaID = Convert.ToInt32(reader["MarcaID"]),
                                Codigo = reader["Codigo"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(value: reader["Estado"]),
                            });
                        }
                    }
                }
                conexion.Close();
            }
            marca = marcas.Where(item => item.MarcaID == id).FirstOrDefault();
            return marca;
        }

        public List<Marcas> ObtenerMarcas()
        {
            List<Marcas> marcas = new List<Marcas>();
                using (var conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_lista_marcas", conexion))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                marcas.Add(new Marcas
                                {
                                    MarcaID = Convert.ToInt32(reader["MarcaID"]),
                                    Codigo = reader["Codigo"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                });
                            }
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    conexion.Close();
                }
            return marcas;
        }
    }
}
