using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class OperacijaCreate : Window
    {
        private List<Lekar> lekari;
        private List<Pacijent> pacijenti;
        private List<Prostorija> prostorije;
        private List<String> dostupniTermini;
        Termin termin = new Termin();

        public OperacijaCreate()
        {
            InitializeComponent();

            LekarStorage storageL = new LekarStorage();
            lekari = storageL.Read();

            PacijentStorage storageP = new PacijentStorage();
            //pacijenti = storageP.Read();

            ProstorijaStorage storagePr = new ProstorijaStorage();
            //prostorije = storagePr.Read();

            doktoriCombo.ItemsSource = lekari;
            //pacijentiCombo.ItemSource = pacijenti;
            //prostorijeCombo.ItemSource = prostorije;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Kreiranje novog pregleda
            //TODO: Odraditi sve provere

            //TODO: dodati za izbor prostorije i pacijente
            if (doktoriCombo.SelectedItem == null || datePicker1.SelectedDate == null || terminiLista.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                //INIT DATA
                Drzava SrbijaT = new Drzava("Srbija");
                Grad BP = new Grad("Backa Palanka", 15000, SrbijaT);
                Adresa adresaT = new Adresa("Vojvode Putnika", 1, BP);
                UlogovanKorisnik TaraP = new UlogovanKorisnik("Tara", "Pogancev", "1234567891021", "doktor", "doktor", "tara123@gmail.com", "0645568131", adresaT);
                Pacijent p = new Pacijent(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, "00777000", false);
                Prostorija prostorija = new Prostorija(adresaT, 2, 22, true, TipProstorije.zaPreglede);

                termin.Prostorija = prostorija;
                termin.Pacijent = p;

                int durationMin = (duration.SelectedIndex + 1) * 30;
                TimeSpan terminSpan = new TimeSpan(0, durationMin, 0);

                termin.Lekar = lekari[doktoriCombo.SelectedIndex];
                String vreme = datePicker1.Text + " " + terminiLista.Text;
                termin.PocetnoVreme = DateTime.Parse(vreme);
                termin.VremeTrajanja = terminSpan;
                termin.VrstaTermina = TipTermina.operacija;

                LekarUI.getInstance().dodajTermin(termin);
                this.Close();
            }
            
        }


        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktoriCombo.SelectedItem != null)
            {
                Lekar lek = lekari[doktoriCombo.SelectedIndex];
                List<Termin> doktoroviTermini = new List<Termin>();
                dostupniTermini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                terminiLista.ItemsSource = dostupniTermini;
                
                foreach (Termin termin in PacijentUI.getInstance().Termini)
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
