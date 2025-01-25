using ApiEliteWebAcceso.Application.DTOs.Login;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IMenuService
    {
        Task<Result<List<MenuDto>>> GetMenuUsuario(int idUsuario, int idEmpresa);
    }
}
