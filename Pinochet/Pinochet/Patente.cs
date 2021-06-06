using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinochet
{
   public class Patente
    {
        public int IdPatente { get; set; }

        public string NomePatente { get; set; }

        public override string ToString()
        {
            return string.Format($"{IdPatente} {NomePatente}  ");
        }
    }
}
