using ApiEliteWebAcceso.Domain.Entities.Acceso;

namespace ApiEliteWebAcceso.Application.DTOs.Menu
{
    public class MenuDto
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
        public bool? IsExpanded { get; set; }
        public string Tipo { get; set; }
        public List<MenuDto> Children { get; set; } = new List<MenuDto>();
    }
}
