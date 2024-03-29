﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIMS.Model;
using SIMS.DTO;
using SIMS.Controller;

namespace SIMS.ViewSecretary.Appointments
{
    public partial class UpdateAppointment : Page
    {
        private List<DoctorDTO> _doctors; 
        private List<Patient> _patients;
        private List<Room> _rooms;
        private List<string> _freeAppointments;
        private Appointment _appointment;

        private DoctorController doctorController = new DoctorController();

        public UpdateAppointment(Appointment appointment)
        {
            InitializeComponent();
            _appointment = appointment;

            PatientController patientController = new PatientController();
            RoomController roomController = new RoomController();

            _doctors = doctorController.GetAllDoctorsDTO();
            _patients = patientController.GetAllPatients();
            _rooms = roomController.GetAllRooms();
            _freeAppointments = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };

            doctorsComboBox.ItemsSource = _doctors;
            patientsComboBox.ItemsSource = _patients;
            roomsComboBox.ItemsSource = _rooms;
            appointmentsComboBox.ItemsSource = _freeAppointments;

            List<string> duration = new List<string>() { "30 minuta", "60 minuta", "90 minuta" };
            durationComboBox.ItemsSource = duration;

            SetValuesForSelectedAppointment();
        }

        private void UpdateExamination_Click(object sender, RoutedEventArgs e)
        {
            if (doctorsComboBox.SelectedItem == null || datePicker.SelectedDate == null || appointmentsComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show(TranslationSource.Instance["FillFieldsMessage"]);
                return;
            }

            UpdateAppointmentFromUserInput();
            if (IsAppointmentValid())
            {
                AppointmentController appointmentController = new AppointmentController();
                appointmentController.UpdateAppointment(_appointment);
                ViewAppointments.GetInstance().RefreshView();

                NavigationService.Navigate(ViewAppointments.GetInstance());
            }
        }

        private void UpdateAppointmentFromUserInput()
        {
            string dateAndTime = datePicker.Text + " " + appointmentsComboBox.Text;
            DateTime appointmentDateAndTime = DateTime.Parse(dateAndTime);
            _appointment.StartTime = appointmentDateAndTime;
            _appointment.InitialTime = _appointment.StartTime;

            if (durationComboBox.SelectedIndex == 0)
                _appointment.Duration = 30;
            else if (durationComboBox.SelectedIndex == 1)
                _appointment.Duration = 60;
            else
                _appointment.Duration = 90;

            _appointment.Room = _rooms[roomsComboBox.SelectedIndex];
            _appointment.Patient = _patients[patientsComboBox.SelectedIndex];
            _appointment.Doctor = _doctors[doctorsComboBox.SelectedIndex];
        }

        private bool IsAppointmentValid()
        {
            AppointmentController appointmentController = new AppointmentController();
            List<Appointment> appointments = appointmentController.GetAllAppointments();
            if (doctorController.OnVacation(_appointment.Doctor, _appointment.StartTime))
            {
                CustomMessageBox.Show(TranslationSource.Instance["DoctorOnVacationMessage"]);
                return false;
            }
            foreach (Appointment a in appointments)
            {
                if (a.GetEndTime() > _appointment.StartTime && a.StartTime < _appointment.GetEndTime() && !a.AppointmentID.Equals(_appointment.AppointmentID))
                {
                    if (a.Doctor.Jmbg.Equals(_appointment.Doctor.Jmbg))
                    {
                        CustomMessageBox.Show(TranslationSource.Instance["DoctorUnavailableMessage"]);
                        return false;
                    }
                    else if (a.Room.Number.Equals(_appointment.Room.Number))
                    {
                        CustomMessageBox.Show(TranslationSource.Instance["RoomUnavailableMessage"]);
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
            datePicker.DisplayDate = _appointment.StartTime;
            datePicker.Text = _appointment.StartTime.ToString("dd.MM.yyyy.");
        }

        private void SetDurateionValue()
        {
            if (_appointment.Duration == 30)
                durationComboBox.SelectedIndex = 0;
            else if (_appointment.Duration == 60)
                durationComboBox.SelectedIndex = 1;
            else
                durationComboBox.SelectedIndex = 2;
        }

        private void SetRoomValue()
        {
            int index = 0;
            foreach (Room r in _rooms)
            {
                if (r.Number.Equals(_appointment.Room.Number))
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
                if (p.Jmbg.Equals(_appointment.Patient.Jmbg))
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
                if (a.Equals(_appointment.GetAppointmentTime()))
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
                if (d.Jmbg.Equals(_appointment.Doctor.Jmbg))
                {
                    break;
                }
                index++;
            }
            doctorsComboBox.SelectedIndex = index;
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ViewAppointments.GetInstance());
        }
    }
}
