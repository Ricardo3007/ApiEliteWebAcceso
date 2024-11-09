namespace ApiEliteWebAcceso.Application.DTOs.Login
{
    public class LoginUsuarioDto
    {
        public string Documento { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public string Dispositivo { get; set; }
    }
}
