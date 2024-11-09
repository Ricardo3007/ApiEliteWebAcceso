using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class Menus
{
    public int pk_menu_c { get; set; }

    public string? nombre_c { get; set; }

    public string? url_c { get; set; }

    public int? order_c { get; set; }

    public int? parent_c { get; set; }

    public string? icono_c { get; set; }

    public DateTime? fecha_creacion_c { get; set; }

    public DateTime? fecha_actualizacion_c { get; set; }

    public string? estado_c { get; set; }

    public virtual ICollection<Rol_Menu> Rol_Menu { get; set; } = new List<Rol_Menu>();
}
