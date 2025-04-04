namespace ApiEliteWebAcceso.Application.DTOs.Empresa
{
    public class PermisoEmpresaDTO
    {
        public int IdPermisoEmpresaDTO { get; set; } // Equivalente a PK_PERMISO_EMPRESA_C

        public int IdAplicativoDTO { get; set; } // Equivalente a FK_APLICATIVO_C

        public int IdEmpresaDTO { get; set; } // Equivalente a FK_EMPRESA_C

        public string EstadoDTO { get; set; } // Equivalente a ESTADO_C
    }

    public class PermisoEmpresaAplicativoDTO
    {
        public int IdPermisoEmpresaDTO { get; set; } // Equivalente a PK_PERMISO_EMPRESA_C

        public int IdAplicativoDTO { get; set; } // Equivalente a FK_APLICATIVO_C
        public string ?NombreAplicativoDTO { get; set; } // Equivalente a NOMBRE_APLICATIVO_C
        public string ?InicialesAplicativoDTO { get; set; } // Equivalente a INICIALES_APLICATIVO_C

        public int IdEmpresaDTO { get; set; } // Equivalente a FK_EMPRESA_C
        public string ?NombreEmpresaDTO { get; set; } // Equivalente a NOMBRE_EMPRESA_C

        public string ?EstadoDTO { get; set; } // Equivalente a ESTADO_C
    }

}
