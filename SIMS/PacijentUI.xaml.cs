using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace SIMS
{

    public partial class PacijentUI : Window
    {
        private ObservableCollection<Termin> termini;

       
        private Pacijent pacijent;

      

       
        public PacijentUI(Pacijent p)
        {
            InitializeComponent();

            pacijent = p;
            TerminStorage trm = new TerminStorage();
            termini = new ObservableCollection<Termin>(trm.Read(pacijent));

            /*     Drzava Bih = new Drzava("Bosna i Hercegovina");
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
                 termini.Add(termin1);
                 termini.Add(termin2);
                 termini.Add(termin3);*/


            //trm.Create(termini.ToList()); 

            terminiTabela.ItemsSource = termini;

        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = (Termin)terminiTabela.SelectedItem;
            if (termin.VrstaTermina == TipTermina.operacija)
            {
                MessageBox.Show("Operacije moze otkazati samo sekretar");
            }
            else
            {
                termini.Remove(termin);
            }
        }

        private void Izmijeni_Click(object sender, RoutedEventArgs e)
        {
            izmjenaTermina izm = new izmjenaTermina((Termin)terminiTabela.SelectedItem,this);
            izm.Show();
            int k=terminiTabela.SelectedIndex;
            izm.fillComboBoxes(termini[k]);
        }

        private void Kreiraj_Click(object sender, RoutedEventArgs e)
        {
            KreirajTermin kreirajTermin = new KreirajTermin(pacijent,this);
            kreirajTermin.Show();
        }

        public void dodajTermin(Termin termin)
        {
            termini.Add(termin);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TerminStorage tr = new TerminStorage();
            tr.Update(termini.ToList(),pacijent);
        }

        public ObservableCollection<Termin> Termini { get => termini; set => termini = value; }

        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
    }
}
