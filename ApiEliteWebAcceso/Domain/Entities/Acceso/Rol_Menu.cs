using System;
using System.Collections.Generic;

namespace ApiEliteWebAcceso.Domain.Entities.Acceso;

public partial class Rol_Menu
{
    public int pk_rol_menu_c { get; set; }

    public int fk_rol_c { get; set; }

    public int fk_menu_c { get; set; }

    public virtual Menus fk_menu_cNavigation { get; set; } = null!;

    public virtual Roles fk_rol_cNavigation { get; set; } = null!;
}
