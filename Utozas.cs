using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps.Serialization;

namespace EUtazas2020
{
    internal class Utozas
    {
        public int Sorszam { get; set; }
        public string Datum { get; set; }

        public int Azonosito { get; set; }

        public string JegyTipus { get; set; }

        public int Ervenyesseg { get; set; }

        public Utozas(string sorok)
        {
            var sor = sorok.Split(' ');
            Sorszam = Convert.ToInt32(sor[0]);
            Datum = sor[1];
            Azonosito = Convert.ToInt32(sor[2]);
            JegyTipus = sor[3];
            Ervenyesseg = Convert.ToInt32(sor[4]);
        }

    }
}
