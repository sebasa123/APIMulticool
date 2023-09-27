using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class CodigoRecuperacion
    {
        public int Idcr { get; set; }
        public string CodigoRec { get; set; } = null!;
        public bool EstadoCr { get; set; }
        public DateTime FechaCr { get; set; }
        public string Email { get; set; } = null!;
        public int Fkusuario { get; set; }

        public virtual Usuario FkusuarioNavigation { get; set; } = null!;
    }
}
