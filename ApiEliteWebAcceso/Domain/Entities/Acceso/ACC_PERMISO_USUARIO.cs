using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_PERMISO_USUARIO
{
    public int FK_USUARIO_C { get; set; }

    public int FK_OPCION_MENU_C { get; set; }

    public int FK_EMPRESA_C { get; set; }

    public int PK_PERMISO_USUARIO_C { get; set; }

    public virtual ACC_EMPRESA FK_EMPRESA_CNavigation { get; set; } = null!;

    public virtual ACC_MENU_ELITE FK_OPCION_MENU_CNavigation { get; set; } = null!;

    public virtual ACC_USUARIO FK_USUARIO_CNavigation { get; set; } = null!;
}
