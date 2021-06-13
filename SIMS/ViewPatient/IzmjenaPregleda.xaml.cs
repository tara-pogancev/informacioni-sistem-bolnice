using SIMS.Repositories.SecretaryRepo;
using SIMS.Model;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS.Controller;
using SIMS.Service;
using SIMS.Repositories.RoomRepo;
using SIMS.Service.AppointmentServices;
using SIMS.Controller;

namespace SIMS.PacijentGUI
{
    /// <summary>
    /// Interaction logic for IzmjenaPregleda.xaml
    /// </summary>
    
    public partial class IzmjenaPregleda : Page
    {

        private List<Doctor> lekari;
        private ObservableCollection<String> dostupniTermini;
        public String Vrijeme { get; set;}
        public Appointment OdabraniTerminZaIzmjenu { get; set; }
        Boolean doktorSelektovan;
        Patient pacijent;
        List<Room> slobodneProstorije;
        Doctor izabraniLekar;
        public bool firstOpen;
        
        
        
        
        public IzmjenaPregleda(Appointment termin)
        {
            InitializeComponent();
            lekari = new DoctorController().GetDoctorsForExamination();
            pacijent = PocetnaStranica.getInstance().Pacijent;
            doktorSelektovan = false;
            dostupniTermini = new ObservableCollection<String>(new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" });
            OdabraniTerminZaIzmjenu = termin;
            izabraniLekar = termin.Doctor;
            slobodneProstorije = new RoomFileRepository().UcitajProstorijeZaPreglede();
            firstOpen = true;
            

            Doktori.ItemsSource = lekari;
            BlokirajDatumeNaKalendaru();
            FillComboBoxes(termin);
            this.DataContext = this;
            terminiLista.ItemsSource = dostupniTermini;
            Vrijeme = OdabraniTerminZaIzmjenu.GetAppointmentTime();
            //Vrijeme = "";
        }

        private void BlokirajDatumeNaKalendaru()
        {
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, OdabraniTerminZaIzmjenu.InitialTime.AddDays(-3));
            CalendarDateRange cdr1 = new CalendarDateRange(OdabraniTerminZaIzmjenu.InitialTime.AddDays(3), DateTime.MaxValue);
            OdabirDatuma.BlackoutDates.Add(cdr);
            OdabirDatuma.BlackoutDates.Add(cdr1);
        }

       

        
        private void IzbacivanjeNedostupnihTermina()
        {
            Doctor chosenDoctor = (Doctor)Doktori.SelectedItem;
            String chosenDate = OdabirDatuma.SelectedDate.Value.ToString("dd.MM.yyyy.");
            dostupniTermini = new ObservableCollection<string>(new ScheduleAppointmentControler().GetAvailableTimeOfAppointment(chosenDoctor, chosenDate, pacijent));
            terminiLista.ItemsSource = dostupniTermini;
        }

        

        public void FillComboBoxes(Appointment termin)
        {
            OdabirDatuma.DisplayDate = termin.StartTime;
            OdabirDatuma.Text = termin.StartTime.ToString("dd.MM.yyyy.");
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
                
                if (firstOpen == false)
                {
                    terminiLista.Text= "";
                    terminiLista.SelectedIndex = -1;
                }
                else
                {
                    firstOpen = false;
                }
                
            }
        }

        private void OdabirDatuma_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doktorSelektovan)
            {
                IzbacivanjeNedostupnihTermina();
                if (firstOpen == false)
                {
                    terminiLista.Text= "";
                    terminiLista.SelectedIndex = -1;
                }
                else
                {
                    firstOpen = false;
                }
            }

        }

        bool ValidirajPopunjenostPolja()
        {
            return (Doktori.SelectedItem == null || OdabirDatuma.SelectedDate == null) ||
                   terminiLista.Text =="";
        }

        bool ValidirajUneseniDatum()
        {
            return (OdabirDatuma.SelectedDate.Value > OdabraniTerminZaIzmjenu.InitialTime.AddDays(2)) ||
                   (OdabirDatuma.SelectedDate.Value < OdabraniTerminZaIzmjenu.InitialTime.AddDays(-3));
        }

        private bool ValidirajUnos()
        {
            if (ValidirajPopunjenostPolja())
            {
                Poruka.Visibility = Visibility.Visible;
                Task.Delay(3000).ContinueWith(_ =>
                {
                    Application.Current.Dispatcher.Invoke(
                        System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(
                            delegate ()
                            {
                                Poruka.Visibility = Visibility.Collapsed;
                            }
                        ));
                });
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
            PocetnaStranica.getInstance().frame.Content = mojiTermini;

        }

        private void IzmijeniTermin()
        {
            
            OdabraniTerminZaIzmjenu.Doctor.Jmbg = lekari[Doktori.SelectedIndex].Jmbg;
            OdabraniTerminZaIzmjenu.StartTime = DateTime.Parse(OdabirDatuma.Text + " " + terminiLista.Text);
            OdabraniTerminZaIzmjenu.Room = slobodneProstorije[0];
            new AppointmentController().UpdateAppointment(OdabraniTerminZaIzmjenu);
            
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
            PocetnaStranica.getInstance().frame.Content = mojiTerminiPage;
        }

        private void terminiLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomAvailabilityService roomAvailabilityService = new RoomAvailabilityService();
            DateTime zakazanoVrijemeIzmjenjenogTermina = DateTime.Parse(OdabirDatuma.SelectedDate.Value.Date.ToString("dd.MM.yyyy. ") + terminiLista.SelectedItem);
            
            if (roomAvailabilityService.IsFreeRoomExists(zakazanoVrijemeIzmjenjenogTermina) == false)
            {
                IspisiUpozorenje();
            }
            else
            {
                slobodneProstorije = new RoomAvailabilityService().GetAvailableRooms(zakazanoVrijemeIzmjenjenogTermina);
            }
            
            
        }

        

       private void IspisiUpozorenje()
        {
            
            
                MessageBox.Show("Trenutno ne postoji slobodna ordinacija za ovaj termin. Milimo Vas izaberite neki drugi termin!");
                UkloniNedostupniTermin();
            
        }

        private void UkloniNedostupniTermin()
        {
            if (terminiLista.SelectedIndex<0)
            {
                return;
            }
            dostupniTermini.RemoveAt(terminiLista.SelectedIndex);
            terminiLista.ItemsSource = dostupniTermini;
            terminiLista.SelectedIndex = -1;
        }

       
    }
}
