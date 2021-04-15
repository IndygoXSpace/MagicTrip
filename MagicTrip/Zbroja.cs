using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    public abstract class Zbroja
    {
        public string Nazwa { get; set; }
        public int Obrona { get; set; }
        public int PoziomZuzycia { get; set; }

        public abstract int ModyfikujObrażenia(int obrazenia);
        public void Zuzycie()
        {
            PoziomZuzycia++;
            if(PoziomZuzycia >= 100 && Obrona > 1)
            {
                Obrona--;
            }
        }        
    }
}