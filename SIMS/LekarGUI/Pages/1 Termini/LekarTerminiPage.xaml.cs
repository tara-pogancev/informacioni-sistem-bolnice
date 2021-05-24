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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Stranica Lekara u okviru glavnog prozora koja daje u uvid sve aktivne termine uz mogucnost editovanja istih
    /// </summary>
    public partial class DoctorAppointmentsPage : Page
    {
        public static DoctorAppointmentsPage instance;

        private static Doctor doctorUser;

        public ObservableCollection<Appointment> AppointmentsViewModel { get; set; }

        public static DoctorAppointmentsPage GetInstance(Doctor doctor)
        {
            if (instance == null)
            {
                doctorUser = doctor;
                instance = new DoctorAppointmentsPage();
            }
            return instance;
        }

        public static DoctorAppointmentsPage GetInstance()
        {
            return instance;
        }

        public DoctorAppointmentsPage()
        {
            InitializeComponent();

            this.DataContext = this;
            AppointmentsViewModel = new ObservableCollection<Appointment>(AppointmentFileRepository.Instance.GetAll());
            RefreshView();
        }

        public void RefreshView()
        {
            AppointmentsViewModel.Clear();
            List<Appointment> temp = new List<Appointment>(AppointmentFileRepository.Instance.GetDoctorAppointments(doctorUser));
            
            foreach (Appointment t in temp)
            {
                if (!t.IsPast() && !t.GetIfRecorded())
                    AppointmentsViewModel.Add(t);

                t.Patient = new PatientFileRepository().FindById(t.Patient.Jmbg);
                t.Room = new RoomFileRepository().FindById(t.Room.Number);
            }
        }

        private void ButtonAppointment(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi pregled
            AppointmentCreate appointmentCreate = new AppointmentCreate();
            appointmentCreate.ShowDialog();
        }

        private void ButtonSurgery(object sender, RoutedEventArgs e)
        {
            //Button: Zakazi operaciju
            SurgeryCreate surgeryCreate = new SurgeryCreate();
            surgeryCreate.ShowDialog();
        }

        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            //Button: Uredi termin
            if (dataGridAppointments.SelectedItem != null)
            {
                AppointmentUpdate appointmentUpdate = new AppointmentUpdate((Appointment)dataGridAppointments.SelectedItem);
                appointmentUpdate.ShowDialog();
            }
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            //Button: Otkaži pregled

            if (dataGridAppointments.SelectedItem != null)
            {

                if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                "Otkaži termin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Appointment appointmentToDelete = (Appointment)dataGridAppointments.SelectedItem;
                    AppointmentFileRepository.Instance.Delete(appointmentToDelete.AppointmentID);
                    RefreshView();
                    MessageBox.Show("Termin je uspešno otkazan!");
                }

            }
        }

        public void AddAppointment(Appointment termin)
        {
            AppointmentFileRepository.Instance.Save(termin);
            RefreshView();
            MessageBox.Show("Termin uspešno zakazan.");
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
            if (dataGridAppointments.SelectedItem != null)
            {
                Appointment sellectedAppointment = (Appointment)dataGridAppointments.SelectedItem;
                Patient sellectedPatient = PatientFileRepository.Instance.FindById(sellectedAppointment.Patient.Jmbg);

                DoctorUI.GetInstance().SellectedTab.Content = PacijentKartonView.GetInstance(sellectedPatient);
            }
        }
    }
}
