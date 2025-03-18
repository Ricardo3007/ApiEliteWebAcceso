namespace ApiEliteWebAcceso.Application.DTOs.Empresa
{
    public class PermisoEmpresaDTO
    {
        public int IdPermisoEmpresaDTO { get; set; } // Equivalente a PK_PERMISO_EMPRESA_C

        public int IdAplicativoDTO { get; set; } // Equivalente a FK_APLICATIVO_C

        public int IdEmpresaDTO { get; set; } // Equivalente a FK_EMPRESA_C

        public string EstadoDTO { get; set; } // Equivalente a ESTADO_C
    }
}
