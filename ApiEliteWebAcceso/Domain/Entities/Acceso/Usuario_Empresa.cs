using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class Usuario_Empresa
{
    public int pk_usuario_empresa_c { get; set; }

    public int fk_usuario_c { get; set; }

    public int fk_empresa_c { get; set; }

    public string? estado_c { get; set; }

    public virtual Empresas fk_empresa_cNavigation { get; set; } = null!;

    public virtual Usuarios fk_usuario_cNavigation { get; set; } = null!;
}
