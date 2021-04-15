using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace Gra
{
    public class Dane
    {
        private string conString = "Data Source=DESKTOP-V691KEC;Initial Catalog=Gra; Integrated Security=True;";
        public List<IBron> WczytajBronie()
        {
            var bronie = new List<IBron>();
            using(StreamReader reader = new StreamReader("bronie.txt"))
            {
                string linia;
                while((linia = reader.ReadLine()) != null)
                {
                    //kod  <- to samo co w kod2
                    var odczyt = linia.Split(';');
                    bronie.Add(new Bron(odczyt[0], int.Parse(odczyt[1]), int.Parse(odczyt[2])));
                }
            }

            using(StreamReader reader = new StreamReader("bronieDwureczne.txt"))
            {
                string linia = reader.ReadLine();
                while(linia != null)
                {
                    //kod2 <- to samo co w kod1
                    string[] odczyt = linia.Split(';');
                    string nazwa = odczyt[0];
                    int cena = int.Parse(odczyt[1]);
                    int obrazenia = int.Parse(odczyt[2]);
                    bronie.Add(new BronDwureczna(nazwa, cena, obrazenia));
                    linia = reader.ReadLine();
                }
            }

            return bronie;
        }

        public void ZapiszGrę(Bohater bohater)
        {
            string zapytanie = "INSERT INTO Bohater (Imie, MaxZycie, PosiadaneZycie, Obrazenia, Lvl, Doswiadczenie, Sakwa) " +
                $"VALUES('{bohater.Imie}', {bohater.MaksymalneŻycie}, {bohater.PosiadaneZycie}, {bohater.Obrazenia}, {bohater.Level}, {bohater.PunktyDoswiadczenia}, {bohater.Sakwa})";
            ModyfikacjaDanych(zapytanie);
        }  
        
        public IEnumerable<Bohater> ListaZapisanychGier()
        {
            string zapytanie = "SELECT NrZapisu, Imie, MaxZycie, PosiadaneZycie, Obrazenia, Lvl, Doswiadczenie, Sakwa FROM Bohater";
            DataRowCollection wiersze = PobierzDane(zapytanie);
            foreach(DataRow wiersz in wiersze)
            {
                string imie = wiersz["Imie"].ToString();
                Bohater bohater = new Bohater(imie);
                bohater.MaksymalneŻycie = (int)wiersz["MaxZycie"];
                bohater.PosiadaneZycie = (int)wiersz["PosiadaneZycie"];
                bohater.Obrazenia = (int)wiersz["Obrazenia"];
                bohater.Level = (int)wiersz["Lvl"];
                bohater.PunktyDoswiadczenia = (int)wiersz["Doswiadczenie"];
                bohater.Sakwa = (int)wiersz["Sakwa"];
                bohater.NrZapisu = (int)wiersz["NrZapisu"];
                yield return bohater;
            }
        }

        private void ModyfikacjaDanych(string zapytanie)
        {
            using (SqlConnection sCon = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(zapytanie, sCon);
                sCon.Open();
                cmd.ExecuteNonQuery();
                sCon.Close();
            } 
        }             

        private DataRowCollection PobierzDane(string zapytanie)
        {
            using (SqlConnection scon = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(zapytanie, scon);
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
                return ds.Tables[0].Rows;
            }
        }
    }
}