
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using System.ComponentModel.DataAnnotations;

namespace ApiEliteWebAcceso.Application.DTOs.Usuario
{
    public class UsuarioDto
    {

        public int idUsuarioDTO { get; set; }

        public string? usuarioDTO { get; set; }

        public string? documentoDTO { get; set; }

        public string? nombreDTO { get; set; }

        public int? tipoUsuarioDTO { get; set; }
        public int? TdiDTO { get; set; }
        public string? nombreTipoUsuarioDTO { get; set; }

        public string? emailDTO { get; set; }

        public string? passwordDTO { get; set; }

        public string? estadoDTO { get; set; }
        public List<EmpresaDto> Empresas { get; set; }
    }

    public class UsuarioInsertDto
    {
        public int IdUsuarioDTO { get; set; }
        public string UsuarioDTO { get; set; }
        public string DocumentoDTO { get; set; }
        public string NombreDTO { get; set; }
        public int TipoUsuarioDTO { get; set; }
        public string EmailDTO { get; set; }
        public string PasswordDTO { get; set; }
        public string EstadoDTO { get; set; }
    }

    public class PermisoEmpresaInsertDTO
    {
        public int IdUsuarioDTO { get; set; }
        public List<PermisoEmpresaUsuarioDTO> ?PermisosPorEmpresa { get; set; }
    }

    public class PermisoEmpresaUsuarioDTO
    {
        public int IdEmpresaDTO { get; set; }
        public int ?IdRolDTO { get; set; }
        public List<int> ?Permisos { get; set; }
    }
}