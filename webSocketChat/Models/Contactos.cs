using System.ComponentModel.DataAnnotations;

namespace webSocketChat.Models
{
    public class Contactos
    {
        [Key]
        public int IdContacto { get; set; }
        public int IdUsuario { get; set; }
        public int IdAmigo { get; set; }
        public DateTime FechaAmistad { get; set; }
        public string Estado { get; set; }

    }
}
