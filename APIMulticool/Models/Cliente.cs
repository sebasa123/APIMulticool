using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Idcli { get; set; }
        public string NombreCli { get; set; } = null!;
        public string ApellidoCli { get; set; } = null!;
        public int CedulaCli { get; set; }
        public string DireccionCli { get; set; } = null!;
        public bool EstadoCli { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
