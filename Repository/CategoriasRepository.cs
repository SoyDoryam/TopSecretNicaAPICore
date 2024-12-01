using Dapper;
using System.Data;
using System.Data.SqlClient;
using TopSecretNicaAPICore.Models;
using TopSecretNicaAPICore.Repositories;

namespace TopSecretNicaAPICore.Repository
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly string _connectionString;
        public CategoriasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<int> Agregar(Categorias categoria)
        {
 using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    "sp_guardar_categoria", 
                    new 
                    {
                        Nombre = categoria.Nombre,
                        Descripcion = categoria.Descripcion,
                        Estado = categoria.Estado
                    }, 
                    commandType: CommandType.StoredProcedure
                );
                return result;
            }        }

        public async Task<int> Eliminar(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    "sp_eliminar_categoria",
                    new { CategoriaID = id },
                    commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<int> Modificar(Categorias categoria)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    "sp_editar_categoria",
                    new Categorias
                    {
                        CategoriaID = categoria.CategoriaID,
                        Nombre = categoria.Nombre,
                        Descripcion = categoria.Descripcion,
                        Estado = categoria.Estado
                    },
                    commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<Categorias>> Obtener()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync    <Categorias>(
                    "sp_lista_categorias",
                    commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<Categorias> ObtenerPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<Categorias>(
                    "sp_lista_categorias",
                    new { CategoriaID = id },
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }
    }
}
