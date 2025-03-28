namespace ApiEliteWebAcceso.Application.DTOs.Menu
{
    public class MenuNodeDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public bool? Checked { get; set; }
        public List<MenuNodeDto> Children { get; set; } = new();
    }
}
