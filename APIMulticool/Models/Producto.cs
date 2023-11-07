using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Idprod { get; set; }
        public string NombreProd { get; set; } = null!;
        public int FktipoProd { get; set; }
        public bool EstadoProd { get; set; }

        public virtual TipoProducto? FktipoProdNavigation { get; set; } = null!;
        public virtual ICollection<Pedido>? Pedidos { get; set; }
    }
}
