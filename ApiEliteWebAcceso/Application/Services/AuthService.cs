using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Login;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Application.Resource;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;


namespace ApiEliteWebAcceso.Application.Services
{
    public class AuthService: IAuthService
    {
        private readonly IConfiguration _config;
        //private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;

        public AuthService(IConfiguration config, IAuthRepository authRepository)
        {
            _config = config;
            _authRepository = authRepository;
            //_mapper = mapper;
        }
              
        public async Task<Result<UsuarioLoginDto>> ValidarLogin(LoginUsuarioDto loginUsuario)
        {
            try
            {

                if (String.IsNullOrEmpty(loginUsuario.Documento))
                    return Result<UsuarioLoginDto>.BadRequest([ String.Format(Resources.ParameterRequired, nameof(loginUsuario.Documento)) ]);

                if (String.IsNullOrEmpty(loginUsuario.Password))
                    return Result<UsuarioLoginDto>.BadRequest([ String.Format(Resources.ParameterRequired, nameof(loginUsuario.Password)) ]);

                Usuarios usuarioLogin = await _authRepository.ValidarLogin(loginUsuario.Documento);
                var Password = BCryptNet.HashPassword(loginUsuario.Password);
                if (usuarioLogin == null || !BCryptNet.Verify(loginUsuario.Password, usuarioLogin.password_c))
                {
                    return Result<UsuarioLoginDto>.BadRequest([Resources.DocumentoOrPasswordIncorrect]);
                }

                List<Empresas> empresasUsuario = await _authRepository.ObtenerEmpresasPorUsuario(usuarioLogin.pk_usuario_c);

                UsuarioLoginDto usuarioTokenResult = new()
                {
                    Usuario = UsuarioToUsuarioDto(usuarioLogin, empresasUsuario)
                };

                //var emptyDto = new UsuarioLoginDto();
                return Result<UsuarioLoginDto>.Success(usuarioTokenResult);
            }
            catch (Exception ex)
            {
                return Result<UsuarioLoginDto>.Failure(ex.Message);
            }
        }

        private UsuarioDto UsuarioToUsuarioDto(Usuarios usuario, List<Empresas> empresasUsuario)
        {
            return new UsuarioDto
            {
                idUsuarioDTO = usuario.pk_usuario_c,
                documentoDTO = usuario.documento_c,
                nombreDTO = usuario.nombre_c,
                emailDTO = usuario.email_c,
                terceroDTO = usuario.fk_tercero_c,
                Empresas = empresasUsuario.Select(e => new EmpresaDto
                {
                    idEmpresaDTO = e.pk_empresa_c,
                    nombreDTO = e.nombre_c,
                    logoDTO = e.logo_c,
                    cadenaConexionDTO = e.cadena_conexion_c
                }).ToList()
            };
        }



    }
}
