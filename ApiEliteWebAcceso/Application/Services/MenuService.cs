using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Menu;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IConfiguration _config;
        private readonly IMenuRepository _menuRepository;

        public MenuService(IConfiguration config, IMenuRepository menuRepository)

        {
            _config = config;
            _menuRepository = menuRepository;
        }

        public async Task<Result<List<MenuDto>>> getMenuUsuario(int idUsuario,int idEmpresa)
        {
            try
            {
                var items = await _menuRepository.ObtenerMenuUsuarioRolEmpresa(idUsuario,idEmpresa);

                // Crear un diccionario para facilitar el acceso por ID
                var lookup = items.ToLookup(item => (int?)item.Parent_C); // Agrupar por Parent
                var allItems = items.ToDictionary(item => (int)item.PK_Opcion_Menu_C, item => new MenuDto
                {
                    Label = item.Text_C,
                    Icon = item.Icono_C,
                    Url = item.Url_C,
                    IsExpanded = false
                });

                // Construir la jerarquía
                foreach (var item in items)
                {
                    if (item.Parent_C != null)
                    {
                        var parentItem = allItems[(int)item.Parent_C];
                        parentItem.Children.Add(allItems[(int)item.PK_Opcion_Menu_C]);
                    }
                }

                return Result<List<MenuDto>>.Success(items.Where(item => item.Parent_C == null)
                            .Select(item => allItems[(int)item.PK_Opcion_Menu_C])
                            .ToList());
            }
            catch (Exception ex)
            {
                return Result<List<MenuDto>>.Failure(ex.Message);
            }
        }
    }
}
