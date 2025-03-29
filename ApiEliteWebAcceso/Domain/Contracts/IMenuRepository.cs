using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using System.Threading.Tasks;

namespace ApiEliteWebAcceso.Domain.Contracts
{
    public interface IMenuRepository
    {
        Task<List<MenuOption>> ObtenerMenuUsuarioRolEmpresa(int idUsuario, int idEmpresa);
        Task<List<MenuNodeDto>> GetMenuPermiso(int idGrupoEmpresa, int? idRol = null);
        Task<List<MenuOption>> GetMenuPadre(int idAplicativo);
        Task<List<MenuPrincipalDTO>> GetMenu();
        Task<MenuPrincipalDTO> GetMenuID(int idMenu);

        Task<bool> CreateMenu(MenuPrincipalDTO menu);
        Task<bool> UpdateMenu(MenuPrincipalDTO menu);
        Task<bool> DeleteMenu(int idMenu);
    }
}
