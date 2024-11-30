using TopSecretNicaAPICore.Models;

namespace TopSecretNicaAPICore.Repository.Interfaces
{
    public interface IUsuariosRepository
    {
        List<Usuarios> ObtenerUsuarios();
        Usuarios ObtenerUsuarioPorId(int id);
        bool GuardarUsuario(Usuarios usuario);
        bool EditarUsuario(Usuarios usuario);
        bool EliminarUsuario(int usuarioId);
    }
}
    