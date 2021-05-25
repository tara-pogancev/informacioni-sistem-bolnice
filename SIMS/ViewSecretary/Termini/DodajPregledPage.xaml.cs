using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;

namespace SIMS.SekretarGUI
{
    public partial class DodajPregledPage : Page
    {
        private List<Doctor> _doctors;
        private List<Patient> _patients;
        private List<Room> _rooms;

        public DodajPregledPage()
        {
            InitializeComponent();

            _doctors = DoctorFileRepository.Instance.GetAll();
            _patients = PatientFileRepository.Instance.GetAll();

            _rooms = new List<Room>(RoomFileRepository.Instance.ReadAll().Values);

            doctorsComboBox.ItemsSource = _doctors;
            patientsComboBox.ItemsSource = _patients;
            roomsComboBox.ItemsSource = _rooms;

            List<string> duration = new List<string>() { "30 minuta", "60 minuta", "90 minuta" };
            durationComboBox.ItemsSource = duration;
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem == null || datePicker.SelectedDate == null || appointmentsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return;
            }

            Appointment appointment = CreateAppointmentFromUserInput();
            if (IsAppointmentValid(appointment))
            {
                AppointmentFileRepository.Instance.Save(appointment);
                SekretarTerminiPage.GetInstance().RefreshView();

                NavigationService.Navigate(SekretarTerminiPage.GetInstance());
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
            appointment.Type = AppointmentType.examination;

            return appointment;
        }

        private bool IsAppointmentValid(Appointment appointment)
        {
            List<Appointment> appointments = AppointmentFileRepository.Instance.GetAll();
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
            NavigationService.Navigate(SekretarTerminiPage.GetInstance());
        }
    }
}
