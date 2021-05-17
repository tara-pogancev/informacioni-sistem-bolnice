using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SIMS.SekretarGUI
{
    public partial class IzmeniTerminPage : Page
    {
        private List<Doctor> _doctors;
        private List<Patient> _patients;
        private List<Room> _rooms;
        private List<string> _freeAppointments;
        private Appointment _appointment;

        public IzmeniTerminPage(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;

            _doctors = DoctorRepository.Instance.ReadList();
            _patients = PatientRepository.Instance.ReadList();
            _rooms = new List<Room>(RoomRepository.Instance.ReadAll().Values);
            _freeAppointments = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };

            doctorsComboBox.ItemsSource = _doctors;
            patientsComboBox.ItemsSource = _patients;
            roomsComboBox.ItemsSource = _rooms;
            appointmentsComboBox.ItemsSource = _freeAppointments;

            List<string> duration = new List<string>() { "30 minuta", "60 minuta", "90 minuta" };
            durationComboBox.ItemsSource = duration;

            SetValuesForSelectedAppointment();
        }

        private void AddExamination_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem == null || datePicker.SelectedDate == null || appointmentsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return;
            }

            UpdateAppointmentFromUserInput();
            if (IsAppointmentValid())
            {
                AppointmentRepository.Instance.Update(_appointment);
                SekretarTerminiPage.GetInstance().RefreshView();

                NavigationService.Navigate(SekretarTerminiPage.GetInstance());
            }
        }

        private void UpdateAppointmentFromUserInput()
        {
            string dateAndTime = datePicker.Text + " " + appointmentsComboBox.Text;
            DateTime appointmentDateAndTime = DateTime.Parse(dateAndTime);
            _appointment.PocetnoVreme = appointmentDateAndTime;
            _appointment.InicijalnoVrijeme = _appointment.PocetnoVreme;

            if (durationComboBox.SelectedIndex == 0)
                _appointment.VremeTrajanja = 30;
            else if (durationComboBox.SelectedIndex == 1)
                _appointment.VremeTrajanja = 60;
            else
                _appointment.VremeTrajanja = 90;

            _appointment.Prostorija = _rooms[roomsComboBox.SelectedIndex];
            _appointment.Pacijent = _patients[patientsComboBox.SelectedIndex];
            _appointment.Lekar = _doctors[doctorsComboBox.SelectedIndex];
        }

        private bool IsAppointmentValid()
        {
            List<Appointment> appointments = AppointmentRepository.Instance.ReadList();
            foreach (Appointment a in appointments)
            {
                if (a.KrajnjeVreme > _appointment.PocetnoVreme && a.PocetnoVreme < _appointment.KrajnjeVreme && !a.TerminKey.Equals(_appointment.TerminKey))
                {
                    if (a.Lekar.Jmbg.Equals(_appointment.Lekar.Jmbg))
                    {
                        MessageBox.Show("Lekar je zauzet u navedenom terminu.", "Zauzet termin");
                        return false;
                    }
                    else if (a.NazivProstorije.Equals(_appointment.NazivProstorije))
                    {
                        MessageBox.Show("Prostorija je zauzeta u navedenom terminu.", "Zauzet termin");
                        return false;
                    }
                }
            }
            return true;
        }

        private void SetValuesForSelectedAppointment()
        {
            SetDatePickerValue();

            SetDurateionValue();

            SetDoctorValue();

            SetAppointmentValue();

            SetPatientValue();

            SetRoomValue();
        }

        private void SetDatePickerValue()
        {
            datePicker.DisplayDate = _appointment.PocetnoVreme;
            datePicker.Text = _appointment.PocetnoVreme.ToString("dd.MM.yyyy.");
        }

        private void SetDurateionValue()
        {
            if (_appointment.VremeTrajanja == 30)
                durationComboBox.SelectedIndex = 0;
            else if (_appointment.VremeTrajanja == 60)
                durationComboBox.SelectedIndex = 1;
            else
                durationComboBox.SelectedIndex = 2;
        }

        private void SetRoomValue()
        {
            int index = 0;
            foreach (Room r in _rooms)
            {
                if (r.Number.Equals(_appointment.Prostorija.Number))
                {
                    break;
                }
                index++;
            }
            roomsComboBox.SelectedIndex = index;
        }

        private void SetPatientValue()
        {
            int index = 0;
            foreach (Patient p in _patients)
            {
                if (p.Jmbg.Equals(_appointment.Pacijent.Jmbg))
                {
                    break;
                }
                index++;
            }
            patientsComboBox.SelectedIndex = index;
        }

        private void SetAppointmentValue()
        {
            int index = 0;
            foreach (String a in _freeAppointments)
            {
                if (a.Equals(_appointment.Vrijeme))
                {
                    break;
                }
                index++;
            }
            appointmentsComboBox.SelectedIndex = index;
        }

        private void SetDoctorValue()
        {
            int index = 0;
            foreach (Doctor d in _doctors)
            {
                if (d.Jmbg.Equals(_appointment.Lekar.Jmbg))
                {
                    break;
                }
                index++;
            }
            doctorsComboBox.SelectedIndex = index;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(SekretarTerminiPage.GetInstance());
        }
    }
}
