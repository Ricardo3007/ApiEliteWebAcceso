using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_EMPRESA
{
    public int PK_EMPRESA_C { get; set; }

    public string NOMBRE_EMPRESA_C { get; set; } = null!;

    public int FK_GRUPO_EMPRESA_C { get; set; }

    public string ID_EMPRESA_C { get; set; } = null!;

    public string? LOGO_EMPRESA_C { get; set; }

    public string NOMBRE_BD_C { get; set; } = null!;

    public string SERVIDOR_BD_C { get; set; } = null!;

    public string USUARIO_BD_C { get; set; } = null!;

    public string PASSWORD_BD_C { get; set; } = null!;

    public string ESTADO_C { get; set; } = null!;

    public string NOMBRE_GRUPO_C { get; set; } = null;


    public virtual ICollection<ACC_PERMISO_EMPRESA> ACC_PERMISO_EMPRESA { get; set; } = new List<ACC_PERMISO_EMPRESA>();

    public virtual ACC_GRUPO_EMPRESAS FK_GRUPO_EMPRESA_CNavigation { get; set; } = null!;
}
