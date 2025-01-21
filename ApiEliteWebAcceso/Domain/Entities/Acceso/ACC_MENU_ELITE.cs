using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_MENU_ELITE
{
    public int PK_OPCION_MENU_C { get; set; }

    public int FK_APLICATIVO_C { get; set; }

    public string URL_C { get; set; } = null!;

    public int? PARENT_C { get; set; }

    public string TEXT_C { get; set; } = null!;

    public string? DESCRIPCION_C { get; set; }

    public int? ICONO_C { get; set; }

    public string ESTADO_C { get; set; } = null!;

    public virtual ICollection<ACC_OPCIONES_ROL> ACC_OPCIONES_ROL { get; set; } = new List<ACC_OPCIONES_ROL>();

    public virtual ACC_APLICACION FK_APLICATIVO_CNavigation { get; set; } = null!;
}
