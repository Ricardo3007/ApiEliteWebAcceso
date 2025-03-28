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

        public async Task<Result<List<MenuDto>>> GetMenuUsuario(int idUsuario,int idEmpresa)
        {
            try
            {
                var items = await _menuRepository.ObtenerMenuUsuarioRolEmpresa(idUsuario,idEmpresa);

                // Agrupar elementos por NombreAplicativo
                var groupedByApplication = items.GroupBy(item => item.Iniciales_Aplicativo_C);

                // Construir la jerarquía
                var rootItems = new List<MenuDto>();

                foreach (var group in groupedByApplication)
                {
                    // Crear el elemento raíz para cada NombreAplicativo
                    var rootItem = new MenuDto
                    {
                        Label = group.Key, // Nombre del aplicativo como raíz
                        Icon = "folder", // Ícono genérico para aplicativos
                        IsExpanded = false
                    };

                    // Crear un diccionario para elementos hijos
                    var allItems = group.ToDictionary(
                        item => (int)item.PK_Opcion_Menu_C,
                        item => new MenuDto
                        {
                            Label = item.Text_C,
                            Icon = item.Icono_C,
                            Url = item.Url_C,
                            Code = item.Iniciales_Aplicativo_C,
                            IsExpanded = false
                        });

                    // Vincular elementos hijos con sus padres
                    foreach (var item in group)
                    {
                        if (item.Parent_C != null)
                        {
                            var parentItem = allItems[(int)item.Parent_C];
                            parentItem.Children.Add(allItems[(int)item.PK_Opcion_Menu_C]);
                        }
                    }

                    // Añadir los elementos raíz (sin padres) al Children del aplicativo
                    rootItem.Children = group.Where(item => item.Parent_C == null)
                                             .Select(item => allItems[(int)item.PK_Opcion_Menu_C])
                                             .ToList();

                    rootItems.Add(rootItem);
                }


                return Result<List<MenuDto>>.Success(rootItems);
            }
            catch (Exception ex)
            {
                return Result<List<MenuDto>>.Failure(ex.Message);
            }
        }


        public async Task<Result<List<MenuNodeDto>>> GetMenuPermiso(int idGrupoEmpresa, int? idRol = null)
        {

            try
            {
                var menulist = await _menuRepository.GetMenuPermiso(idGrupoEmpresa, idRol);

                return Result<List<MenuNodeDto>>.Success(menulist);
            }
            catch (Exception ex)
            {
                return Result<List<MenuNodeDto>>.Failure(ex.Message);
            }

        }
    }
}
