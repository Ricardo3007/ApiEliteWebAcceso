
using ApiEliteWebAcceso.Application.DTOs.Usuario;

namespace ApiEliteWebAcceso.Application.DTOs.Login
{
    public class UsuarioLoginDto
    {
        public UsuarioDto Usuario { get; set; }
        public string Token { get; set; }
        public string TokenRefresh { get; set; }
    }
}
