using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class AppointmentUpdate : Window
    {
        private List<Doctor> doctors;
        private List<Patient> patients;
        private List<Room> rooms;
        private List<String> availableTimes;
        Appointment appointment;

        public AppointmentUpdate(Appointment appointment)
        {
            InitializeComponent();
            this.appointment = appointment;

            InitComboBoxes();

            SetValues();
        }

        private void InitComboBoxes()
        {
            IDoctorRepository storageL = new DoctorFileRepository();
            doctors = storageL.GetAll();

            PatientFileRepository storageP = new PatientFileRepository();
            patients = storageP.GetAll();

            RoomFileRepository roomR = new RoomFileRepository();
            rooms = roomR.GetAll();

            List<String> trajanjeVrednosti = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            trajanjeLista.ItemsSource = trajanjeVrednosti;

            doctorCombo.ItemsSource = doctors;
            patientCombo.ItemsSource = patients;
            roomCombo.ItemsSource = rooms;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            //Izmena pregleda
            //TODO: Odraditi sve provere

            if (doctorCombo.SelectedItem == null || datePicker.SelectedDate == null || availableTimesList.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                CreateAppointment();

                if (!doctors[doctorCombo.SelectedIndex].IsFreeUpdate(appointment))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else
                {
                    SaveUpdatedAppointment();
                    this.Close();
                }

            }
        }

        private void SaveUpdatedAppointment()
        {
            appointment.Doctor.Serialize = false;
            appointment.Patient.Serialize = false;
            appointment.Room.Serialize = false;

            AppointmentFileRepository.Instance.Update(appointment);
            DoctorAppointmentsPage.GetInstance().RefreshView();
        }

        private void CreateAppointment()
        {
            String vrijemeIDatum = datePicker.Text + " " + availableTimesList.Text;
            DateTime vremenskaOdrednica = DateTime.Parse(vrijemeIDatum);
            appointment.StartTime = vremenskaOdrednica;
            SetSelectedDuration();
            appointment.Room = rooms[roomCombo.SelectedIndex];
            appointment.Patient = patients[patientCombo.SelectedIndex];
            appointment.Doctor = doctors[doctorCombo.SelectedIndex];
        }

        private void SetSelectedDuration()
        {
            if (trajanjeLista.SelectedIndex == 0)
                appointment.Duration = 30;
            else if (trajanjeLista.SelectedIndex == 1)
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
                trajanjeLista.SelectedIndex = 0;
            else if (appointment.Duration == 60)
                trajanjeLista.SelectedIndex = 1;
            else
                trajanjeLista.SelectedIndex = 2;
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
                if (str.Equals(appointment.AppointmentTime))
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

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }


}
