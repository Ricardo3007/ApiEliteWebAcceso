using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class Usuarios
{
    public int pk_usuario_c { get; set; }

    public string documento_c { get; set; } = null!;

    public string? nombre_c { get; set; }

    public string? email_c { get; set; }

    public string password_c { get; set; } = null!;

    public int fk_tercero_c { get; set; }

    public DateTime? fecha_creacion_c { get; set; }

    public DateTime? fecha_actualizacion_c { get; set; }

    public string? estado_c { get; set; }

    public virtual ICollection<Usuario_Empresa> Usuario_Empresa { get; set; } = new List<Usuario_Empresa>();

    public virtual ICollection<Usuario_Rol> Usuario_Rol { get; set; } = new List<Usuario_Rol>();
}
