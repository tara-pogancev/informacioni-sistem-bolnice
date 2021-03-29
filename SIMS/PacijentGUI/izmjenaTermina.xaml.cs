using Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for izmjenaTermina.xaml
    /// </summary>
    public partial class izmjenaTermina : Window
    {
        private List<Lekar> lekari;
        private List<String> termini;
        List<String> imena = new List<String>();
        Termin termin;
        Boolean doktorSelektovan;
        PacijentUI pacijentUI;
        public izmjenaTermina(Termin termin,PacijentUI ui)
        {
            InitializeComponent();
            this.pacijentUI = ui;
            LekarStorage lk = new LekarStorage();
            doktorSelektovan = false;
            lekari = new List<Lekar>();
            lekari = lk.Read();
            this.termin = termin;

            
            filtrirajTermine();
            /*Drzava SrbijaT = new Drzava("Srbija");
            Grad BP = new Grad("Backa Palanka", 15000, SrbijaT);
            Adresa adresaT = new Adresa("Vojvode Putnika", 1, BP);
            UlogovanKorisnik TaraP = new UlogovanKorisnik("Tara", "Pogancev", "1234567891021", "doktor", "doktor", "tara123@gmail.com", "0645568131", adresaT);

            Lekar l = new Lekar(TaraP.Ime, TaraP.Prezime, TaraP.Jmbg, TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, 15);
            Lekar l2 = new Lekar("Milan", "Markovic", "1", TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, 15);
            Lekar l3 = new Lekar("Sonja", "Simovic", "2", TaraP.KorisnickoIme, TaraP.Lozinka, TaraP.Email, TaraP.Telefon, TaraP.Adresa, 15);
            lekari.Add(l);
            lekari.Add(l2);
            lekari.Add(l3);
            lk.Create(lekari);*/

            terminiLista.ItemsSource = termini;
            doktori.ItemsSource = lekari;

        }

        private void filtrirajTermine()
        {
            termini = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            foreach (Termin ter in new TerminStorage().ReadAll())
            {
                if (termin.Lekar.Jmbg.Equals(ter.Lekar.Jmbg))
                {
                    if (termin.Datum.Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                        termini.Remove(ter.Vrijeme);
                }
            }
        }

        private void filtrirajTermine1()
        {
            Lekar lek = (Lekar)doktori.SelectedItem;
            termini = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            foreach (Termin ter in new TerminStorage().ReadAll())
            {
                if (termin.Lekar.Jmbg.Equals(lek.Jmbg))
                {
                    if (datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(ter.Datum) && !termin.Vrijeme.Equals(ter.Vrijeme))
                        termini.Remove(ter.Vrijeme);
                }
            }
            terminiLista.ItemsSource = termini;
        }

        public void fillComboBoxes(Termin termin)
        {
            datePicker1.DisplayDate = termin.PocetnoVreme;
            datePicker1.Text = termin.PocetnoVreme.ToString("dd/MM/yyyy");
            int index = 0;
            foreach (Lekar lek in lekari) {
                if (lek.Jmbg.Equals(termin.Lekar.Jmbg)) {
                    break;
                }
                index++;
            }
            doktori.SelectedIndex = index;
            index = 0;
            foreach (String str in termini)
            {

                if (str.Equals(termin.Vrijeme)) {
                    break;
                }
                index++;
            }
            terminiLista.SelectedIndex = index;
            doktorSelektovan = true;

        }

        private void doktori_DropDownOpened(object sender, EventArgs e)
        {
            doktori.SelectedItem = null;
        }

        private void doktori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktori.SelectedItem != null)
            {
                filtrirajTermine1();
            }
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                /*Lekar lek = lekari[doktori.SelectedIndex];
                List<Termin> doktoroviTermini = new List<Termin>();
                termini = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                terminiLista.ItemsSource = termini;
                foreach (Termin termin in PacijentUI.getInstance().Termini)
                {
                    if (termin.Lekar.Jmbg.Equals(lek.Jmbg) && datePicker1.SelectedDate.Value.Date.ToShortDateString().Equals(termin.Datum))
                    {
                        doktoroviTermini.Add(termin);
                    }
                }

                foreach (Termin termin in doktoroviTermini)
                {
                    termini.Remove(termin.Vrijeme);
                }
                */
                filtrirajTermine1();
            }
            
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            termin.Lekar = lekari[doktori.SelectedIndex];
            String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            termin.PocetnoVreme = vremenskaOdrednica;
            foreach (Termin term in pacijentUI.Termini)
            {

            }
            this.Close();
        }

        private void Odbaci_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    
    }
}



   
 
