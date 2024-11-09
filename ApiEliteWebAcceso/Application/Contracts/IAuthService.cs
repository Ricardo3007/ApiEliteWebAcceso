using ApiEliteWebAcceso.Application.DTOs.Login;
using ApiEliteWebAcceso.Application.Response;

namespace ApiEliteWebAcceso.Application.Contracts
{
    public interface IAuthService
    {
        Task<Result<UsuarioLoginDto>> ValidarLogin(LoginUsuarioDto loginUsuario);

    }
}
