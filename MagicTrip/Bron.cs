using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    public class Bron : IBron
    {        
        public string Nazwa { get; set; }
        public int Cena { get; set; }
        public int ModyfikatorObrazen { get; set; }

        public bool MozliwoscNoszeniaTarczy
        {
            get
            {
                return true;
            }
        }

        public Bron(string nazwa, int cena, int modyfikatorObrazen)
        {
            Nazwa = nazwa;
            Cena = cena;
            ModyfikatorObrazen = modyfikatorObrazen;
        }

        public int ObliczObrazenia()
        {
            return ModyfikatorObrazen;
        }
    }
}