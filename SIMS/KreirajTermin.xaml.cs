using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for KreirajTermin.xaml
    /// </summary>
    public partial class KreirajTermin : Window
    {
        private List<Lekar> lekari;
        private List<String> dostupniTermini;
        private Termin termin;
        private Boolean doktorSelektovan;
        private Pacijent pacijent;
        private PacijentUI pacijentUI;
        public KreirajTermin(Pacijent pacijent,PacijentUI ui)
        {
            InitializeComponent();
            lekari = new List<Lekar>();
            this.pacijent = pacijent;
            this.pacijentUI = ui;
            
            termin = new Termin();
            doktorSelektovan = false;
            LekarStorage lk = new LekarStorage();
            lekari = lk.Read();
            doktori.ItemsSource = lekari;
            

            //////////////////////////////////////////
            /*Drzava Bih = new Drzava("Bosna i Hercegovina");
            Grad Foca = new Grad("Foca", 73300, Bih);
            Adresa adresaDj = new Adresa("Barakovac", 1, Foca);
            UlogovanKorisnik Djordje = new UlogovanKorisnik("Djordje", "Krsmanovic", "1234567891021", "pacijent", "pacijent", "djordje1499@gmail.com", "0645568131", adresaDj);
            //public Pacijent(string ime, string prezime, string jmbg, string korisnickoIme, string lozinka, string email, string telefon, Adresa adresa,String lbo,Boolean gost)
            Pacijent p = new Pacijent(Djordje.Ime, Djordje.Prezime, Djordje.Jmbg, Djordje.KorisnickoIme, Djordje.Lozinka, Djordje.Email, Djordje.Telefon, Djordje.Adresa, "123", false);
            Drzava SrbijaT = new Drzava("Srbija");
            Grad BP = new Grad("Backa Palanka", 15000, SrbijaT);
            Adresa adresaT = new Adresa("Vojvode Putnika", 1, BP);
            UlogovanKorisnik TaraP = new UlogovanKorisnik("Tara", "Pogancev", "1234567891021", "doktor", "doktor", "tara123@gmail.com", "0645568131", adresaT);

            Lekar l = new Lekar(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, 15);
            Prostorija prostorija = new Prostorija(adresaT, 2, 22, true, TipProstorije.zaPreglede);
            Termin termin1 = new Termin(DateTime.Parse("13.3.2020. 13:30"), new TimeSpan(0, 30, 0), TipTermina.pregled, l, p, prostorija);
            Termin termin2 = new Termin(DateTime.Parse("13.3.2020. 15:30"), new TimeSpan(0, 30, 0), TipTermina.pregled, l, p, prostorija);
            Termin termin3 = new Termin(DateTime.Parse("19.3.2020. 13:30"), new TimeSpan(0, 30, 0), TipTermina.pregled, l, p, prostorija);
            sviTermini.Add(termin1);
            sviTermini.Add(termin2);
            sviTermini.Add(termin3);
            lekari.Add(l);*/
            //////////////////////////////////////////////////////
            
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

            termin.Lekar = lekari[doktori.SelectedIndex];
            String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            termin.PocetnoVreme = vremenskaOdrednica;
            termin.Pacijent = pacijent;
            Prostorija p = new Prostorija();
            p.Broj = 10;
            termin.Prostorija = p;
            
            pacijentUI.dodajTermin(termin);
            this.Close();
        }

        private void Odbaci_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void doktori_DropDownOpened(object sender, EventArgs e)
        {
            doktori.SelectedItem = null;
        }

        private void doktori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktori.SelectedItem != null)
            {
                doktorSelektovan = true;
            }
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                Lekar lek = lekari[doktori.SelectedIndex];
                List<Termin> doktoroviTermini = new List<Termin>();
                dostupniTermini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                terminiLista.ItemsSource = dostupniTermini;
                foreach (Termin termin in pacijentUI.Termini)
                {
                    if (termin.Lekar.Jmbg.Equals(lek.Jmbg) && datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(termin.Datum))
                    {
                        doktoroviTermini.Add(termin);
                    }
                }

                foreach (Termin termin in doktoroviTermini)
                {
                    dostupniTermini.Remove(termin.Vrijeme);
                }
            }
        }
    }
}
