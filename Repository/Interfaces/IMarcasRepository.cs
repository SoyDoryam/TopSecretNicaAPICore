using System.Collections.Generic;
using TopSecretNicaAPICore.Models;

namespace TopSecretNicaAPICore.Repository.Interfaces
{
    public interface IMarcasRepository
    {
        List<Marcas> ObtenerMarcas();
        Marcas ObtenerMarcarPorId(int id);
        public bool Guardar(Marcas marcas);
        public bool Editar(Marcas marcas);
        public bool Desactivar(int id);
    }
}
