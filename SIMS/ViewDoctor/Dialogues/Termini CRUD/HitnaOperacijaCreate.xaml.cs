using SIMS.Repositories.SecretaryRepo;
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
using System.Windows.Shapes;
using SIMS.Model;
using SIMS.Controller;
using SIMS.DTO;

namespace SIMS.LekarGUI.Dialogues.Termini_CRUD
{
    /// <summary>
    /// Interaction logic for HitnaOperacijaCreate.xaml
    /// </summary>
    public partial class UrgentSurgeryCreate : Window
    {
        private List<String> specializationList;
        private List<Specialization> specializationEnumList;
        private List<String> durationList = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
        private ObservableCollection<Appointment> AvailableAppointments { get; set; }
        private Patient patient;

        private DoctorController doctorController = new DoctorController();
        private PatientController patientController = new PatientController();
        private AppointmentController appointmentController = new AppointmentController();
        private DoctorAppointmentController doctorAppointmentController = new DoctorAppointmentController();
        private NotificationController notificationController = new NotificationController();

        public UrgentSurgeryCreate(Patient patientPar)
        {
            InitializeComponent();
            patient = patientController.GetPatient(patientPar.Jmbg);

            DataContext = this;
            DurationComboBox.SelectedIndex = 0;

            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            AvailableAppointments = new ObservableCollection<Appointment>();
            AvailableComboBox.DataContext = AvailableAppointments;

            specializationList = doctorController.GetAvailableSpecializationString();
            specializationList.Remove("Lekar opšte prakse");
            specializationEnumList = doctorController.GetAvailableSpecialization();
            specializationEnumList.Remove(Specialization.OpstaPraksa);

            SpecializationComboBox.ItemsSource = specializationList;
            DurationComboBox.ItemsSource = durationList;
            AvailableComboBox.ItemsSource = AvailableAppointments;
        }

        private Specialization GetSelectedSpecialization()
        {
            int idx = SpecializationComboBox.SelectedIndex;

            if (idx == -1)
                return specializationEnumList[0];
            else 
                return specializationEnumList[idx];
        }

        private int GetSelectedDuration()
        {
            int idx = DurationComboBox.SelectedIndex;

            if (idx == -1)
                return 0;
            else return (idx + 1) * 30;

        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                Appointment selecetdAppointment = GetSelecetdAppointment();
                selecetdAppointment.InitData();

                appointmentController.SaveAppointment(selecetdAppointment);

                SendNotification(selecetdAppointment);
                DoctorAppointmentsPage.GetInstance().RefreshView();

                this.Close();
                MessageBox.Show("Hitna operacija uspešno zakazana!");
            }
        }

        private Appointment GetSelecetdAppointment()
        {
            return (Appointment)AvailableComboBox.SelectedItem;
        }

        private bool ValidateForm()
        {
            return DurationComboBox.SelectedItem != null && SpecializationComboBox.SelectedItem != null;
        }

        private void SendNotification(Appointment appointment)
        {
            String author = DoctorUI.GetInstance().GetUser().FullName;
            List<String> target = new List<String>();
            target.Add(patient.Jmbg);
            target.Add(appointment.Doctor.Jmbg);
                        
            Notification notification = new Notification(author, DateTime.Now,
                ("Zakazana hitna operacija [" + appointment.GetAppointmentDate() + " " + appointment.GetAppointmentTime() + ", " + appointment.Room.Number + "] za pacijenta " 
                + appointment.GetPatientName() + ", vodeći lekar " + appointment.GetDoctorName() + "."), target);

            notificationController.SaveNotification(notification);
        }

        private void DurationChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }

        private void SpecializationChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }

        private void GetAvailableAppointments()
        {
            AvailableAppointments.Clear();

            if (ValidateForm())
            {
                List<Appointment> allAppointments = 
                    doctorAppointmentController.GetAvailableAppointmentsForAllDoctors(GetSelectedSpecialization(), GetSelectedDuration(), patient);
                allAppointments = doctorAppointmentController.SortAppointmentsByTimeA(allAppointments);

                foreach (Appointment app in allAppointments)
                {
                    AvailableAppointments.Add(app);
                    if (AvailableAppointments.Count >= 5)
                        break;
                }
            }

            AvailableComboBox.ItemsSource = AvailableAppointments;
        }

    }
}
