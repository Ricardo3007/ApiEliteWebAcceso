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

        public async Task<Result<UsuarioConMenuDTO>> ObtenerMenuUsuarioRolEmpresa(int idUsuario,int idEmpresa)
        {
            try
            {


                List<Menu_Usuario> menu = await _menuRepository.ObtenerMenuUsuarioRolEmpresa(idUsuario,idEmpresa);

                var menuSinDuplicados = menu
                  .GroupBy(x => new { x.Menu, x.URL, x.Menu_Padre })  // Agrupar por las propiedades relevantes
                  .Select(g => g.First())                                      // Seleccionar solo el primer elemento de cada grupo
                  .ToList();

                // Agrupar por los campos 'usuarioDTO', 'rolDTO' y 'empresaDTO'
                var grupo = menu
                    .GroupBy(x => new { x.Usuario, x.Rol, x.Empresa })  // Agrupar solo por estos tres campos
                    .Select(g => new UsuarioConMenuDTO
                    {
                        UsuarioDTO = g.Key.Usuario,    // Asignar 'usuarioDTO'
                        RolDTO = g.Key.Rol,            // Asignar 'rolDTO'
                        EmpresaDTO = g.Key.Empresa,    // Asignar 'empresaDTO'
                        Menu = g.Select(x => new MenuItemDTO  // Mapear los menús dentro de este grupo
                        {
                            MenuDTO = x.Menu,
                            MenuPadreDTO = x.Menu_Padre,
                            UrlDTO = x.URL
                        }).ToList()
                    })
                    .FirstOrDefault();


                // Luego, puedes asignar 'menuList' a la propiedad 'menu' de 'UsuarioMenuDTO'
                UsuarioConMenuDTO usuarioMenuDTO = new UsuarioConMenuDTO
                {
                    UsuarioDTO = "Juan P rez",  // Aquí puedes poner los valores dinámicos
                    RolDTO = "Administrador",    // Aquí puedes poner los valores dinámicos
                    EmpresaDTO = "Empresa1 S.A.", // Aquí puedes poner los valores dinámicos
                    Menu = menuSinDuplicados.Select(x => new MenuItemDTO
                    {
                        MenuDTO = x.Menu,
                        MenuPadreDTO = x.Menu_Padre,
                        UrlDTO = x.URL
                    }).ToList()
                };



                return Result<UsuarioConMenuDTO>.Success(usuarioMenuDTO);
            }
            catch (Exception ex)
            {
                return Result<UsuarioConMenuDTO>.Failure(ex.Message);
            }
        }
    }
}
