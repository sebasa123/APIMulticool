using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class Repuesto
    {
        public Repuesto()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Idrep { get; set; }
        public bool CompletoRep { get; set; }
        public string DescripcionRep { get; set; } = null!;
        public int FktipoRep { get; set; }
        public int Fkherramientas { get; set; }

        public virtual ICollection<Pedido>? Pedidos { get; set; }
    }
}
