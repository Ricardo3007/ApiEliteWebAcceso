
using ApiEliteWebAcceso.Application.DTOs.Empresa;

namespace ApiEliteWebAcceso.Application.DTOs.Usuario
{
    public class UsuarioDto
    {

        public int idUsuarioDTO { get; set; }

        public string? usuarioDTO { get; set; }

        public string? documentoDTO { get; set; }

        public string? nombreDTO { get; set; }

        public string? emailDTO { get; set; }

        public string? passwordDTO { get; set; }

        public string? estadoDTO { get; set; }

        public List<EmpresaDto> Empresas { get; set; }

    }
}
