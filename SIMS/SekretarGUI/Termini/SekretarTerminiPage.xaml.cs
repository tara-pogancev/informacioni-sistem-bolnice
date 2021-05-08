using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class SekretarTerminiPage : Page
    {
        private static SekretarTerminiPage _instance = null;

        private ObservableCollection<Termin> _appointmentsForView;

        public static SekretarTerminiPage GetInstance()
        {
            if (_instance == null)
                _instance = new SekretarTerminiPage();
            return _instance;
        }

        public SekretarTerminiPage()
        {
            InitializeComponent();

            this.DataContext = this;
            _appointmentsForView = new ObservableCollection<Termin>();
            appointmentsTable.ItemsSource = _appointmentsForView;
            RefreshView();
        }

        public void RefreshView()
        {
            _appointmentsForView.Clear();
            ObservableCollection<Termin> appointments = new ObservableCollection<Termin>(TerminStorage.Instance.ReadList());
            GetPatientAndDoctorData(appointments);
            SortAppointments(appointments);
            foreach (Termin appointment in appointments)
            {
                _appointmentsForView.Add(appointment);
            }
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DodajPregledPage());
        }

        private void AddOperation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DodajOperacijuPage());
        }

        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedItem != null)
            {
                this.NavigationService.Navigate(new IzmeniTerminPage((Termin)appointmentsTable.SelectedItem));
            }

        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedItem != null)
            {

                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Termin toDelete = (Termin)appointmentsTable.SelectedItem;
                    TerminStorage.Instance.Delete(toDelete.TerminKey);
                    MessageBox.Show("Termin je uspešno otkazan!");
                    RefreshView();
                }

            }
        }

        private void AddUrgentExamination_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HitanPregledPage());
        }

        private void AddUrgentOperation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HitnaOperacijaPage());
        }

        public void RemoveInstance()
        {
            _instance = null;
        }

        private void GetPatientAndDoctorData(ObservableCollection<Termin> appointments)
        {
            foreach (Termin appointment in appointments)
            {
                appointment.Pacijent = PacijentStorage.Instance.Read(appointment.Pacijent.Jmbg);
                appointment.Lekar = LekarStorage.Instance.Read(appointment.Lekar.Jmbg);
            }
        }

        private void SortAppointments(ObservableCollection<Termin> appointments)
        {
            for (int i = 0; i < appointments.Count - 1; ++i)
            {
                for (int j = 0; j < appointments.Count - i - 1; ++j)
                {
                    if (appointments[j].PocetnoVreme > appointments[j + 1].PocetnoVreme)
                    {
                        Termin temp = appointments[j];
                        appointments[j] = appointments[j + 1];
                        appointments[j + 1] = temp;
                    }
                }
            }
        }
    }
}
