using ApiEliteWebAcceso.Application.Contracts;
using ApiEliteWebAcceso.Application.DTOs.Aplicacion;
using ApiEliteWebAcceso.Application.DTOs.Empresa;
using ApiEliteWebAcceso.Application.DTOs.Usuario;
using ApiEliteWebAcceso.Application.Response;
using ApiEliteWebAcceso.Domain.Contracts;
using ApiEliteWebAcceso.Infrastructure.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ApiEliteWebAcceso.Application.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IConfiguration _config;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IConfiguration config, IUsuarioRepository usuarioRepository)
        {
            _config = config;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Result<int>> CreateUsuario(UsuarioInsertDto usuarioDto)
        {
            try
            {
                // Validaciones de datos obligatorios
                if (string.IsNullOrWhiteSpace(usuarioDto.UsuarioDTO)) throw new ArgumentException("El usuario es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.DocumentoDTO)) throw new ArgumentException("El documento es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.NombreDTO)) throw new ArgumentException("El nombre es obligatorio");
                if (usuarioDto.TipoUsuarioDTO == null) throw new ArgumentException("El tipo de usuario es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.EmailDTO)) throw new ArgumentException("El email es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.PasswordDTO)) throw new ArgumentException("La contraseña es obligatoria");
                if (string.IsNullOrWhiteSpace(usuarioDto.EstadoDTO)) throw new ArgumentException("El estado es obligatorio");

                usuarioDto.PasswordDTO = BCryptNet.HashPassword(usuarioDto.PasswordDTO);

                return Result<int>.Success(await _usuarioRepository.CreateUsuario(usuarioDto));
            }
            catch (Exception ex)
            {
                return Result<int>.Failure(ex.Message);
            }           

        }

        public async Task<Result<bool>> DeleteUsuario(int idUsuario)
        {
            // Podés validar si el usuario existe antes de eliminar
            // var existe = await _usuarioRepository.Exists(idUsuario);
            // if (!existe) throw new Exception("Usuario no encontrado.");

            if (idUsuario <= 0)
                throw new ArgumentException("ID de usuario no válido");

            var existe = await _usuarioRepository.DeleteUsuario(idUsuario);

            if (!existe) throw new Exception("Usuario no encontrado.");

            return Result<bool>.Success(existe);
        }

        public async Task<Result<List<UsuarioEmpresaPermisoDto>>> GetPermisoUsuarioEmpresaID(int idGrupoEmpresa, bool isSuperAdmin)
        {
            try
            {
                // Obtener la lista de empresas desde el repositorio
                var permisoUsuarios = await _usuarioRepository.GetPermisoUsuarioEmpresaID(idGrupoEmpresa, isSuperAdmin);

                var permisoUsuariosDTO = permisoUsuarios.Select(e => new UsuarioEmpresaPermisoDto
                {
                    EmpresaIdDTO = e.PK_EMPRESA_C,
                    NombreEmpresaDTO = e.NOMBRE_EMPRESA_C,
                    GrupoEmpresaIdDTO = e.PK_GRUPO_EMPRESA_C,
                    NombreGrupoDTO = e.NOMBRE_GRUPO_C,
                    TienePermisoDTO = e.TIENE_PERMISO,
                    TieneMenuDTO = e.TIENE_MENU
                }).ToList();

                return Result<List<UsuarioEmpresaPermisoDto>>.Success(permisoUsuariosDTO);
            }
            catch (Exception ex)
            {
                return Result<List<UsuarioEmpresaPermisoDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<UsuarioDto>>> GetUsuario()
        {
            try
            {
                // Obtener la lista de empresas desde el repositorio
                var usuarios = await _usuarioRepository.GetUsuario();

                // Mapear la lista de ACC_EMPRESA a EmpresaDto
                var usuariosDto = usuarios.Select(e => new UsuarioDto
                {
                    idUsuarioDTO = e.PK_USUARIO_C,
                    nombreDTO = e.NOMBRE_USUARIO_C,
                    documentoDTO = e.ID_USUARIO_C,
                    usuarioDTO = e.USUARIO_C,
                    emailDTO = e.MAIL_USUARIO_C,
                    passwordDTO = e.PASSWORD_C,
                    tipoUsuarioDTO = e.TIPO_USUARIO_C,
                }).ToList();

                return Result<List<UsuarioDto>>.Success(usuariosDto);
            }
            catch (Exception ex)
            {
                return Result<List<UsuarioDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<UsuarioDto>> GetUsuarioID(int idUsuario, int idGrupoEmpresa)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuarioID(idUsuario);

                if (usuario == null)
                {
                    return Result<UsuarioDto>.Failure("Usuario no encontrado.");
                }

                /* var permisoUsuario = await _usuarioRepository.GetPermisoUsuarioID(idUsuario, idGrupoEmpresa);
                 var permisosDto = permisoUsuario
                 .Select(p => new PermisoUsuarioDto
                 {
                     PkPermisoUsuarioC = p.PK_PERMISO_USUARIO_C,
                     FkUsuarioC = p.FK_USUARIO_C,
                     FkOpcionMenuC = p.FK_OPCION_MENU_C,
                     DescripcionMenuC = p.DESCRIPCION_C,
                     FkEmpresaC = p.FK_EMPRESA_C,
                     NombreEmpresaC = p.NOMBRE_EMPRESA_C,
                     FkGrupoEmpresaC = p.FK_GRUPO_EMPRESA_C,
                     InicialesAplicativoC = p.INICIALES_APLICATIVO_C,
                     NombreAplicativoC = p.NOMBRE_APLICATIVO_C,
                     OrdenAplicativoC = p.ORDEN_C,
                     tienepermiso = p.PERMISO_C

                 })
                 .ToList();
                */

                /*
                 var empresaUsu = await _usuarioRepository.GetPermisoUsuarioEmpresaID(idGrupoEmpresa, false);

                 var permisoEmpresaDTO = empresaUsu.Select(e => new UsuarioEmpresaPermisoDto
                 {
                     EmpresaIdDTO = e.PK_EMPRESA_C,
                     NombreEmpresaDTO = e.NOMBRE_EMPRESA_C,
                     GrupoEmpresaIdDTO = e.PK_GRUPO_EMPRESA_C,
                     NombreGrupoDTO = e.NOMBRE_GRUPO_C,
                     TienePermisoDTO = e.TIENE_PERMISO
                 }).ToList();
                 */
                var resultUsuario = new UsuarioDto
                {
                    idUsuarioDTO = usuario.PK_USUARIO_C,
                    nombreDTO = usuario.NOMBRE_USUARIO_C,
                    documentoDTO = usuario.ID_USUARIO_C,
                    usuarioDTO = usuario.USUARIO_C,
                    emailDTO = usuario.MAIL_USUARIO_C,
                    passwordDTO = usuario.PASSWORD_C,
                    tipoUsuarioDTO = usuario.TIPO_USUARIO_C,
                    nombreTipoUsuarioDTO =  usuario.TIPO_USUARIO_C == 1 ? "Superadministrador" : usuario.TIPO_USUARIO_C == 2 ? "Administrador" : "Estandar",
                    estadoDTO = usuario.ESTADO_C,
                    //Permisos = permisosDto
                };

                return Result<UsuarioDto>.Success(resultUsuario);
            }
            catch (Exception ex)
            {
                return Result<UsuarioDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> UpdateUsuario(UsuarioInsertDto usuarioDto)
        {
            try
            {
                // Validaciones de datos obligatorios
                if (string.IsNullOrWhiteSpace(usuarioDto.UsuarioDTO)) throw new ArgumentException("El usuario es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.DocumentoDTO)) throw new ArgumentException("El documento es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.NombreDTO)) throw new ArgumentException("El nombre es obligatorio");
                if (usuarioDto.TipoUsuarioDTO == null) throw new ArgumentException("El tipo de usuario es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.EmailDTO)) throw new ArgumentException("El email es obligatorio");
                if (string.IsNullOrWhiteSpace(usuarioDto.PasswordDTO)) throw new ArgumentException("La contraseña es obligatoria");
                if (string.IsNullOrWhiteSpace(usuarioDto.EstadoDTO)) throw new ArgumentException("El estado es obligatorio");

                usuarioDto.PasswordDTO = BCryptNet.HashPassword(usuarioDto.PasswordDTO);

                return Result<bool>.Success(await _usuarioRepository.UpdateUsuario(usuarioDto));
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> InsertPermisoEmpresa(PermisoEmpresaInsertDTO dto)
        {
            try
            {
                var result = await _usuarioRepository.InsertPermisoEmpresa(dto);
                return Result<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al insertar permisos por empresa: {ex.Message}");
            }
        }

        public async Task<Result<bool>> UpdatePermisoEmpresa(PermisoEmpresaInsertDTO dto)
        {
            try
            {
                var result = await _usuarioRepository.UpdatePermisoEmpresa(dto);
                return Result<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al actualizar permisos por empresa: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeletePermisoEmpresa(int idUsuario, int idEmpresa)
        {
            try
            {
                var result = await _usuarioRepository.DeletePermisoEmpresa(idUsuario, idEmpresa);
                return Result<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar permisos por empresa: {ex.Message}");
            }
        }


    }
}
