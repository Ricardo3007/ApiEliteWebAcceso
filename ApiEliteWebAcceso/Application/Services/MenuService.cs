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


        public async Task<Result<List<MenuNodeDto>>> GetMenuPermiso(List<int> idEmpresas, int? idRol = null)
        {

            try
            {
                var menulist = await _menuRepository.GetMenuPermiso(idEmpresas, idRol);

                return Result<List<MenuNodeDto>>.Success(menulist);
            }
            catch (Exception ex)
            {
                return Result<List<MenuNodeDto>>.Failure(ex.Message);
            }

        }

        public async Task<Result<List<MenuPadreDTO>>> GetMenuPadre(int idAplicativo)
        {
            try
            {
                // Obtener la lista de opciones de menú desde el repositorio
                var menuPadre = await _menuRepository.GetMenuPadre(idAplicativo);

                // Verificar si hay datos
                if (menuPadre == null || !menuPadre.Any())
                {
                    return Result<List<MenuPadreDTO>>.Failure("No se encontraron menús para el aplicativo.");
                }

                // Convertir las entidades a DTOs
                var menuPadreDtoList = menuPadre.Select(menu => new MenuPadreDTO
                {
                    Id = menu.PK_Opcion_Menu_C,
                    AplicativoId = menu.FK_Aplicativo_C,
                    Url = menu.Url_C,
                    ParentId = menu.Parent_C,
                    Texto = menu.Text_C,
                    Descripcion = menu.Descripcion_C,
                    Icono = menu.Icono_C,
                    Estado = menu.Estado_C,
                    InicialesAplicativo = menu.Iniciales_Aplicativo_C,
                    NombreAplicativo = menu.Nombre_Aplicativo_C,
                    Nivel = menu.Nivel,
                    Jerarquia = menu.Jerarquia,
                    ParentNombre = menu.ParentName,
                    Tipo = menu.Tipo_C
                }).ToList();

                // Retornar la lista con éxito
                return Result<List<MenuPadreDTO>>.Success(menuPadreDtoList);
            }
            catch (Exception ex)
            {
                return Result<List<MenuPadreDTO>>.Failure($"Error al obtener el menú padre: {ex.Message}");
            }
        }

        public async Task<Result<List<MenuPrincipalDTO>>> GetMenu()
        {
            try
            {
                var menulist = await _menuRepository.GetMenu();

                return Result<List<MenuPrincipalDTO>>.Success(menulist);
            }
            catch (Exception ex)
            {
                return Result<List<MenuPrincipalDTO>>.Failure(ex.Message);
            }
        }

        public async Task<Result<MenuPrincipalDTO>> GetMenuID(int idMenu)
        {
            try
            {
                var menuId = await _menuRepository.GetMenuID(idMenu);

                return Result<MenuPrincipalDTO>.Success(menuId);
            }
            catch (Exception ex)
            {
                return Result<MenuPrincipalDTO>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> CreateMenu(MenuPrincipalDTO menu)
        {
            try
            {
                return Result<bool>.Success(await _menuRepository.CreateMenu(menu));
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> UpdateMenu(MenuPrincipalDTO menu)
        {
            try
            {
                return Result<bool>.Success(await _menuRepository.UpdateMenu(menu));
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteMenu(int idMenu)
        {
            try
            {
                return Result<bool>.Success(await _menuRepository.DeleteMenu(idMenu));
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<MenuNodeDto>>> GetMenuUsuarioEmpresa(int idEmpresa, int? idRol = null)
        {

            try
            {
                var menulist = await _menuRepository.GetMenuUsuarioEmpresa(idEmpresa, idRol);

                return Result<List<MenuNodeDto>>.Success(menulist);
            }
            catch (Exception ex)
            {
                return Result<List<MenuNodeDto>>.Failure(ex.Message);
            }

        }


        public async Task<Result<List<MenuNodeDto>>> GetMenuPermisoGrupoEmpresa(int idGrupoEmpresa, int? idRol = null)
        {

            try
            {
                var menulist = await _menuRepository.GetMenuPermisoGrupoEmpresa(idGrupoEmpresa, idRol);

                return Result<List<MenuNodeDto>>.Success(menulist);
            }
            catch (Exception ex)
            {
                return Result<List<MenuNodeDto>>.Failure(ex.Message);
            }

        }
    }
}
