using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.DTOs.Menu
{
    public class MenuDto
    {
        public string UsuarioDTO { get; set; }
        public string RolDTO { get; set; }
        public string EmpresaDTO { get; set; }
        public string MenuDTO { get; set; }
        public string? Menu_PadreDTO { get; set; }
        public string? URLDTO { get; set; }
    }

    public class MenuItemDTO
    {
        public string MenuDTO { get; set; }
        public string MenuPadreDTO { get; set; }
        public string UrlDTO { get; set; }
    }

    public class UsuarioConMenuDTO
    {
        public string UsuarioDTO { get; set; }
        public string RolDTO { get; set; }
        public string EmpresaDTO { get; set; }
        public List<MenuItemDTO> Menu { get; set; }
    }
}
