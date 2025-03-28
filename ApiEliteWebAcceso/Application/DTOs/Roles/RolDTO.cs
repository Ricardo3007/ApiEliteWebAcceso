namespace ApiEliteWebAcceso.Application.DTOs.Roles
{
    public class RolDTO
    {
        public int? IdRolDTO { get; set; }
        public string NombreRolDTO { get; set; } = string.Empty;
        public int GrupoEmpresaIdDTO { get; set; }
        public string EstadoDTO { get; set; } = "A";
        public List<int> Permisos { get; set; } = new List<int>();
    }
}
