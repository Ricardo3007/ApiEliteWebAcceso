using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class Empresas
{
    public int pk_empresa_c { get; set; }

    public string? nombre_c { get; set; }

    public string? cadena_conexion_c { get; set; }

    public string? logo_c { get; set; }

    public DateTime? fecha_creacion_c { get; set; }

    public DateTime? fecha_actualizacion_c { get; set; }

    public string? estado_c { get; set; }

    public virtual ICollection<Roles> Roles { get; set; } = new List<Roles>();

    public virtual ICollection<Usuario_Empresa> Usuario_Empresa { get; set; } = new List<Usuario_Empresa>();
}
