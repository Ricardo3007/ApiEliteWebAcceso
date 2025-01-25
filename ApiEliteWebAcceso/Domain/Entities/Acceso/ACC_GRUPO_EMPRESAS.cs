using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_GRUPO_EMPRESAS
{
    public int PK_GRUPO_EMPRESA_C { get; set; }

    public string NOMBRE_GRUPO_C { get; set; } = null!;

    public string ESTADO_C { get; set; } = null!;

    public virtual ICollection<ACC_EMPRESA> ACC_EMPRESA { get; set; } = new List<ACC_EMPRESA>();

    public virtual ICollection<ACC_ROLES> ACC_ROLES { get; set; } = new List<ACC_ROLES>();
}
