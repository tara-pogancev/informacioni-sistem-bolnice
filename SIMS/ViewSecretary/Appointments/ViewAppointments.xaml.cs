using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;
using System.Windows.Input;
using System.Collections.Generic;
using SIMS.Sort;

namespace SIMS.ViewSecretary.Appointments
{
    public partial class ViewAppointments : Page
    {
        private static ViewAppointments _instance = null;

        public ObservableCollection<AppointmentDTO> _appointmentsForView { get; }
        private AppointmentController appointmentController = new AppointmentController();
        private DoctorController doctorController = new DoctorController();
        private PatientController patientController = new PatientController();

        private readonly ISortAppointments sortAppointmentsDescendingService;
        private readonly ISortAppointments sortAppointmentsController;
             
        public static ViewAppointments GetInstance()
        {
            if (_instance == null)
                _instance = new ViewAppointments();
            return _instance;
        }

        public ViewAppointments()
        {
            InitializeComponent();

            sortAppointmentsDescendingService = new SortAppointmentsDescendingService();
            sortAppointmentsController = new SortAppointmentsController(sortAppointmentsDescendingService);

            this.DataContext = this;
            _appointmentsForView = new ObservableCollection<AppointmentDTO>();
            appointmentsView.ItemsSource = _appointmentsForView;
            RefreshView();
        }

        public void RefreshView()
        {
            _appointmentsForView.Clear();
            List<Appointment> appointmentsList = appointmentController.GetAllAppointments();
            GetPatientAndDoctorData(appointmentsList);
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            foreach(Appointment a in appointmentsList)
            {
                appointmentDTOs.Add(new AppointmentDTO(a));
            }
            sortAppointmentsController.SortAppointments(appointmentDTOs);
            ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
            foreach (Appointment a in appointmentDTOs)
            {
                appointments.Add(a);
            }
            foreach (Appointment appointment in appointments)
            {
                _appointmentsForView.Add(new AppointmentDTO(appointment));
            }
            appointmentsView.ItemsSource = _appointmentsForView;
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddExamination());
        }

        private void AddOperation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddSurgery());
        }

        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsView.SelectedItem != null)
            {
                this.NavigationService.Navigate(new UpdateAppointment((Appointment)appointmentsView.SelectedItem));
            }
            else
            {
                CustomMessageBox.Show(TranslationSource.Instance["ChooseAppointmentToUpdateMessage"]);
            }

        }

        private void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentsView.SelectedItem != null)
            {
                Appointment toDelete = (Appointment)appointmentsView.SelectedItem;
                appointmentController.DeleteAppointment(toDelete);

                CustomMessageBox.Show(TranslationSource.Instance["AppointmentCanceledMessage"]);
                RefreshView();
            }
            else
            {
                CustomMessageBox.Show(TranslationSource.Instance["ChooseAppointmentToCancelMessage"]);
            }
        }

        private void AddUrgentExamination_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddUrgentExamination());
        }

        private void AddUrgentOperation_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddUrgentSurgery());
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Appointment details = (Appointment)appointmentsView.SelectedItem;
            NavigationService.Navigate(new AppointmentDetails(details));

        }

        public void RemoveInstance()
        {
            _instance = null;
        }

        private void GetPatientAndDoctorData(List<Appointment> appointments)
        {
            foreach (Appointment appointment in appointments)
            {
                appointment.Patient = patientController.GetPatient(appointment.Patient.Jmbg);
                appointment.Doctor = doctorController.GetDoctor(appointment.Doctor.Jmbg);
            }
        }

        /*private void SortAppointments(List<AppointmentDTO> appointments)
        {
            for (int i = 0; i < appointments.Count - 1; ++i)
            {
                for (int j = 0; j < appointments.Count - i - 1; ++j)
                {
                    if (appointments[j].StartTime > appointments[j + 1].StartTime)
                    {
                        var temp = appointments[j];
                        appointments[j] = appointments[j + 1];
                        appointments[j + 1] = temp;
                    }
                }
            }
        }*/
    }
}
