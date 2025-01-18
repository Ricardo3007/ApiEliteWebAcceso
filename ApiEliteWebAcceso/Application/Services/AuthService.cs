using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Login;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Application.Resource;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Domain.Entities.Acceso;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

                DateTime fechaActual = DateTime.Now;
                DateTime fechaFinToken = fechaActual.AddDays(1);
                DateTime fechaFinTokenRefresh = fechaActual.AddDays(15);

                //SesionUsuario sesionUsuario = new()
                //{
                //    UsuarioId = usuarioLogin.Id,
                //    Ip = loginUsuario.Ip,
                //    Dispositivo = loginUsuario.Dispositivo,
                //    FechaInicio = fechaActual,
                //    FechaFinToken = fechaFinToken,
                //    FechaFinTokenRefresh = fechaFinTokenRefresh
                //};

                //SERVICIO.RegistrarSessionUsuario(sesionUsuario);
                //SERVICIO.UpdateLoginUserLastSesion(usuarioLogin.Id);

                List<Empresas> empresasUsuario = await _authRepository.ObtenerEmpresasPorUsuario(usuarioLogin.pk_usuario_c);

                var token = GenerateJSONWebToken(usuarioLogin, fechaFinToken, "token");
                var tokenRefresh = GenerateJSONWebToken(usuarioLogin, fechaFinTokenRefresh, "refresh");

                UsuarioLoginDto usuarioTokenResult = new()
                {
                    Usuario = UsuarioToUsuarioDto(usuarioLogin, empresasUsuario),
                    Token = token,
                    TokenRefresh = tokenRefresh
                };

                //var emptyDto = new UsuarioLoginDto();
                return Result<UsuarioLoginDto>.Success(usuarioTokenResult);
            }
            catch (Exception ex)
            {
                return Result<UsuarioLoginDto>.Failure(ex.Message);
            }
        }



        private string GenerateJSONWebToken(Usuarios usuario, DateTime fechaFin, string tipo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Cualquier parametro nuevo añadirlo abajo
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.pk_usuario_c.ToString()),
                //new Claim(ClaimTypes.Role, usuario.Usuario_Rol),
                new Claim("type", tipo),
                //new Claim("perfilCodigo", usuario.Perfil.Codigo),
                //new Claim("tipoUsuarioId", usuario.TipoUsuarioId.ToString())

                // Nos sirve para validar si es admin [Authorize(Roles="1")]
                // O varios tipos asi [Authorize(Roles="1,2,3")]
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: fechaFin,
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
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
