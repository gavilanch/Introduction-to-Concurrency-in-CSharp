using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Operacion
    {
        public int Id { get; set; }
        public string Tarjeta { get; set; }
        public bool Aprobada { get; set; }
    }
}
