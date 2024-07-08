using System.ComponentModel.DataAnnotations;

namespace webSocketChat.Models
{
    public class Chat
    {
        [Key]
        public int IdChat { get; set; }
        public int IdRemitente { get; set; }
        public int IdReseptor { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string EstadoMensaje { get; set; }
        public DateTime FechaLectura { get; set; }

    }
}
