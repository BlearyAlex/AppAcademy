namespace AppAcademy.Application.DTOs
{
    public class RolDto
    {  
        public string NameRol { get; set; }      
        public List<string> Permisos { get; set; } = new List<string>();  
    }
}
