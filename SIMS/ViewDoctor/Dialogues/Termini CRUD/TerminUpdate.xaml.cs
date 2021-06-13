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
        private List<String> durationValues;
        private List<String> availableTimes;
        Appointment appointment;

        private DoctorController doctorController = new DoctorController();
        private AppointmentController appointmentController = new AppointmentController();
        private PatientController patientController = new PatientController();
        private RoomController roomController = new RoomController();

        private ExaminationScheduleController examinationScheduleController = new ExaminationScheduleController();
        private SurgeryScheduleController surgeryScheduleController = new SurgeryScheduleController();

        public AppointmentUpdate(Appointment appointment)
        {
            InitializeComponent();
            this.appointment = appointment;

            InitComboBoxes();

            SetValues();
        }

        private void InitComboBoxes()
        {
            if (appointment.Type == AppointmentType.examination)
            {
                doctors = doctorController.GetDTOFromList(examinationScheduleController.GetDoctorsForAppointment());
                rooms = examinationScheduleController.GetRoomsForAppointment();
                durationValues = examinationScheduleController.GetDurationList();
            } 
            else
            {
                doctors = doctorController.GetDTOFromList(surgeryScheduleController.GetDoctorsForAppointment());
                rooms = surgeryScheduleController.GetRoomsForAppointment();
                durationValues = surgeryScheduleController.GetDurationList();
            }
            
            patients = patientController.GetAllPatients();
            
            doctorCombo.ItemsSource = doctors;
            patientCombo.ItemsSource = patients;
            roomCombo.ItemsSource = rooms;
            durationValuesList.ItemsSource = durationValues;
        }

        private void ButtonAccept(object sender, RoutedEventArgs e)
        {
            DoUpdateAppointment();
        }

        private void DoUpdateAppointment()
        {
            //Izmena pregleda
            if (doctorCombo.SelectedItem == null || datePicker.SelectedDate == null || availableTimesList.SelectedItem == null)
                MessageBox.Show("Molimo popunite sva polja!");
            else
            {
                CreateAppointment();
                Doctor doctor = GetSelectedDoctor();

                if (!doctorController.CheckIfFreeUpdate(doctor, appointment))
                    MessageBox.Show("Odabrani lekar nije dostupan u datom terminu. Molimo izaberite drugi termin.", "Upozorenje!");

                else if (!appointment.Room.GetIfFreeForAppointmentUpdate(appointment))
                    MessageBox.Show("Odabrana soba nije dostupna u datom terminu.", "Upozorenje!");

                else
                {
                    SaveUpdatedAppointment();
                    this.Close();
                    MessageBox.Show("Termin uspešno izmenjen.");
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
            appointment.Duration = examinationScheduleController.GetDurationFromString((String)durationValuesList.SelectedValue);
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
            //datePicker.DisplayDate = appointment.StartTime;
            //datePicker.Text = appointment.StartTime.ToString("dd.MM.yyyy.");
            datePicker.SelectedDate = appointment.StartTime;
        }

        private void InitDuration()
        {
            int index = 0;

            foreach (String duration in durationValues)
            {
                int durationInt = examinationScheduleController.GetDurationFromString(duration);
                if (appointment.Duration == durationInt)
                    break;

                index++;
            }

            durationValuesList.SelectedIndex = index;
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
