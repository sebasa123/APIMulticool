using System;
using System.Collections.Generic;

namespace APIMulticool.Models
{
    public partial class Herramientum
    {
        public int Idher { get; set; }
        public string NombreHer { get; set; } = null!;
        public int NumeroHer { get; set; }
        public bool EstadoHer { get; set; }
    }
}
