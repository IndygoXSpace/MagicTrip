using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    public class Bohater
    {
        public int NrZapisu { get; set; }
        public string Imie { get; set; }
        public int MaksymalneŻycie { get; set; }
        public int PosiadaneZycie { get; set; }
        public int Obrazenia { get; set; }
        public int Level { get; set; }
        public int PunktyDoswiadczenia { get; set; }
        public int Sakwa { get; set; }
        public IBron NaszaBron { get; private set; }
        public Napiersnik NoszonyNapiersnik { get; set; }
        public Tarcza NoszonaTarcza { get; set; }

        public Bohater (string imie)
        {
            Imie = imie;
            MaksymalneŻycie = 10;
            PosiadaneZycie = 10;
            Level = 1;
            PunktyDoswiadczenia = 0;
            Sakwa = 100;
        }

        public void Przegrana()
        {
            if (PosiadaneZycie <= 0)
            {
                PosiadaneZycie = 1;
                Console.WriteLine("Looser ... przegrałeś ciamajdo. ale jako buk i włatca daję Ci 1 życia. Nie zmarnuj tego");
            }            
        }

        public void Odpocznij()
        {
            Console.WriteLine("Rozbiłeś szałas. Chyba nic Cię nie zje, więc ucinasz sobie drzemkę");
            PosiadaneZycie++;
            if (MaksymalneŻycie < PosiadaneZycie)
            {
                PosiadaneZycie = MaksymalneŻycie;
            }
        }

        public void PokazPostac()
        {
            Console.WriteLine(Imie + " Lvl: " + Level);
            Console.WriteLine("Życie: " + PosiadaneZycie + "/" + MaksymalneŻycie);
            Console.WriteLine("Sakwa " + Sakwa + " golda");
            if(NaszaBron != null)
            {
                Console.WriteLine(NaszaBron.Nazwa + "  obrażenia: " + NaszaBron.ModyfikatorObrazen);
            }
            if(NoszonaTarcza != null)
            {
                Console.WriteLine(NoszonaTarcza.Nazwa);
            }
            if(NoszonyNapiersnik != null)
            {
                Console.WriteLine(NoszonyNapiersnik.Nazwa);
            }            
        }

        public void KupBron(IBron bron)
        {
            if (bron.Cena <= Sakwa)
            {
                Sakwa -= bron.Cena;
                NaszaBron = bron;
                Console.WriteLine("Od teraz dzierżysz " + bron.Nazwa);
            }
            else
            {
                Console.WriteLine("Nie stać cię bidoku");
            }            
        }
    }
}