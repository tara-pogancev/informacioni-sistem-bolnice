using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.Controller;
using SIMS.DTO;
using System.Windows.Input;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class AppointmentUpdate : Window
    {
        private List<DoctorDTO> doctors;
        private List<Patient> patients;
        private List<Room> rooms;
        private List<String> availableTimes;
        Appointment appointment;

        private DoctorController doctorController = new DoctorController();
        private AppointmentController appointmentController = new AppointmentController();
        private PatientController patientController = new PatientController();
        private RoomController roomController = new RoomController();

        public AppointmentUpdate(Appointment appointment)
        {
            InitializeComponent();
            this.appointment = appointment;

            InitComboBoxes();

            SetValues();
        }

        private void InitComboBoxes()
        {
            doctors = doctorController.GetDTOFromList(doctorController.GetAllDoctors());
            patients = patientController.GetAllPatients();
            rooms = roomController.GetAllRooms();

            List<String> durationValues = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            durationValuesList.ItemsSource = durationValues;

            doctorCombo.ItemsSource = doctors;
            patientCombo.ItemsSource = patients;
            roomCombo.ItemsSource = rooms;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            DoUpdateAppointment();
        }

        private void DoUpdateAppointment()
        {
            //Izmena pregleda
            //TODO: Odraditi sve provere

            if (doctorCombo.SelectedItem == null || datePicker.SelectedDate == null || availableTimesList.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                CreateAppointment();
                Doctor doctor = GetSelectedDoctor();

                if (!doctorController.CheckIfFreeUpdate(doctor, appointment))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else
                {
                    SaveUpdatedAppointment();
                    this.Close();
                }

            }
        }

        private Doctor GetSelectedDoctor()
        {
            DoctorDTO dto = doctors[doctorCombo.SelectedIndex];
            return doctorController.GetDoctor(dto.Jmbg);
        }

        private void SaveUpdatedAppointment()
        {
            appointmentController.UpdateAppointment(appointment);
            DoctorAppointmentsPage.GetInstance().RefreshView();
        }

        private void CreateAppointment()
        {
            String dateAndTime = datePicker.Text + " " + availableTimesList.Text;
            DateTime appointmenTtime = DateTime.Parse(dateAndTime);
            appointment.StartTime = appointmenTtime;
            SetSelectedDuration();
            appointment.Room = rooms[roomCombo.SelectedIndex];
            appointment.Patient = patients[patientCombo.SelectedIndex];
            appointment.Doctor = GetSelectedDoctor();
        }

        private void SetSelectedDuration()
        {
            if (durationValuesList.SelectedIndex == 0)
                appointment.Duration = 30;
            else if (durationValuesList.SelectedIndex == 1)
                appointment.Duration = 60;
            else
                appointment.Duration = 90;
        }

        private void SetValues()
        {
            availableTimes = new List<String>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            availableTimesList.ItemsSource = availableTimes;

            InitStartTime();
            InitDuration();
            InitDoctor();
            InitTime();
            InitPatient();
            InitRoom();

        }

        private void InitStartTime()
        {
            datePicker.DisplayDate = appointment.StartTime;
            datePicker.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
        }

        private void InitDuration()
        {
            if (appointment.Duration == 30)
                durationValuesList.SelectedIndex = 0;
            else if (appointment.Duration == 60)
                durationValuesList.SelectedIndex = 1;
            else
                durationValuesList.SelectedIndex = 2;
        }

        private void InitDoctor()
        {
            int index = 0;
            foreach (Doctor l in doctors)
            {
                if (l.Jmbg.Equals(appointment.Doctor.Jmbg))
                {
                    break;
                }
                index++;
            }
            doctorCombo.SelectedIndex = index;
        }

        private void InitTime()
        {
            int index = 0;
            foreach (String str in availableTimes)
            {
                if (str.Equals(appointment.GetAppointmentTime()))
                {
                    break;
                }
                index++;
            }
            availableTimesList.SelectedIndex = index;
        }

        private void InitPatient()
        {
            int index = 0;
            foreach (Patient p in patients)
            {
                if (p.Jmbg.Equals(appointment.Patient.Jmbg))
                {
                    break;
                }
                index++;
            }
            patientCombo.SelectedIndex = index;
        }

        private void InitRoom()
        {
            int index = 0;
            foreach (Room pr in rooms)
            {
                if (pr.Number.Equals(appointment.Room.Number))
                {
                    break;
                }
                index++;
            }
            roomCombo.SelectedIndex = index;
        }

        //TODO
        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WindowKeyListener(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
            else if (e.Key == Key.Return)
                DoUpdateAppointment();
        }

    }


}
