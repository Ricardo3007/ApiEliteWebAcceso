using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class GRL_TIPO_IDENTIFICACION
{
    public int PK_TDI_C { get; set; }

    public string CODIGO_TDI_C { get; set; } = null!;

    public string NOMBRE_TDI_C { get; set; } = null!;

    public byte CLASE_TDI_C { get; set; }

    public byte SW_EXPEDIDA_C { get; set; }

    public string SIGLAS_C { get; set; } = null!;

    public byte CARACTERES_ALFANUMERICOS_C { get; set; }

    public virtual ICollection<ACC_USUARIO> ACC_USUARIO { get; set; } = new List<ACC_USUARIO>();
}
