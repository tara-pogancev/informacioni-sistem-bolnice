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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class AppointmentCreate : Window
    {
        private List<Doctor> doctors;
        private List<Patient> patients;
        private List<Room> rooms;
        private List<String> availableTimes;

        private DoctorController doctorController = new DoctorController();

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
            foreach (Doctor l in doctors)
            {
                if (l.Jmbg.Equals(DoctorUI.GetInstance().GetUser().Jmbg))
                {
                    break;
                }
                index++;
            }

            doctorCombo.SelectedIndex = index;
        }

        private void InitComboBoxes()
        {
            DoctorFileRepository storageL = new DoctorFileRepository();
            doctors = storageL.GetAll();

            PatientFileRepository storageP = new PatientFileRepository();
            patients = storageP.GetAll();

            RoomFileRepository roomR = new RoomFileRepository();
            rooms = roomR.GetAll();

            doctorCombo.ItemsSource = doctors;
            patientCombo.ItemsSource = patients;
            roomCombo.ItemsSource = rooms;

            List<String> durationValues = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            durationValuesList.ItemsSource = durationValues;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            //TODO: Odraditi sve provere

            if (doctorCombo.SelectedItem == null || datePicker.SelectedDate == null || timePicker.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                Appointment appointment = new Appointment();
                CreateAppointment(appointment);
                Doctor doctor = doctors[doctorCombo.SelectedIndex];

                //PROVERA DOSTUPNOSTI LEKARA
                if (!doctorController.CheckIfFree(doctor, appointment))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else
                {
                    SaveAppointment(appointment);
                    this.Close();
                }

            }
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

        private static void SaveAppointment(Appointment termin)
        {
            termin.Doctor.Serialize = false;
            termin.Patient.Serialize = false;
            termin.Room.Serialize = false;

            AppointmentFileRepository.Instance.Save(termin);
            DoctorAppointmentsPage.GetInstance().RefreshView();
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doctorCombo.SelectedItem != null)
            {
                Doctor doctor = doctors[doctorCombo.SelectedIndex];
                List<Appointment> doktoroviTermini = new List<Appointment>();
                availableTimes = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };

                timePicker.ItemsSource = availableTimes;

            }
        }
    }
}
