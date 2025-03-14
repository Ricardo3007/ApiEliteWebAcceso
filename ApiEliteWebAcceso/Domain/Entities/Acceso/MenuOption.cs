namespace ApiEliteWebAcceso.Domain.Entities.Acceso
{
    public class MenuOption
    {
        public int PK_Opcion_Menu_C { get; set; } // Identificador único del menú
        public int FK_Aplicativo_C { get; set; } // Identificador del aplicativo asociado
        public string Url_C { get; set; } // URL asociada al menú
        public int? Parent_C { get; set; } // Identificador del menú padre (nullable)
        public string Text_C { get; set; } // Texto del menúccc
        public string Descripcion_C { get; set; } // Descripción del menú
        public string Icono_C { get; set; } // Ícono asociado al menú
        public string Estado_C { get; set; } // Estado del menú (ejemplo: 'A' para activo)
        public string Iniciales_Aplicativo_C { get; set; } // Iniciales del aplicativo asociado
        public string Nombre_Aplicativo_C { get; set; } // Nombre completo del aplicativo
        public int Nivel { get; set; } // Nivel de profundidad en la jerarquía del menú
        public string Jerarquia { get; set; } // Cadena representando la jerarquía del menú
        public string ParentName { get; set; } // Nombre del menú padre
        public string Tipo_C { get; set; } = null!;

    }
}
