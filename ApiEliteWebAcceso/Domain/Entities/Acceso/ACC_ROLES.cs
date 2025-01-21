using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_ROLES
{
    public int PK_ROL_C { get; set; }

    public string NOMBRE_ROL_C { get; set; } = null!;

    public int FK_GRUPO_EMPRESA_C { get; set; }

    public string ESTADO_C { get; set; } = null!;

    public virtual ICollection<ACC_OPCIONES_ROL> ACC_OPCIONES_ROL { get; set; } = new List<ACC_OPCIONES_ROL>();

    public virtual ACC_GRUPO_EMPRESAS FK_GRUPO_EMPRESA_CNavigation { get; set; } = null!;
}
