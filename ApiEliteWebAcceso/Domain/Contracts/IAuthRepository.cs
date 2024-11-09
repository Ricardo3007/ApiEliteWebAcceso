
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IAuthRepository
    {
        Task<Usuarios> ValidarLogin(string documento);
        Task<List<Empresas>> ObtenerEmpresasPorUsuario(int idUsuario);

    }
}
