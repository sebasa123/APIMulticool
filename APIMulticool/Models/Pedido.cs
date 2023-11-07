using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class Pedido
    {
        public int Idped { get; set; }
        public string DecripcionPed { get; set; } = null!;
        public DateTime FechaPed { get; set; }
        public int Fkrep { get; set; }
        public int Fkcli { get; set; }
        public int Fkus { get; set; }
        public int Fkprod { get; set; }
        public bool EstadoPed { get; set; }

        public virtual Cliente?FkcliNavigation { get; set; } = null!;
        public virtual Producto? FkprodNavigation { get; set; } = null!;
        public virtual Repuesto? FkrepNavigation { get; set; } = null!;
        public virtual Usuario? FkusNavigation { get; set; } = null!;
    }
}
