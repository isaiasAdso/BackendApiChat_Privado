using System.ComponentModel.DataAnnotations;

namespace webSocketChat.Models
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }

    }
}
