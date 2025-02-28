using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class ACC_USUARIO
{
    public int PK_USUARIO_C { get; set; }

    public string USUARIO_C { get; set; } = null!;

    public string NOMBRE_USUARIO_C { get; set; } = null!;

    public int FK_TDI_C { get; set; }

    public string ID_USUARIO_C { get; set; } = null!;

    public string? PASSWORD_C { get; set; }

    public string MAIL_USUARIO_C { get; set; } = null!;

    public string ESTADO_C { get; set; } = null!;

    public int? TIPO_USUARIO_C { get; set; }

    public virtual GRL_TIPO_IDENTIFICACION FK_TDI_CNavigation { get; set; } = null!;
}
