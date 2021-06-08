using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
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
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica Lekara u okviru glavnog prozora koja daje u uvid sve aktivne termine uz mogucnost editovanja istih
    /// </summary>
    public partial class DoctorAppointmentsPage : Page
    {
        public static DoctorAppointmentsPage instance;

        private static Doctor doctorUser;

        private AppointmentController appointmentController = new AppointmentController();
        private PatientController patientController = new PatientController();
        private DoctorAppointmentController doctorAppointmentController = new DoctorAppointmentController();

        public ObservableCollection<AppointmentDTO> AppointmentsViewModel { get; set; }

        public static DoctorAppointmentsPage GetInstance()
        {
            if (instance == null)
            {
                doctorUser = DoctorUI.GetInstance().GetUser();
                instance = new DoctorAppointmentsPage();
            }
            return instance;
        }

        public DoctorAppointmentsPage()
        {
            InitializeComponent();

            this.DataContext = this;

            AppointmentsViewModel = new ObservableCollection<AppointmentDTO>();
            RefreshView();
        }

        public void RefreshView()
        {
            List<Appointment> allAppointments = doctorAppointmentController.GetFutureAppointmentsByDoctor(doctorUser);
            AppointmentsViewModel.Clear();
            foreach (AppointmentDTO dto in doctorAppointmentController.GetDTOFromList(allAppointments))
                AppointmentsViewModel.Add(dto);
        }

        private void ButtonAppointment(object sender, RoutedEventArgs e)
        {
            new AppointmentCreate().ShowDialog();
        }

        private void ButtonSurgery(object sender, RoutedEventArgs e)
        {
            new SurgeryCreate().ShowDialog();
        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            if (dataGridAppointments.SelectedItem != null)
            {
                Appointment appointmentToEdit = GetSellectedAppointment();
                new AppointmentUpdate(appointmentToEdit).ShowDialog();
            }
        }

        private Appointment GetSellectedAppointment()
        {
            AppointmentDTO sellectedDTO = (AppointmentDTO)dataGridAppointments.SelectedItem;
            return appointmentController.GetAppointment(sellectedDTO.AppointmentID);
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            if (dataGridAppointments.SelectedItem != null)
            {
                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Appointment appointmentToDelete = GetSellectedAppointment();
                    appointmentController.DeleteAppointment(appointmentToDelete);
                    RefreshView();
                    MessageBox.Show("Termin je uspešno otkazan!");
                }
            }
        }

        public void RemoveInstance()
        {
            instance = null;
        }

        private void ButtonHome(object sender, MouseButtonEventArgs e)
        {
            DoctorUI.GetInstance().ChangeTab(0);
        }

        private void EventPatientRecord(object sender, MouseButtonEventArgs e)
        {
            DoShowPatientRecord();
        }

        private void DoShowPatientRecord()
        {
            if (dataGridAppointments.SelectedItem != null)
            {
                Appointment sellectedAppointment = GetSellectedAppointment();
                Patient sellectedPatient = patientController.GetPatient(sellectedAppointment.Patient.Jmbg);

                DoctorUI.GetInstance().SellectedTab.Content = new PatientRecordCheck(sellectedPatient);
            }
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                DoShowPatientRecord();

        }
    }
}
