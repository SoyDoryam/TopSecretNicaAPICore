namespace TopSecretNicaAPICore.Models
{
    public class Usuarios
    {
        public int UsuarioID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Correo { get; set; }
        public int RolID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }
}
