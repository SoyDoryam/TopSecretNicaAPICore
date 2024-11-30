using System.Collections.Generic;
using System.Threading.Tasks;
using TopSecretNicaAPICore.Models;

namespace TopSecretNicaAPICore.Repositories
{
    public interface ICategoriasRepository
    {
        Task<IEnumerable<Categorias>> Obtener();
        Task<Categorias> ObtenerPorId(int id);
        Task<int> Agregar(Categorias categoria);
        Task<int> Modificar(Categorias categoria);
        Task<int> Eliminar(int id);
    }
}
