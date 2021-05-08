using Model;
using SIMS.Model;
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
        private ObservableCollection<String> moguceSatniceTermina;
        Termin odabraniTerminZaIzmjenu;
        Boolean doktorSelektovan;
        Pacijent pacijent;
        List<Prostorija> slobodneProstorije;
        Lekar izabraniLekar;
        
        
        
        public IzmjenaPregleda(Termin termin)
        {
            InitializeComponent();
            
           
            lekari = new LekarStorage().ReadList();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            doktorSelektovan = false;
            moguceSatniceTermina = new ObservableCollection<String>(new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
            odabraniTerminZaIzmjenu = termin;
            izabraniLekar = termin.Lekar;
            slobodneProstorije = new ProstorijaStorage().UcitajProstorijeZaPreglede();

            Doktori.ItemsSource = lekari;
            BlokirajDatumeNaKalendaru();
            FillComboBoxes(termin);
            this.DataContext = this;
            terminiLista.ItemsSource = moguceSatniceTermina;
        }

        private void BlokirajDatumeNaKalendaru()
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, odabraniTerminZaIzmjenu.InicijalnoVrijeme.AddDays(-3));
            CalendarDateRange cdr1 = new CalendarDateRange(odabraniTerminZaIzmjenu.InicijalnoVrijeme.AddDays(3), DateTime.MaxValue);
            OdabirDatuma.BlackoutDates.Add(cdr);
            OdabirDatuma.BlackoutDates.Add(cdr1);
        }

        bool LekarZauzet(Termin zakazaniTermin)
        {
            return zakazaniTermin.Lekar.Jmbg.Equals(izabraniLekar.Jmbg);
        }

        bool PacijentZauzet(Termin zakazaniTermin)
        {
            return pacijent.Jmbg.Equals(zakazaniTermin.Pacijent.Jmbg);
        }

        bool PoklapanjeDatuma(Termin zakazaniTermin)
        {
            return (OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy.").Equals(zakazaniTermin.Datum) &&
                !this.odabraniTerminZaIzmjenu.Vrijeme.Equals(zakazaniTermin.Vrijeme));
        }

        bool TerminSeNeMozeZakazati(Termin termin)
        {
            return PoklapanjeDatuma(termin) && (PacijentZauzet(termin) || LekarZauzet(termin));
        }

        



        private void IzbacivanjeNedostupnihTermina()
        {
            Lekar lekar = (Lekar)Doktori.SelectedItem;
            moguceSatniceTermina = new ObservableCollection<String>(new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
            List<Termin> zakazaniTermini = new TerminStorage().ReadList();
            
            foreach (Termin termin in zakazaniTermini)
            {
                if (TerminSeNeMozeZakazati(termin))
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
            Doktori.SelectedIndex = index;
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
            OdabirDatuma.DisplayDate = termin.PocetnoVreme;
            OdabirDatuma.Text = termin.PocetnoVreme.ToString("dd.MM.yyyy.");
            PopuniDoktora();
            PopuniVrijeme();
            doktorSelektovan = true;
        }

        private void doktori_DropDownOpened(object sender, EventArgs e)
        {
            Doktori.SelectedItem = null;
        }

        private void Doktori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Doktori.SelectedItem != null)
            {
                izabraniLekar = (Lekar)Doktori.SelectedItem;
                IzbacivanjeNedostupnihTermina();
                terminiLista.SelectedIndex = -1;
            }
        }

        private void OdabirDatuma_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                IzbacivanjeNedostupnihTermina();
                terminiLista.SelectedIndex = -1;
            }

        }

        bool ValidirajPopunjenostPolja()
        {
            return (Doktori.SelectedItem == null || OdabirDatuma.SelectedDate == null) ||
                   terminiLista.SelectedItem == null;
        }

        bool ValidirajUneseniDatum()
        {
            return (OdabirDatuma.SelectedDate.Value > odabraniTerminZaIzmjenu.InicijalnoVrijeme.AddDays(2)) ||
                   (OdabirDatuma.SelectedDate.Value < odabraniTerminZaIzmjenu.InicijalnoVrijeme.AddDays(-3));
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
            odabraniTerminZaIzmjenu.Lekar.Jmbg = lekari[Doktori.SelectedIndex].Jmbg;
            odabraniTerminZaIzmjenu.PocetnoVreme = DateTime.Parse(OdabirDatuma.Text + " " + terminiLista.Text); ;
            Serijalizuj();
            FormirajLog();
        }
        private void Serijalizuj()
        {
            odabraniTerminZaIzmjenu.Lekar.Serijalizuj = false;
            odabraniTerminZaIzmjenu.Pacijent.Serijalizuj = false;
            odabraniTerminZaIzmjenu.Prostorija.Serijalizuj = false;
            TerminStorage.Instance.Update(odabraniTerminZaIzmjenu);
        }
        private void FormirajLog()
        {
            TerminLog terminLog = new TerminLog(FormirajKljucLoga(odabraniTerminZaIzmjenu), odabraniTerminZaIzmjenu.TerminKey, pacijent.Jmbg, DateTime.Now, TipOperacije.Izmjena);
            new TerminLogStorage().Create(terminLog);
        }

        public String FormirajKljucLoga(Termin termin)
        {
            return termin.TerminKey + pacijent.Jmbg + DateTime.Now.ToString("hhmmss");
        }

    

        private void Odbaci_Click(object sender, RoutedEventArgs e)
        {
            VratiSeNaPrethodnuStranicu();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            VratiSeNaPrethodnuStranicu();
        }

        private void VratiSeNaPrethodnuStranicu()
        {
            MojiTermini mojiTerminiPage = new MojiTermini(pacijent);
            PocetnaStranica.getInstance().Tabovi.Content = mojiTerminiPage;
        }

        private void terminiLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            slobodneProstorije = new ProstorijaStorage().UcitajProstorijeZaPreglede();
            DateTime zakazanoVrijemeIzmjenjenogTermina = DateTime.Parse(OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy. ") + terminiLista.SelectedItem);
            foreach (Termin termin in new TerminStorage().ReadList())
            {
                if (postojiZakazanTermin(termin,zakazanoVrijemeIzmjenjenogTermina))
                {
                    IzbaciProstoriju(termin.Prostorija.Broj);
                }
            }
            IspisiUpozorenje();
            
        }

        private bool postojiZakazanTermin(Termin termin,DateTime zakazanoVrijemeIzmjenjenogTermina)
        {
            return (termin.PocetnoVreme.Equals(zakazanoVrijemeIzmjenjenogTermina) && zakazanoVrijemeIzmjenjenogTermina != odabraniTerminZaIzmjenu.PocetnoVreme);
        }

       private void IspisiUpozorenje()
        {
            if (slobodneProstorije.Count == 0)
            {
                MessageBox.Show("Trenutno ne postoji slobodna ordinacija za ovaj termin. Milimo Vas izaberite neki drugi termin!");
                UkloniNedostupniTermin();
            }
        }

        private void UkloniNedostupniTermin()
        {
            moguceSatniceTermina.RemoveAt(terminiLista.SelectedIndex);
            terminiLista.ItemsSource = moguceSatniceTermina;
            terminiLista.SelectedIndex = -1;
        }

        private void IzbaciProstoriju(String brojProstorije)
        {
            for (int j = 0; j < slobodneProstorije.Count; j++)
            {
                if (slobodneProstorije[j].Broj == brojProstorije)
                {
                    slobodneProstorije.RemoveAt(j);
                    j--;
                }
            }

        }
    }
}
