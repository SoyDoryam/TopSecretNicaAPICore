using TopSecretNicaAPICore.Models;

namespace TopSecretNicaAPICore.Repository.Interfaces
{
    public interface IRolesRepository
    {
        List<Roles> ObtenerRoles();
        Roles ObtenerRolPorId(int idRol);
        bool GuardarRol(Roles rol);
        bool EditarRol(Roles rol);
        bool EliminarRol(int rolId);
    }
}
