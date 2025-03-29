namespace ApiEliteWebAcceso.Application.DTOs.Menu
{
    public class MenuPadreDTO
    {

        public int Id { get; set; }
        public int AplicativoId { get; set; }
        public string? Url { get; set; }
        public int? ParentId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Icono { get; set; }
        public string Estado { get; set; } = "A";
        public string? InicialesAplicativo { get; set; }
        public string? NombreAplicativo { get; set; }
        public int Nivel { get; set; }
        public string? Jerarquia { get; set; }
        public string? ParentNombre { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
