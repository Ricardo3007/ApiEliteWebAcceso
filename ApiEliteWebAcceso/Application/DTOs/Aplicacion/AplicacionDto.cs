namespace ApiEliteWebAcceso.Application.DTOs.Aplicacion
{

    public class AplicativoDto
    {
        public int? IdAplicativoDTO { get; set; }  // ID del aplicativo (opcional)
        public string? InicialesAplicativoDTO { get; set; } // Iniciales del aplicativo
        public string? NombreAplicativoDTO { get; set; } // Nombre del aplicativo
        public byte? OrdenDTO { get; set; } // Orden del aplicativo
        public string? EstadoDTO { get; set; } // Estado del aplicativo
    }

}
