namespace ApiEliteWebAcceso.Application.DTOs.Usuario
{
    public class UsuarioEmpresaPermisoDto
    {
        public int EmpresaIdDTO { get; set; }
        public int GrupoEmpresaIdDTO { get; set; }
        public string? NombreEmpresaDTO { get; set; }
        public string? NombreGrupoDTO { get; set; }
        public int TienePermisoDTO { get; set; }
    }
}
