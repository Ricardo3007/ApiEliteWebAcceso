
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
        public string? nombreTipoUsuarioDTO { get; set; }

        public string? emailDTO { get; set; }

        public string? passwordDTO { get; set; }

        public string? estadoDTO { get; set; }

        public List<EmpresaDto> Empresas { get; set; }
        public List<PermisoUsuarioDto>? Permisos { get; set; }

    }
    public class PermisoUsuarioDto
    {
        public int PkPermisoUsuarioC { get; set; } // PK_PERMISO_USUARIO_C
        public int FkUsuarioC { get; set; }       // FK_USUARIO_C
        public int FkOpcionMenuC { get; set; }    // FK_OPCION_MENU_C
        public string? DescripcionMenuC { get; set; } // MENU.DESCRIPCION_C
        public int FkEmpresaC { get; set; }       // FK_EMPRESA_C
        public string? NombreEmpresaC { get; set; } // EMP.NOMBRE_EMPRESA_C
        public int FkGrupoEmpresaC { get; set; }  // EMP.FK_GRUPO_EMPRESA_C
        public string? InicialesAplicativoC { get; set; } // APL.INICIALES_APLICATIVO_C
        public string? NombreAplicativoC { get; set; }    // APL.NOMBRE_APLICATIVO_C
        public int OrdenAplicativoC { get; set; }        // APL.ORDEN_C
        public int tienepermiso { get; set; }        // APL.ORDEN_C
        
    }

    public class UsuarioInsertDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El usuario no puede exceder los 50 caracteres")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio")]
        [StringLength(20, ErrorMessage = "El documento no puede exceder los 20 caracteres")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El tipo de usuario debe ser un número positivo")]
        public int TipoUsuario { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La contraseña debe tener entre 4 y 100 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [RegularExpression("^[AI]$", ErrorMessage = "El estado debe ser 'A' (Activo) o 'I' (Inactivo)")]
        public string Estado { get; set; } = "A";

        [Required(ErrorMessage = "La empresa es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de empresa debe ser un número positivo")]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de rol debe ser un número positivo")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "Debe asignar al menos un permiso")]
        [MinLength(1, ErrorMessage = "Debe asignar al menos un permiso")]
        public List<int> Permisos { get; set; } = new List<int>();

        // Método para hashear la contraseña
        public string HashPassword()
        {
            return BCrypt.Net.BCrypt.HashPassword(this.Password);
        }
    }

}