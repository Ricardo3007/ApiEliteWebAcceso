using ApiEliteWebAcceso.Application.DTOs.Login;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IMenuService
    {
        Task<Result<List<MenuDto>>> GetMenuUsuario(int idUsuario, int idEmpresa);
        Task<Result<List<MenuNodeDto>>> GetMenuPermiso(int idGrupoEmpresa, int? idRol = null);
        Task<Result<List<MenuPadreDTO>>> GetMenuPadre(int idAplicativo);
        Task<Result<List<MenuPrincipalDTO>>> GetMenu();
        Task<Result<MenuPrincipalDTO>> GetMenuID(int idMenu);

        Task<Result<bool>> CreateMenu(MenuPrincipalDTO menu);
        Task<Result<bool>> UpdateMenu(MenuPrincipalDTO menu);
        Task<Result<bool>> DeleteMenu(int idMenu);

    }
}
