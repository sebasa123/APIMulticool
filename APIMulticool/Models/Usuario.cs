using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            CodigoRecuperacions = new HashSet<CodigoRecuperacion>();
            Pedidos = new HashSet<Pedido>();
        }

        public int Idus { get; set; }
        public string NombreUs { get; set; } = null!;
        public string ContraUs { get; set; } = null!;
        public int FktipoUsuario { get; set; }
        public bool EstadoUs { get; set; }

        public virtual TipoUsuario? FktipoUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<CodigoRecuperacion>? CodigoRecuperacions { get; set; }
        public virtual ICollection<Pedido>? Pedidos { get; set; }
    }
}
