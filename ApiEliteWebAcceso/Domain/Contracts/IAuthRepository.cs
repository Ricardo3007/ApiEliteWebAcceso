
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IAuthRepository
    {
        Task<ACC_USUARIO> ValidarLogin(string usuario);
        Task<List<ACC_EMPRESA>> ObtenerEmpresasPorUsuario(int idUsuario);

    }
}
