using SIMS.Repositories.PatientRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
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

        private List<Doctor> lekari;
        private ObservableCollection<String> moguceSatniceTermina;
        Appointment odabraniTerminZaIzmjenu;
        Boolean doktorSelektovan;
        Patient pacijent;
        List<Room> slobodneProstorije;
        Doctor izabraniLekar;
        
        
        
        public IzmjenaPregleda(Appointment termin)
        {
            InitializeComponent();
            
           
            lekari = new DoctorFileRepository().GetAll();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            doktorSelektovan = false;
            moguceSatniceTermina = new ObservableCollection<String>(new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
            odabraniTerminZaIzmjenu = termin;
            izabraniLekar = termin.Lekar;
            slobodneProstorije = new RoomRepository().UcitajProstorijeZaPreglede();

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

        bool LekarZauzet(Appointment zakazaniTermin)
        {
            return zakazaniTermin.Lekar.Jmbg.Equals(izabraniLekar.Jmbg);
        }

        bool PacijentZauzet(Appointment zakazaniTermin)
        {
            return pacijent.Jmbg.Equals(zakazaniTermin.Pacijent.Jmbg);
        }

        bool PoklapanjeDatuma(Appointment zakazaniTermin)
        {
            return (OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy.").Equals(zakazaniTermin.Datum) &&
                !this.odabraniTerminZaIzmjenu.Vrijeme.Equals(zakazaniTermin.Vrijeme));
        }

        bool TerminSeNeMozeZakazati(Appointment termin)
        {
            return PoklapanjeDatuma(termin) && (PacijentZauzet(termin) || LekarZauzet(termin));
        }

        



        private void IzbacivanjeNedostupnihTermina()
        {
            Doctor lekar = (Doctor)Doktori.SelectedItem;
            moguceSatniceTermina = new ObservableCollection<String>(new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
            List<Appointment> zakazaniTermini = new AppointmentFileRepository().GetAll();
            
            foreach (Appointment termin in zakazaniTermini)
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
            foreach (Doctor lekar in lekari)
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

        public void FillComboBoxes(Appointment termin)
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
                izabraniLekar = (Doctor)Doktori.SelectedItem;
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
            odabraniTerminZaIzmjenu.Prostorija.Serialize = false;
            AppointmentFileRepository.Instance.Update(odabraniTerminZaIzmjenu);
        }
        private void FormirajLog()
        {
            AppointmentLog terminLog = new AppointmentLog(FormirajKljucLoga(odabraniTerminZaIzmjenu), odabraniTerminZaIzmjenu.TerminKey, pacijent.Jmbg, DateTime.Now, SurgeryType.Izmjena);
            new AppointmentLogFileRepository().Save(terminLog);
        }

        public String FormirajKljucLoga(Appointment termin)
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
            slobodneProstorije = new RoomRepository().UcitajProstorijeZaPreglede();
            DateTime zakazanoVrijemeIzmjenjenogTermina = DateTime.Parse(OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy. ") + terminiLista.SelectedItem);
            foreach (Appointment termin in new AppointmentFileRepository().GetAll())
            {
                if (postojiZakazanTermin(termin,zakazanoVrijemeIzmjenjenogTermina))
                {
                    IzbaciProstoriju(termin.Prostorija.Number);
                }
            }
            IspisiUpozorenje();
            
        }

        private bool postojiZakazanTermin(Appointment termin,DateTime zakazanoVrijemeIzmjenjenogTermina)
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
            if (terminiLista.SelectedIndex<0)
            {
                return;
            }
            moguceSatniceTermina.RemoveAt(terminiLista.SelectedIndex);
            terminiLista.ItemsSource = moguceSatniceTermina;
            terminiLista.SelectedIndex = -1;
        }

        private void IzbaciProstoriju(String brojProstorije)
        {
            for (int j = 0; j < slobodneProstorije.Count; j++)
            {
                if (slobodneProstorije[j].Number == brojProstorije)
                {
                    slobodneProstorije.RemoveAt(j);
                    j--;
                }
            }

        }
    }
}
