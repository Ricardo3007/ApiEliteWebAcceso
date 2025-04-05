using System.Runtime.ConstrainedExecution;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso
{
    public class ACC_PERMISO_USUARIO_DETALLE
    {
        public int PK_PERMISO_USUARIO_C { get; set; } // PK_PERMISO_USUARIO_C
        public int FK_USUARIO_C { get; set; }         // FK_USUARIO_C
        public int FK_OPCION_MENU_C { get; set; }     // FK_OPCION_MENU_C
        public string ?DESCRIPCION_C { get; set; }     // MENU.DESCRIPCION_C
        public int FK_EMPRESA_C { get; set; }         // FK_EMPRESA_C
        public string ?NOMBRE_EMPRESA_C { get; set; }  // EMP.NOMBRE_EMPRESA_C
        public int FK_GRUPO_EMPRESA_C { get; set; }  // EMP.FK_GRUPO_EMPRESA_C
        public string ?INICIALES_APLICATIVO_C { get; set; } // APL.INICIALES_APLICATIVO_C
        public string ?NOMBRE_APLICATIVO_C { get; set; }    // APL.NOMBRE_APLICATIVO_C
        public string? FK_ROL_C { get; set; } // APL.INICIALES_APLICATIVO_C
        public string? NOMBRE_ROL_C { get; set; }    // APL.NOMBRE_APLICATIVO_C
        public int ORDEN_C { get; set; }
        public int PERMISO_C { get; set; }
    }

    public class ACC_PERMISO_USUARIO_EMPRESA
    {
        public int PK_EMPRESA_C { get; set; } 
        public int PK_GRUPO_EMPRESA_C { get; set; }   
        public string? NOMBRE_EMPRESA_C { get; set; }  
        public string? NOMBRE_GRUPO_C { get; set; }         
        public int TIENE_PERMISO { get; set; }
        public int TIENE_MENU { get; set; }
    }


}
