namespace ApiEliteWebAcceso.Domain.Entities.Acceso
{
    public class Menu_Usuario
    {
            public string Usuario { get; set; }
            public string Rol { get; set; }
            public string Empresa { get; set; }
            public string Menu { get; set; }
            public string? Menu_Padre { get; set; }
            public string? URL { get; set; }
    }
}
