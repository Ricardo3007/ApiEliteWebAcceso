namespace ApiEliteWebAcceso.Application.DTOs.Maestros
{
    public class TipoIdentificacionDto
    {
        public int IdDto { get; set; }
        public string CodigoDto { get; set; } = string.Empty;
        public string NombreDto { get; set; } = string.Empty;
        public byte ClaseDto { get; set; }
        public byte EsExpedidaDto { get; set; }
        public string SiglasDto { get; set; } = string.Empty;
        public byte CaracteresAlfanumericosDto { get; set; }
    }
}
