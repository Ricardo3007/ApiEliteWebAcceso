namespace ApiEliteWebAcceso.Application.DTOs.Menu
{
    public class MenuPrincipalDTO
    {
        public int IdDTO { get; set; }
        public int AplicativoIdDTO { get; set; }
        public string? UrlDTO { get; set; }
        public int? ParentIdDTO { get; set; }
        public string? NombreParentDTO { get; set; }
        public string TextoDTO { get; set; } = string.Empty;
        public string? DescripcionDTO { get; set; }
        public string? IconoDTO { get; set; }
        public string EstadoDTO { get; set; } = "A";
        public string? InicialesAplicativoDTO { get; set; }
        public string? NombreAplicativoDTO { get; set; }
        public int NivelDTO { get; set; }
        public string? JerarquiaDTO { get; set; }
        public string? ParentNombreDTO { get; set; }
        public string TipoDTO { get; set; } = string.Empty;
    }
}
