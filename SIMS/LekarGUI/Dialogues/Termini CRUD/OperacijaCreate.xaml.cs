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

namespace SIMS.LekarGUI
{
    /// <summary>
    /// Interaction logic for TerminCreate.xaml
    /// </summary>
    public partial class OperacijaCreate : Window
    {
        private List<Doctor> doctors;
        private List<Patient> patients;
        private List<Room> rooms;
        private List<String> availableTimes;

        public OperacijaCreate(Patient patient)
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

        public OperacijaCreate()
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
            IDoctorRepository storageL = new DoctorFileRepository();
            doctors = storageL.GetAll();

            PatientFileRepository storageP = new PatientFileRepository();
            patients = storageP.GetAll();

            RoomFileRepository roomR = new RoomFileRepository();
            rooms = roomR.GetAll();

            doctorCombo.ItemsSource = doctors;
            patientCombo.ItemsSource = patients;
            roomCombo.ItemsSource = rooms;

            List<String> trajanjeVrednosti = new List<String>() { "30 minuta", "60 minuta", "90 minuta" };
            trajanjeLista.ItemsSource = trajanjeVrednosti;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            //TODO: Odraditi sve provere

            if (doctorCombo.SelectedItem == null || datePicker.SelectedDate == null || timeStringList.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                Appointment surgery = CreateSurgery();

                //PROVERA DOSTUPNOSTI LEKARA
                if (!doctors[doctorCombo.SelectedIndex].IsFree(surgery))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else
                {
                    SaveSurgery(surgery);
                    this.Close();
                }
            }

        }

        private static void SaveSurgery(Appointment surgery)
        {
            surgery.Doctor.Serialize = false;
            surgery.Patient.Serialize = false;
            surgery.Room.Serialize = false;

            AppointmentFileRepository.Instance.Save(surgery);
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
            surgery.Doctor = doctors[doctorCombo.SelectedIndex];
            surgery.Type = AppointmentType.surgery;
            return surgery;
        }

        private void SetSurgeryDuration(Appointment surgery)
        {
            if (trajanjeLista.SelectedIndex == 0)
                surgery.Duration = 30;
            else if (trajanjeLista.SelectedIndex == 1)
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

    }

}
