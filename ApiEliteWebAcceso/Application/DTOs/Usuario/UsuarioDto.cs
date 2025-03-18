
using ApiEliteWebAcceso.Application.DTOs.Empresa;

namespace ApiEliteWebAcceso.Application.DTOs.Usuario
{
    public class UsuarioDto
    {

        public int idUsuarioDTO { get; set; }

        public string? usuarioDTO { get; set; }

        public string? documentoDTO { get; set; }

        public string? nombreDTO { get; set; }

        public int? tipoUsuarioDTO { get; set; }

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
    }
}