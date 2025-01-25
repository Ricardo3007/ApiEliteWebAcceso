using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_OPCIONES_ROL
{
    public int PK_OPCION_ROL_C { get; set; }

    public int FK_OPCION_MENU_C { get; set; }

    public int FK_ROL_C { get; set; }

    public virtual ACC_MENU_ELITE FK_OPCION_MENU_CNavigation { get; set; } = null!;

    public virtual ACC_ROLES FK_ROL_CNavigation { get; set; } = null!;
}
