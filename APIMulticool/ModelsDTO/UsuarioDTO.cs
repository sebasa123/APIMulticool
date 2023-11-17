namespace APIMulticool.ModelsDTO
{
    public class UsuarioDTO
    {
        public int Idus { get; set; }
        public string NombreUs { get; set; } = null!;
        public string ContraUs { get; set; } = null!;
        public int FktipoUsuario { get; set; }
        public bool EstadoUs { get; set; }
        //public string NombreTU { get; set; } = null!;
    }
}
