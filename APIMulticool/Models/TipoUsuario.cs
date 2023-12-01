using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Idtu { get; set; }
        public string NombreTu { get; set; } = null!;

        public virtual ICollection<Usuario>? Usuarios { get; set; }
    }
}
