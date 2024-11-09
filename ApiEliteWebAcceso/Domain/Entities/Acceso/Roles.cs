using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class Roles
{
    public int pk_rol_c { get; set; }

    public string? nombre_c { get; set; }

    public DateTime? fecha_creacion_c { get; set; }

    public DateTime? fecha_actualizacion_c { get; set; }

    public int fk_empresa_c { get; set; }

    public string? estado_c { get; set; }

    public virtual ICollection<Rol_Menu> Rol_Menu { get; set; } = new List<Rol_Menu>();

    public virtual ICollection<Usuario_Rol> Usuario_Rol { get; set; } = new List<Usuario_Rol>();

    public virtual Empresas fk_empresa_cNavigation { get; set; } = null!;
}
