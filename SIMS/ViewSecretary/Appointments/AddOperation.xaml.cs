using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.ViewSecretary.Appointments
{
    public partial class AddOperation : Page
    {
        private List<DoctorDTO> _doctors;
        private List<Patient> _patients;
        private List<Room> _rooms;

        private DoctorController doctorController = new DoctorController();

        public AddOperation()
        {
            InitializeComponent();

            PatientController patientController = new PatientController();
            RoomController roomController = new RoomController();

            _doctors = doctorController.GetAllDoctorsDTO();
            _patients = patientController.GetAllPatients();
            _rooms = roomController.GetAllRooms();

            doctorsComboBox.ItemsSource = _doctors;
            patientsComboBox.ItemsSource = _patients;
            roomsComboBox.ItemsSource = _rooms;

            List<string> duration = new List<string>() { "30 minuta", "60 minuta", "90 minuta" };
            durationComboBox.ItemsSource = duration;
        }

        private void AddOperation_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem == null || datePicker.SelectedDate == null || appointmentsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return;
            }

            Appointment appointment = CreateAppointmentFromUserInput();
            if (IsAppointmentValid(appointment))
            {
                AppointmentController appointmentController = new AppointmentController();
                appointmentController.SaveAppointment(appointment);
                ViewAppointments.GetInstance().RefreshView();

                NavigationService.Navigate(ViewAppointments.GetInstance());
            }
        }

        private Appointment CreateAppointmentFromUserInput()
        {
            Appointment appointment = new Appointment();
            string dateAndTime = datePicker.Text + " " + appointmentsComboBox.Text;
            DateTime appointmentDateAndTime = DateTime.Parse(dateAndTime);
            appointment.StartTime = appointmentDateAndTime;
            appointment.InitialTime = appointment.StartTime;

            if (durationComboBox.SelectedIndex == 0)
                appointment.Duration = 30;
            else if (durationComboBox.SelectedIndex == 1)
                appointment.Duration = 60;
            else
                appointment.Duration = 90;

            appointment.Room = _rooms[roomsComboBox.SelectedIndex];
            appointment.Patient = _patients[patientsComboBox.SelectedIndex];
            appointment.Doctor = _doctors[doctorsComboBox.SelectedIndex];
            appointment.Type = AppointmentType.surgery;

            return appointment;
        }

        private bool IsAppointmentValid(Appointment appointment)
        {
            AppointmentController appointmentController = new AppointmentController();
            List<Appointment> appointments = appointmentController.GetAllAppointments();
            if (doctorController.OnVacation(appointment.Doctor, appointment.StartTime))
            {
                MessageBox.Show("Lekar je na odmoru u navedenom terminu.", "Lekar na odmoru");
                return false;
            }
            foreach (Appointment a in appointments)
            {
                if (a.GetEndTime() > appointment.StartTime && a.StartTime < appointment.GetEndTime())
                {
                    if (a.Doctor.Jmbg.Equals(appointment.Doctor.Jmbg))
                    {
                        MessageBox.Show("Lekar je zauzet u navedenom terminu.", "Zauzet termin");
                        return false;
                    }
                    else if (a.Room.Number.Equals(appointment.Room.Number))
                    {
                        MessageBox.Show("Prostorija je zauzeta u navedenom terminu.", "Zauzet termin");
                        return false;
                    }
                }
            }
            return true;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem != null)
            {
                List<string> freeAppointments = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
                appointmentsComboBox.ItemsSource = freeAppointments;
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewAppointments.GetInstance());
        }
    }
}
