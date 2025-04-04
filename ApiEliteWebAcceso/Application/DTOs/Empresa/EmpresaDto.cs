using System.ComponentModel.DataAnnotations;

namespace ApiEliteWebAcceso.Application.DTOs.Empresa
{

    public class EmpresaDto
    {
        // Campos del DTO
        public int? idEmpresaDTO { get; set; } // ID de la empresa (opcional)
        public string? nombreDTO { get; set; } // Nombre de la empresa (opcional)
        public int? grupoEmpresaDTO { get; set; } // FK al grupo de la empresa (opcional)
        public string? idEmpresaC_DTO { get; set; } // ID de la empresa (opcional, char(20))
        public string? logoDTO { get; set; } // Logo de la empresa (opcional, char(100))
        public string? nombreBdDTO { get; set; } // Nombre de la base de datos (opcional, char(60))
        public string? servidorBdDTO { get; set; } // Servidor de la base de datos (opcional, char(50))
        public string? usuarioBdDTO { get; set; } // Usuario de la base de datos (opcional, char(50))
        public string? passwordBdDTO { get; set; } // Contraseña de la base de datos (opcional, char(50))
        public string? estadoDTO { get; set; } // Estado de la empresa (opcional, char(1))
        public string? cadenaConexionDTO { get; set; }
        public List<PermisoEmpresaAplicativoDTO> ?aplicativos { get; set; }
    }

    public class EmpresaPorGrupoDto
    {
        // Campos del DTO
        public int? idEmpresaDTO { get; set; } // ID de la empresa (opcional)
        public string? nombreDTO { get; set; } // Nombre de la empresa (opcional)
        public int? idGrupoEmpresaDTO { get; set; } // FK al grupo de la empresa (opcional)
        public string? nombreGrupoEmpresaDTO { get; set; } // Nombre de la empresa (opcional)
    }

    public class EmpresaInsertDto
    {
        // Información básica de la empresa
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El grupo de empresa es obligatorio")]
        public int GrupoEmpresaId { get; set; }

        [StringLength(20, ErrorMessage = "El ID de empresa no puede exceder los 20 caracteres")]
        public string CodigoEmpresa { get; set; }

        [StringLength(200, ErrorMessage = "El logo no puede exceder los 200 caracteres")]
        public string Logo { get; set; }

        // Información de conexión a BD
        [Required(ErrorMessage = "El nombre de la base de datos es obligatorio")]
        [StringLength(60, ErrorMessage = "El nombre de BD no puede exceder los 60 caracteres")]
        public string NombreBaseDatos { get; set; }

        [Required(ErrorMessage = "El servidor de base de datos es obligatorio")]
        [StringLength(50, ErrorMessage = "El servidor no puede exceder los 50 caracteres")]
        public string ServidorBaseDatos { get; set; }

        [Required(ErrorMessage = "El usuario de base de datos es obligatorio")]
        [StringLength(50, ErrorMessage = "El usuario no puede exceder los 50 caracteres")]
        public string UsuarioBaseDatos { get; set; }

        [Required(ErrorMessage = "La contraseña de base de datos es obligatoria")]
        [StringLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres")]
        public string PasswordBaseDatos { get; set; }

        // Estado por defecto
        public string Estado { get; set; } = "A";

        // Lista de aplicativos asociados
        //[Required(ErrorMessage = "Debe especificar al menos un aplicativo")]
        //[MinLength(1, ErrorMessage = "Debe especificar al menos un aplicativo")]
        public List<int> AplicativosIds { get; set; } = new List<int>();

    }

    public class EmpresaUpdateDto
    {
        [Required(ErrorMessage = "El ID de la empresa es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la empresa es obligatorio")]
        [StringLength(200, ErrorMessage = "El nombre no puede exceder los 200 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El grupo de empresa es obligatorio")]
        public int GrupoEmpresaId { get; set; }

        [StringLength(20, ErrorMessage = "El ID de empresa no puede exceder los 20 caracteres")]
        public string CodigoEmpresa { get; set; }

        [StringLength(200, ErrorMessage = "El logo no puede exceder los 200 caracteres")]
        public string Logo { get; set; }

        [Required(ErrorMessage = "El nombre de la base de datos es obligatorio")]
        [StringLength(60, ErrorMessage = "El nombre de BD no puede exceder los 60 caracteres")]
        public string NombreBaseDatos { get; set; }

        [Required(ErrorMessage = "El servidor de base de datos es obligatorio")]
        [StringLength(50, ErrorMessage = "El servidor no puede exceder los 50 caracteres")]
        public string ServidorBaseDatos { get; set; }

        [Required(ErrorMessage = "El usuario de base de datos es obligatorio")]
        [StringLength(50, ErrorMessage = "El usuario no puede exceder los 50 caracteres")]
        public string UsuarioBaseDatos { get; set; }

        [Required(ErrorMessage = "La contraseña de base de datos es obligatoria")]
        [StringLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres")]
        public string PasswordBaseDatos { get; set; }

        public string Estado { get; set; } = "A";

        public List<int> AplicativosIds { get; set; } = new List<int>();
    }

}
