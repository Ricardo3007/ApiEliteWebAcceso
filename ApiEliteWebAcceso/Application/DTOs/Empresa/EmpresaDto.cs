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
    }

    public class EmpresaPorGrupoDto
    {
        // Campos del DTO
        public int? idEmpresaDTO { get; set; } // ID de la empresa (opcional)
        public string? nombreDTO { get; set; } // Nombre de la empresa (opcional)
        public int? idGrupoEmpresaDTO { get; set; } // FK al grupo de la empresa (opcional)
        public string? nombreGrupoEmpresaDTO { get; set; } // Nombre de la empresa (opcional)
    }
}
