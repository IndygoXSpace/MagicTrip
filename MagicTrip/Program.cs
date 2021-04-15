using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Gra
{
    class Program
    {
        private static Bohater _bohater;
        private static List<IBron> _bronie;
        private static List<Zbroja> _zbroje;
        static void Main(string[] args)
        {
            StworzBronie();
            ObslugaMenu();
        }

        static void StworzBronie()
        {
            Dane dane = new Dane();
            _bronie = dane.WczytajBronie();
            _zbroje = new List<Zbroja>();

            //ta część kodu po utworzeniu pliku zaczytujacego bronię jest niepotrzebna

            /*Bron bron = new Bron("Wrzeszczący Kijaszek Pogardliwej Pogardy", 3,4);
         
            _bronie.Add(bron);
            _bronie.Add(new Bron("Mroczny Róg Jednorożca Zdobywcy",10,6));
            _bronie.Add(new Bron("Badyl", 1, 100));
            _bronie.Add(new BronDwureczna("Śmiercionośna Dwuręczna Łodyga Zagłady", 15, 4)); */

            _zbroje.Add(new Tarcza
            {
                Nazwa = "Tarcza Niebios",
                Obrona = 10,
                PoziomZuzycia = 0
            });

            Napiersnik napiersnik = new Napiersnik();
            napiersnik.Nazwa = "Klata chwały";
            napiersnik.Obrona = 15;
            napiersnik.PoziomZuzycia = 0;

            _zbroje.Add(napiersnik);
        }

        static void ObslugaMenu()
        {
            Console.WriteLine("1. Nowa gra");
            Console.WriteLine("2. Wczytaj grę");
            Console.WriteLine("3. Koniec");

            string opcja = Console.ReadLine();

            if (opcja == "1")
            {
                StworzPostac();
            }

            else if (opcja == "2")
            {
                WczytajGre();
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Dzięki za grę :)");
                return;
            }

            while (opcja != "6")
            {
                MenuGry();
                opcja = Console.ReadLine();

                if (opcja == "0")
                {
                    _bohater.PokazPostac();
                }
                else if (opcja == "1")
                {
                    IdzNaWyprawe();
                }
                else if (opcja == "2")
                {
                    _bohater.Odpocznij();
                }
                else if (opcja == "3")
                {
                    Console.WriteLine("Opcja chwilowo niedostępna");
                }
                if (opcja == "4")
                {
                    Sklep();
                }
                else if (opcja == "5")
                {
                    Dane dane = new Dane();
                    dane.ZapiszGrę(_bohater);
                }

                _bohater.Przegrana();
                Console.WriteLine();
                Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
                Console.ReadLine();
            }
        }

        static void WczytajGre()
        {
            Console.Clear();
            Dane dane = new Dane();
            List<Bohater> gry = dane.ListaZapisanychGier().ToList();
            foreach (Bohater bohater in gry)
            {
                Console.WriteLine($"{bohater.NrZapisu}. {bohater.Imie}");
            }
            string opcja = Console.ReadLine();
            _bohater = gry.FirstOrDefault(g => g.NrZapisu == int.Parse(opcja));
            //foreach(Bohater g in gry)
            //{
            //    if(g.NrZapisu == int.Parse(opcja))
            //    {
            //        _bohater = g;
            //        break;
            //    }
            //}
        }

        static void MenuGry()
        {
            Console.Clear();
            Console.WriteLine("0. Zobacz postać");
            Console.WriteLine("1. Idź na wyprawę");
            Console.WriteLine("2. Odpocznij");
            Console.WriteLine("3. Ekwipunek");
            Console.WriteLine("4. Sklep");
            Console.WriteLine("5. Zapisz grę");
            Console.WriteLine("6. Koniec");
        }

        static int DajWieksza(int liczba1, int liczba2)
        {
            if (liczba1 < liczba2)
            {
                return liczba2;
            }
            else
            {
                return liczba1;
            }
        }

        static void StworzPostac()
        {
            Console.Clear();
            Console.Write("Podaj imie postaci: ");
            string imie = Console.ReadLine();
            _bohater = new Bohater(imie);
        }

        static void IdzNaWyprawe()
        {
            Console.Clear();
            Console.WriteLine("Wyruszyłeś na wyprawę");
            bool wynikWalki = Walka();

            if (wynikWalki)
            {
                BonusyZaZwyciestwo();
            }
        }

        static void BonusyZaZwyciestwo()
        {

        }

        static bool Walka()
        {
            Random losuj = new Random();
            int zyciePrzeciwnika = losuj.Next(8, 12);

            while (_bohater.PosiadaneZycie > 0)
            {
                int obrazenia = _bohater.NaszaBron.ObliczObrazenia();
                int obrazeniaZadane = losuj.Next(obrazenia - 2, obrazenia + 2);
                zyciePrzeciwnika -= obrazeniaZadane;

                if (zyciePrzeciwnika <= 0)
                {
                    return true;
                }

                int obrazeniaOtrzymane = losuj.Next(0, 4);
                _bohater.PosiadaneZycie -= obrazeniaOtrzymane;
            }
            return false;
        }

        static void Sklep()
        {
            Console.Clear();
            int licznik = 1;
            foreach (IBron bron in _bronie)
            {
                Console.WriteLine(licznik + ". " + bron.Nazwa);
                licznik++;
            }
            foreach (Zbroja zbroja in _zbroje)
            {
                Console.WriteLine(licznik + ". " + zbroja.Nazwa);
                licznik++;
            }

            Console.WriteLine("Wybierz broń: ");
            string odczyt = Console.ReadLine();
            int opcja = int.Parse(odczyt);

            if (opcja <= _bronie.Count)
            {
                IBron wybranaBron = _bronie[opcja - 1];
                _bohater.KupBron(wybranaBron);
            }
            else
            {
                opcja -= _bronie.Count;
                Zbroja wybranaZbroja = _zbroje[opcja - 1];
                if (wybranaZbroja is Tarcza)
                {
                    _bohater.NoszonaTarcza = wybranaZbroja as Tarcza; //to jest to samo co ->  (is łączy się z as)
                }
                else
                {
                    _bohater.NoszonyNapiersnik = (Napiersnik)wybranaZbroja;// <- to, tylko inaczej zapisane
                }
            }
        }
    }
}