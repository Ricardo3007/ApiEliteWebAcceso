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

                if (String.IsNullOrEmpty(loginUsuario.Usuario))
                    return Result<UsuarioLoginDto>.BadRequest([ String.Format(Resources.ParameterRequired, nameof(loginUsuario.Usuario)) ]);

                if (String.IsNullOrEmpty(loginUsuario.Password))
                    return Result<UsuarioLoginDto>.BadRequest([ String.Format(Resources.ParameterRequired, nameof(loginUsuario.Password)) ]);

                ACC_USUARIO usuarioLogin = await _authRepository.ValidarLogin(loginUsuario.Usuario);
                var Password = BCryptNet.HashPassword(loginUsuario.Password);
                if (usuarioLogin == null || !BCryptNet.Verify(loginUsuario.Password, usuarioLogin.PASSWORD_C))
                {
                    return Result<UsuarioLoginDto>.BadRequest([Resources.DocumentoOrPasswordIncorrect]);
                }

                DateTime fechaActual = DateTime.Now;
                DateTime fechaFinToken = fechaActual.AddDays(1);
                DateTime fechaFinTokenRefresh = fechaActual.AddDays(15);

                List<ACC_EMPRESA> empresasUsuario = await _authRepository.ObtenerEmpresasPorUsuario(usuarioLogin.PK_USUARIO_C);

                var token = GenerateJSONWebToken(usuarioLogin, empresasUsuario, fechaFinToken, "token");
                var tokenRefresh = GenerateJSONWebToken(usuarioLogin, empresasUsuario, fechaFinTokenRefresh, "refresh");

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



        private string GenerateJSONWebToken(ACC_USUARIO usuario, List<ACC_EMPRESA> empresas, DateTime fechaFin, string tipo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.PK_USUARIO_C.ToString()),
                new Claim("type", tipo)
            };

            // Agregamos las credenciales de cada empresa como claims
            foreach (var empresa in empresas)
            {
                var credenciales = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                    $"{empresa.SERVIDOR_BD_C}|{empresa.NOMBRE_BD_C}|{empresa.USUARIO_BD_C}|{empresa.PASSWORD_BD_C}"
                ));

                claims.Add(new Claim($"empresa_{empresa.PK_EMPRESA_C}", credenciales));
            }

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: fechaFin,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UsuarioDto UsuarioToUsuarioDto(ACC_USUARIO usuario, List<ACC_EMPRESA> empresasUsuario)
        {
            return new UsuarioDto
            {
                idUsuarioDTO = usuario.PK_USUARIO_C,
                usuarioDTO = usuario.USUARIO_C,
                documentoDTO = usuario.ID_USUARIO_C,
                nombreDTO = usuario.NOMBRE_USUARIO_C,
                emailDTO = usuario.MAIL_USUARIO_C,
                tipoUsuarioDTO = usuario.TIPO_USUARIO_C,
                Empresas = empresasUsuario.Select(e => new EmpresaDto
                {
                    idEmpresaDTO = e.PK_EMPRESA_C,
                    nombreDTO = e.NOMBRE_EMPRESA_C,
                    logoDTO = e.LOGO_EMPRESA_C,
                    grupoEmpresaDTO = e.FK_GRUPO_EMPRESA_C
                }).ToList()
            };
        }



    }
}
