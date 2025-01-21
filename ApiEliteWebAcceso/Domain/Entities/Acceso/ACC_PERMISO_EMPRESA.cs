using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_PERMISO_EMPRESA
{
    public int PK_USUARIO_C { get; set; }

    public int FK_APLICATIVO_C { get; set; }

    public int FK_EMPRESA_C { get; set; }

    public string ESTADO_C { get; set; } = null!;

    public virtual ACC_APLICACION FK_APLICATIVO_CNavigation { get; set; } = null!;

    public virtual ACC_EMPRESA FK_EMPRESA_CNavigation { get; set; } = null!;
}
