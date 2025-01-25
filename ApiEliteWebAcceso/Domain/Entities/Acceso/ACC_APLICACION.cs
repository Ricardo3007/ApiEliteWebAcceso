using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_APLICACION
{
    public int PK_APLICATIVO_C { get; set; }

    public string INICIALES_APLICATIVO_C { get; set; } = null!;

    public string NOMBRE_APLICATIVO_C { get; set; } = null!;

    public byte ORDEN_C { get; set; }

    public string ESTADO_C { get; set; } = null!;

    public virtual ICollection<ACC_MENU_ELITE> ACC_MENU_ELITE { get; set; } = new List<ACC_MENU_ELITE>();

    public virtual ICollection<ACC_PERMISO_EMPRESA> ACC_PERMISO_EMPRESA { get; set; } = new List<ACC_PERMISO_EMPRESA>();
}
