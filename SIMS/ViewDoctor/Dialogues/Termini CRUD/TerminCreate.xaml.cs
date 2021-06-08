using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class AppointmentCreate : Window
    {
        private List<DoctorDTO> doctors;
        private List<Patient> patients;
        private List<Room> rooms;
        private List<String> availableTimes;

        private DoctorController doctorController = new DoctorController();
        private AppointmentController appointmentController = new AppointmentController();
        private PatientController patientController = new PatientController();
        private RoomController roomController = new RoomController();

        public AppointmentCreate(Patient patient)
        {
            InitializeComponents();
            foreach (Patient currentPatient in patients)
            {
                if (patient.Jmbg == currentPatient.Jmbg)
                {
                    patientCombo.SelectedItem = currentPatient;
                    break;
                }
            }
        }

        public AppointmentCreate()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            InitializeComponent();
            InitComboBoxes();
            InitCurrentDoctorUser();
        }

        private void InitCurrentDoctorUser()
        {
            int index = 0;
            foreach (Doctor doctor in doctorController.GetAllDoctors())
            {
                if (doctor.Jmbg.Equals(DoctorUI.GetInstance().GetUser().Jmbg))
                    break;
                index++;
            }
            doctorCombo.SelectedIndex = index;
        }

        private void InitComboBoxes()
        {
            doctors = doctorController.GetDTOFromList(doctorController.GetAllDoctors());
            patients = patientController.GetAllPatients();
            rooms = roomController.GetAllRooms();

            doctorCombo.ItemsSource = doctors;
            patientCombo.ItemsSource = patients;
            roomCombo.ItemsSource = rooms;

            List<String> durationValues = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            durationValuesList.ItemsSource = durationValues;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            DoCreateAppointment();
        }

        private void DoCreateAppointment()
        {
            if (doctorCombo.SelectedItem == null || datePicker.SelectedDate == null || timePicker.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                Appointment appointment = new Appointment();
                CreateAppointment(appointment);
                Doctor doctor = GetSelectedDoctor();

                //PROVERA DOSTUPNOSTI LEKARA
                if (!doctorController.CheckIfFree(doctor, appointment))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                //PROVERA DOSTUPNOSTI SOBE
                else if (!appointment.Room.GetIfFreeForAppointment(appointment))
                    MessageBox.Show("Odabrana soba nije dostupna u datom terminu.", "Upozorenje!");

                else
                {
                    SaveAppointment(appointment);
                    this.Close();
                    MessageBox.Show("Termin uspešno zakazan.");
                }                
            }
        }

        private Doctor GetSelectedDoctor()
        {
            DoctorDTO dto = doctors[doctorCombo.SelectedIndex];
            return doctorController.GetDoctor(dto.Jmbg);
        }

        private void CreateAppointment(Appointment termin)
        {
            String vrijemeIDatum = datePicker.Text + " " + timePicker.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            termin.StartTime = vremenskaOdrednica;
            termin.InitialTime = vremenskaOdrednica;
            SetSelectedDuration(termin);
            termin.Room = rooms[roomCombo.SelectedIndex];
            termin.Patient = patients[patientCombo.SelectedIndex];
            termin.Doctor = doctors[doctorCombo.SelectedIndex];
            termin.Type = AppointmentType.examination;
        }

        private void SetSelectedDuration(Appointment termin)
        {
            if (durationValuesList.SelectedIndex == 0)
                termin.Duration = 30;
            else if (durationValuesList.SelectedIndex == 1)
                termin.Duration = 60;
            else
                termin.Duration = 90;
        }

        private void SaveAppointment(Appointment appointment)
        {
            appointmentController.SaveAppointment(appointment);
            DoctorAppointmentsPage.GetInstance().RefreshView();
        }

        //TODO
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doctorCombo.SelectedItem != null)
            {
                Doctor doctor = doctors[doctorCombo.SelectedIndex];
                List<Appointment> doctorTimes = new List<Appointment>();
                availableTimes = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };

                timePicker.ItemsSource = availableTimes;

            }
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Return)
                DoCreateAppointment();
        }
    }
}
