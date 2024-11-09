using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class Usuario_Rol
{
    public int pk_usuario_rol_c { get; set; }

    public int fk_usuario_c { get; set; }

    public int fk_rol_c { get; set; }

    public virtual Roles fk_rol_cNavigation { get; set; } = null!;

    public virtual Usuarios fk_usuario_cNavigation { get; set; } = null!;
}
