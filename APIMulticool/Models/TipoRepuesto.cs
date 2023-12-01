using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class TipoRepuesto
    {
        public TipoRepuesto()
        {
            Repuestos = new HashSet<Repuesto>();
        }

        public int Idtr { get; set; }
        public string DescripcionTr { get; set; } = null!;

        public virtual ICollection<Repuesto>? Repuestos { get; set; }
    }
}
