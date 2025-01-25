using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IMenuRepository
    {
        Task<List<Menu_Usuario>> ObtenerMenuUsuarioRolEmpresa(int idUsuario, int idEmpresa);
    }
}
