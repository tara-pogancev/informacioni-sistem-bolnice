using SIMS.Repositories.SecretaryRepo;
using SIMS.LekarGUI;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class SurgeryCreate : Window
    {
        private List<DoctorDTO> doctors;
        private List<Patient> patients;
        private List<Room> rooms;
        private List<String> availableTimes;

        private DoctorController doctorController = new DoctorController();
        private AppointmentController appointmentController = new AppointmentController();
        private PatientController patientController = new PatientController();
        private RoomController roomController = new RoomController();

        public SurgeryCreate(Patient patient)
        {
            InitializeComponents();

            foreach(Patient currentPatient in patients)
            {
                if (patient.Jmbg == currentPatient.Jmbg)
                {
                    patientCombo.SelectedItem = currentPatient;
                    break;
                }
            }
        }

        public SurgeryCreate()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            InitializeComponent();

            InitComboBoxes();

            InitDoctor();
        }

        private void InitDoctor()
        {
            int index = 0;
            foreach (Doctor doctor in doctorController.GetAllDoctors())
            {
                if (doctor.Jmbg.Equals(DoctorUI.GetInstance().GetUser().Jmbg))
                {
                    break;
                }
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
            DoCreateSurgery();

        }

        private void DoCreateSurgery()
        {
            //TODO: Odraditi sve provere

            if (doctorCombo.SelectedItem == null || datePicker.SelectedDate == null || timeStringList.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                Appointment surgery = CreateSurgery();
                Doctor doctor = GetSelectedDoctor();

                //PROVERA DOSTUPNOSTI LEKARA
                if (!doctorController.CheckIfFree(doctor, surgery))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else
                {
                    SaveSurgery(surgery);
                    this.Close();
                }
            }
        }

        private Doctor GetSelectedDoctor()
        {
            DoctorDTO dto = doctors[doctorCombo.SelectedIndex];
            return doctorController.GetDoctor(dto.Jmbg);
        }

        private void SaveSurgery(Appointment surgery)
        {
            appointmentController.SaveAppointment(surgery);
            DoctorAppointmentsPage.GetInstance().RefreshView();
        }

        private Appointment CreateSurgery()
        {
            Appointment surgery = new Appointment();
            DateTime time = GetSurgeryTime();
            surgery.StartTime = time;
            surgery.InitialTime = time;
            SetSurgeryDuration(surgery);
            surgery.Room = rooms[roomCombo.SelectedIndex];
            surgery.Patient = patients[patientCombo.SelectedIndex];
            surgery.Doctor = GetSelectedDoctor();
            surgery.Type = AppointmentType.surgery;
            return surgery;
        }

        private void SetSurgeryDuration(Appointment surgery)
        {
            if (durationValuesList.SelectedIndex == 0)
                surgery.Duration = 30;
            else if (durationValuesList.SelectedIndex == 1)
                surgery.Duration = 60;
            else
                surgery.Duration = 90;
        }

        private DateTime GetSurgeryTime()
        {
            String dateAndTime = datePicker.Text + " " + timeStringList.Text;
            DateTime time = DateTime.Parse(dateAndTime);
            return time;
        }


        //TODO
        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doctorCombo.SelectedItem != null)
            {
                Doctor doctor = doctors[doctorCombo.SelectedIndex];
                List<Appointment> doctorAppointments = new List<Appointment>();
                availableTimes = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                timeStringList.ItemsSource = availableTimes;

            }
        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Return)
                DoCreateSurgery();
        }

    }

}
