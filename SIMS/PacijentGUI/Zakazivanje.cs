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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for zakazivanje.xaml
    /// </summary>
    public partial class Zakazivanje : UserControl
    {
        Patient pacijent;
        private List<Doctor> lekari;
        private  ObservableCollection<String> dostupniTermini;
        private Appointment termin;
        Boolean doktorSelektovan;
        List<Room> slobodneProstorije;
        public Zakazivanje(Patient p)
        {
            InitializeComponent();
            DoctorRepository lk = new DoctorRepository();
            slobodneProstorije= new RoomRepository().UcitajProstorijeZaPreglede();
            lekari = new List<Doctor>();
            lekari = lk.GetAll();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            dostupniTermini = new ObservableCollection<string> (new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
            termin = new Appointment();
            this.DataContext = this;
            doktorSelektovan = false;
            
        }

        public List<Doctor> Lekari { get => lekari; set => lekari = value; }
        public ObservableCollection<string> DostupniTermini { get => dostupniTermini; set => dostupniTermini = value; }
        public Patient Pacijent { get => pacijent; set => pacijent = value; }
        public Appointment Termin { get => termin; set => termin = value; }

        private bool validiraj()
        {
            if (ListaDoktora.SelectedItem == null || OdabirDatuma.SelectedDate == null || terminiLista.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return false;
            }
           
            return true;
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (!validiraj())
            {
                return;
            }
            
            termin.Lekar = lekari[ListaDoktora.SelectedIndex];
            String vrijemeIDatum = OdabirDatuma.Text + " " + terminiLista.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            termin.PocetnoVreme = vremenskaOdrednica;
            termin.InicijalnoVrijeme = vremenskaOdrednica;
            termin.VremeTrajanja = 30;
            termin.Pacijent = pacijent;
            termin.Prostorija = slobodneProstorije[0];
            MessageBox.Show("Termin je uspjesno zakazan");
            termin.Lekar.Serijalizuj = false;
            termin.Pacijent.Serijalizuj = false;
            termin.Prostorija.Serialize = false;
            ZakazivanjeTermina.getInstance().Zakazivanje1.Children.Clear();
            ZakazivanjeTermina.getInstance().Zakazivanje1.Children.Add(new Zakazivanje(pacijent));
            AppointmentRepository.Instance.Save(termin);
        }

        private void ListaDoktora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doktorSelektovan = true;

        }

        private void OdabirDatuma_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                Doctor lek = lekari[ListaDoktora.SelectedIndex];
                List<Appointment> nedostupniTermini = new List<Appointment>();
                dostupniTermini = new ObservableCollection<string>( new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
                
                List<Appointment> sviTermini = new AppointmentRepository().GetAll();
                if (slobodneProstorije.Count == 0)
                {
                    dostupniTermini.Clear();
                    terminiLista.ItemsSource = dostupniTermini;
                    return;
                }
                terminiLista.ItemsSource = dostupniTermini;
                foreach (Appointment termin in sviTermini)
                {
                    if ((termin.Lekar.Jmbg.Equals(lek.Jmbg) && OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy").Equals(termin.PocetnoVreme.ToString("dd.MM.yyyy")))
                    || (termin.Pacijent.Jmbg.Equals(pacijent.Jmbg) && OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy").Equals(termin.PocetnoVreme.ToString("dd.MM.yyyy"))))
                    {
                        nedostupniTermini.Add(termin);
                    }
                    
                    
                }

                foreach (Appointment termin in nedostupniTermini)
                {
                    dostupniTermini.Remove(termin.Vrijeme);
                }
                
            }
        }

        

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            slobodneProstorije = new RoomRepository().UcitajProstorijeZaPreglede();
            if (OdabirDatuma.SelectedDate == null)
            {
                MessageBox.Show("Potrebno je da prvo izaberete datum");
                terminiLista.SelectedIndex = -1;
                return;
            }

            DateTime zakazanoVrijeme =DateTime.Parse( OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy. ") + terminiLista.SelectedItem);
            foreach(Appointment termin in new AppointmentRepository().GetAll())
            {
                if (termin.PocetnoVreme.Equals(zakazanoVrijeme))
                {
                    izbaciProstoriju(termin.Prostorija.Number);
                }
            }
            if (slobodneProstorije.Count == 0)
            {
                MessageBox.Show("Trenutno ne postoji slobodna ordinacija za ovaj termin. Milimo Vas izaberite neki drugi termin!");
                dostupniTermini.RemoveAt(terminiLista.SelectedIndex);
                terminiLista.ItemsSource = dostupniTermini;
                terminiLista.SelectedIndex = -1;
            }
        }

        private void izbaciProstoriju(String brojProstorije)
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
