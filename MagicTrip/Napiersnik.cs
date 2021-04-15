using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    public class Napiersnik : Zbroja
    {
        public override int ModyfikujObrażenia(int obrazenia)
        {
            Zuzycie();
            obrazenia -= Obrona;

            if(obrazenia < 0)
            {
                obrazenia = 0;
            }

            return obrazenia;
        }
    }
}