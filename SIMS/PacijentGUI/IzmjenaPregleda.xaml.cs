using Model;
using SIMS.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for IzmjenaPregleda.xaml
    /// </summary>
    
    public partial class IzmjenaPregleda : Page
    {

        private List<Lekar> lekari;
        private List<String> moguceSatniceTermina;
        Termin odabraniTerminZaIzmjenu;
        Boolean doktorSelektovan;
        Pacijent pacijent;
        
        
        
        public IzmjenaPregleda(Termin termin)
        {
            InitializeComponent();
            
            LekarStorage lekarStorage = new LekarStorage();
            lekari = lekarStorage.ReadList();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            doktorSelektovan = false;
            moguceSatniceTermina =  new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            this.odabraniTerminZaIzmjenu = termin;
            doktori.ItemsSource = lekari;
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue,termin.InicijalnoVrijeme.AddDays(-3));
            CalendarDateRange cdr1 = new CalendarDateRange(termin.InicijalnoVrijeme.AddDays(3), DateTime.MaxValue);
            datePicker1.BlackoutDates.Add(cdr);
            datePicker1.BlackoutDates.Add(cdr1);
            FillComboBoxes(termin);
            this.DataContext = this;
            terminiLista.ItemsSource = moguceSatniceTermina;
        }

        bool LekarZauzet(Termin termin)
        {
            return termin.Lekar.Jmbg.Equals(odabraniTerminZaIzmjenu.Lekar.Jmbg);
        }

        bool PacijentZauzet(Termin termin)
        {
            return odabraniTerminZaIzmjenu.Pacijent.Jmbg.Equals(termin.Pacijent.Jmbg);
        }

        bool PoklapanjeDatuma(Termin termin)
        {
            return (datePicker1.SelectedDate.Value.Date.ToString("dd.MM.yyyy.").Equals(termin.Datum) &&
                !this.odabraniTerminZaIzmjenu.Vrijeme.Equals(termin.Vrijeme));
        }

        bool VrijemeTerminaZauzeto(Termin termin)
        {

            return PoklapanjeDatuma(termin) && (PacijentZauzet(termin) || LekarZauzet(termin));
           
        }

        
        private void IzbacivanjeNedostupnihTermina()
        {
            Lekar lekar = (Lekar)doktori.SelectedItem;
            moguceSatniceTermina = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            List<Termin> zakazaniTermini = new TerminStorage().ReadList();
            
            foreach (Termin termin in zakazaniTermini)
            {
                if (VrijemeTerminaZauzeto(termin))
                {
                    moguceSatniceTermina.Remove(termin.Vrijeme);    
                }
            }
            terminiLista.ItemsSource = moguceSatniceTermina;

        }

        private void PopuniDoktora()
        {
            int index = 0;
            foreach (Lekar lekar in lekari)
            {
                if (lekar.Jmbg.Equals(odabraniTerminZaIzmjenu.Lekar.Jmbg))
                {
                    break;
                }
                index++;
            }
            doktori.SelectedIndex = index;
        }

        public void PopuniVrijeme()
        {
            int index = 0;

            foreach (String moguceSatnice in moguceSatniceTermina)
            {

                if (moguceSatnice.Equals(odabraniTerminZaIzmjenu.Vrijeme))
                {
                    break;
                }
                index++;
            }
            terminiLista.SelectedIndex = index;
        }

        public void FillComboBoxes(Termin termin)
        {
            datePicker1.DisplayDate = termin.PocetnoVreme;
            datePicker1.Text = termin.PocetnoVreme.ToString("dd.MM.yyyy.");
            PopuniDoktora();
            PopuniVrijeme();
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
                
                IzbacivanjeNedostupnihTermina();
                terminiLista.SelectedIndex = -1;
                
            }
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                
                IzbacivanjeNedostupnihTermina();
                terminiLista.SelectedIndex = -1;
            }

        }

        bool ValidirajPopunjenostPolja()
        {
            return (doktori.SelectedItem == null || datePicker1.SelectedDate == null) ||
                   terminiLista.SelectedItem == null;
        }

        bool ValidirajUneseniDatum()
        {
            return (datePicker1.SelectedDate.Value > odabraniTerminZaIzmjenu.InicijalnoVrijeme.AddDays(2)) ||
                   (datePicker1.SelectedDate.Value < odabraniTerminZaIzmjenu.InicijalnoVrijeme.AddDays(-3));
        }

        private bool ValidirajUnos()
        {
            if (ValidirajPopunjenostPolja())
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return false;
            }
            if (ValidirajUneseniDatum())
            {
                MessageBox.Show("Datum je samo moguce pomjeriti 2 dana od inicijalnog termina");
                return false ;
            }
            return true;
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidirajUnos())
            {
                return;
            }

            IzmijeniTermin();
            MojiTermini mojiTermini = new MojiTermini(PocetnaStranica.getInstance().Pacijent);
            PocetnaStranica.getInstance().Tabovi.Content = mojiTermini;

        }

        private void IzmijeniTermin()
        {
            odabraniTerminZaIzmjenu.Lekar.Jmbg = lekari[doktori.SelectedIndex].Jmbg;
            String vrijemeIDatum = datePicker1.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            odabraniTerminZaIzmjenu.PocetnoVreme = vremenskaOdrednica;
            TerminStorage.Instance.Update(odabraniTerminZaIzmjenu);
            formirajLog();
           
        }

        private void formirajLog()
        {
            TerminLog terminLog = new TerminLog(formirajKljucLoga(odabraniTerminZaIzmjenu), odabraniTerminZaIzmjenu.TerminKey, pacijent.Jmbg, DateTime.Now, TipOperacije.Izmjena);
            new TerminLogStorage().Create(terminLog);
        }

        public String formirajKljucLoga(Termin termin)
        {
            return termin.TerminKey + PocetnaStranica.getInstance().Pacijent.Jmbg + DateTime.Now.ToString("hhmmss");
        }

    

        private void Odbaci_Click(object sender, RoutedEventArgs e)
        {
            MojiTermini mj = new MojiTermini(PocetnaStranica.getInstance().Pacijent);
            PocetnaStranica.getInstance().Tabovi.Content = mj;

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            MojiTermini mj = new MojiTermini(PocetnaStranica.getInstance().Pacijent);
            PocetnaStranica.getInstance().Tabovi.Content = mj;
        }

        
    }
}
