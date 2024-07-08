using webSocketChat.Models;
using Microsoft.EntityFrameworkCore;

namespace webSocketChat.Context
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { 
        
        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Contactos> Contactos { get; set; }
        public DbSet<Chat> Chat { get; set; }
        
    }
}
