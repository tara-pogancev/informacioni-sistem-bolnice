using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.ViewSecretary.Appointments
{
    public partial class ViewAppointments : Page
    {
        private static ViewAppointments _instance = null;

        private ObservableCollection<AppointmentDTO> _appointmentsForView;
        private AppointmentController appointmentController = new AppointmentController();
        private DoctorController doctorController = new DoctorController();
        private PatientController patientController = new PatientController();

        public static ViewAppointments GetInstance()
        {
            if (_instance == null)
                _instance = new ViewAppointments();
            return _instance;
        }

        public ViewAppointments()
        {
            InitializeComponent();

            this.DataContext = this;
            _appointmentsForView = new ObservableCollection<AppointmentDTO>();
            appointmentsTable.ItemsSource = _appointmentsForView;
            RefreshView();
        }

        public void RefreshView()
        {
            _appointmentsForView.Clear();
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>(appointmentController.GetAllAppointments());
            GetPatientAndDoctorData(appointments);
            SortAppointments(appointments);
            foreach (Appointment appointment in appointments)
            {
                _appointmentsForView.Add(new AppointmentDTO(appointment));
            }
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddExamination());
        }

        private void AddOperation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddOperation());
        }

        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedItem != null)
            {
                this.NavigationService.Navigate(new UpdateAppointment((Appointment)appointmentsTable.SelectedItem));
            }

        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsTable.SelectedItem != null)
            {

                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Appointment toDelete = (Appointment)appointmentsTable.SelectedItem;
                    appointmentController.DeleteAppointment(toDelete);

                    MessageBox.Show("Termin je uspešno otkazan!");
                    RefreshView();
                }

            }
        }

        private void AddUrgentExamination_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddUrgentExamination());
        }

        private void AddUrgentOperation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddUrgentOperation());
        }

        public void RemoveInstance()
        {
            _instance = null;
        }

        private void GetPatientAndDoctorData(ObservableCollection<Appointment> appointments)
        {
            foreach (Appointment appointment in appointments)
            {
                appointment.Patient = patientController.GetPatient(appointment.Patient.Jmbg);
                appointment.Doctor = doctorController.GetDoctor(appointment.Doctor.Jmbg);
            }
        }

        private void SortAppointments(ObservableCollection<Appointment> appointments)
        {
            for (int i = 0; i < appointments.Count - 1; ++i)
            {
                for (int j = 0; j < appointments.Count - i - 1; ++j)
                {
                    if (appointments[j].StartTime > appointments[j + 1].StartTime)
                    {
                        Appointment temp = appointments[j];
                        appointments[j] = appointments[j + 1];
                        appointments[j + 1] = temp;
                    }
                }
            }
        }
    }
}
