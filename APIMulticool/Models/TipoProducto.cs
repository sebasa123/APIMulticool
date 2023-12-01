using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class TipoProducto
    {
        public TipoProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public int Idtp { get; set; }
        public string NombreTp { get; set; } = null!;

        public virtual ICollection<Producto>? Productos { get; set; }
    }
}
