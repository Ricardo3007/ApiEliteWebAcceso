namespace ApiEliteWebAcceso.Application.DTOs.Roles
{
    public class RolesDTO
    {
        public int IdRolDTO { get; set; }
        public string? NombreRolDTO { get; set; }
        public int? GrupoEmpresaIdDTO { get; set; }
        public string? NombreGrupoDTO { get; set; }
        public string? EstadoDTO { get; set; }
    }

    public class RolesOpcionMenu
    {
        // Propiedad para el identificador único de la opción de menú
        public int OpcionMenuId { get; set; }

        // Propiedad para el identificador del menú padre (si aplica)
        public int? ParentId { get; set; } // Nullable, por si no tiene un menú padre

        // Propiedad para el texto o nombre de la opción de menú
        public string ?Texto { get; set; }

        // Propiedad para el identificador del aplicativo asociado
        public int AplicativoId { get; set; }

        // Propiedad para las iniciales del aplicativo
        public string ?InicialesAplicativo { get; set; }

        // Propiedad para el orden de la opción en el menú
        public int Orden { get; set; }
        public int Check { get; set; }
    }
}
