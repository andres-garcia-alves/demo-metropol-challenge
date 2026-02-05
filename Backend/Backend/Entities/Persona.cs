namespace Backend.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;

        // Acá podrían ir otras propiedades de la Entidad (que no pasan al DTO)
        // Por ejemplo: PasswordHash, etc
    }
}
