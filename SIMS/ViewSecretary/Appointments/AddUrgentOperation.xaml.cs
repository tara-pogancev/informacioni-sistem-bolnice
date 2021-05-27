﻿using SIMS.Repositories.SecretaryRepo;
using SIMS.Repositories.AppointmentRepo;
using SIMS.Repositories.DoctorRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using SIMS.Model;
using SIMS.Controller;
using SIMS.ViewSecretary.Patients;

namespace SIMS.ViewSecretary.Appointments
{
    public partial class AddUrgentOperation : Page
    {
        private List<string> SpecializationList;
        private List<Specialization> SpecializationEnumList;
        private List<string> DurationList = new List<string>() { "30 minuta", "60 minuta", "90 minuta" };
        private ObservableCollection<Patient> PatientList;
        private DoctorController doctorController = new DoctorController();

        private ObservableCollection<Appointment> AvailableAppointments;
        public AddUrgentOperation()
        {
            InitializeComponent();

            DataContext = this;
            DurationComboBox.SelectedIndex = 0;

            AvailableAppointments = new ObservableCollection<Appointment>();
            AvailableComboBox.DataContext = AvailableAppointments;
            SpecializationList = DoctorFileRepository.Instance.GetAvailableSpecializationString();
            SpecializationList.Remove("Lekar opšte prakse");
            SpecializationEnumList = DoctorFileRepository.Instance.GetAvailableSpecialization();
            SpecializationEnumList.Remove(Specialization.OpstaPraksa);
            PatientList = new ObservableCollection<Patient>(PatientFileRepository.Instance.GetAll());

            SpecializationComboBox.ItemsSource = SpecializationList;
            DurationComboBox.ItemsSource = DurationList;
            AvailableComboBox.ItemsSource = AvailableAppointments;
            PatientComboBox.ItemsSource = PatientList;
        }

        private Specialization GetSelectedSpecialization()
        {
            int idx = SpecializationComboBox.SelectedIndex;
            if (idx == -1)
                return SpecializationEnumList[0];
            else return SpecializationEnumList[idx];
        }

        private int GetSelectedDuration()
        {
            int idx = DurationComboBox.SelectedIndex;

            if (idx == -1)
                return 0;
            else return (idx + 1) * 30;

        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            if (PatientComboBox.SelectedItem != null && DurationComboBox.SelectedItem != null && SpecializationComboBox.SelectedItem != null)
            {
                Appointment selectedApp = (Appointment)AvailableComboBox.SelectedItem;
                //selectedApp.InitData();
                if (AvailableAppointments.Count != 1)
                {
                    MovePreviousAppointment(selectedApp);
                }
                AppointmentFileRepository.Instance.Save(selectedApp);

                SendNotification(selectedApp, false);

                ViewAppointments.GetInstance().RefreshView();

                this.NavigationService.Navigate(ViewAppointments.GetInstance());
                MessageBox.Show("Hitna operacija uspesno zakazana!");
            }
        }

        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(ViewAppointments.GetInstance());
        }

        private void Button_Guest(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddGuestPatient());
            NavigationService.Navigated += NavigationService_Navigated;
        }

        private void SendNotification(Appointment selectedApp, bool moved)
        {
            List<string> target = new List<string>(); ;

            target.Add(selectedApp.Patient.Jmbg);
            target.Add(selectedApp.Doctor.Jmbg);
            foreach (Secretary s in SecretaryFileRepository.Instance.GetAll())
            {
                target.Add(s.Jmbg);
            }
            if (moved)
            {
                Notification notification = new Notification("Sekretarijat", DateTime.Now,
                ("Pomeren/a " + selectedApp.Type.ToString() + " [" + selectedApp.GetAppointmentDate() + " " + selectedApp.GetAppointmentTime() + ", " + selectedApp.Room.Number + "] za pacijenta "
                + selectedApp.GetPatientName() + ", vodeći lekar " + selectedApp.GetDoctorName() + "."), target);
                NotificationFileRepository.Instance.Save(notification);
            }
            else
            {
                Notification notification = new Notification("Sekretarijat", DateTime.Now,
                ("Zakazana hitna operacija [" + selectedApp.GetAppointmentDate() + " " + selectedApp.GetAppointmentTime() + ", " + selectedApp.Room.Number + "] za pacijenta "
                + selectedApp.GetPatientName() + ", vodeći lekar " + selectedApp.GetDoctorName() + "."), target);
                NotificationFileRepository.Instance.Save(notification);
            }
        }

        private void DurationChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }

        private void SpecializationChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }
        private void PatientChange(object sender, SelectionChangedEventArgs e)
        {
            GetAvailableAppointments();
        }

        private void GetAvailableAppointments()
        {
            AvailableAppointments.Clear();

            if (PatientComboBox.SelectedItem != null && DurationComboBox.SelectedItem != null && SpecializationComboBox.SelectedItem != null)
            {
                Appointment appointmentValues = new Appointment(DateTime.MinValue, GetSelectedDuration(), AppointmentType.surgery, null, (Patient)PatientComboBox.SelectedItem, null);
                List<Appointment> allAppointments = GetAvailableAppointmentsForAllDoctors(appointmentValues, 2);
                if (allAppointments.Count == 1)
                {
                    zakaziButton.Content = "ZAKAŽI";
                    allAppointments[0].InitData();
                    AvailableAppointments.Add(allAppointments[0]);
                }
                else
                {
                    zakaziButton.Content = "POMERI I\nZAKAŽI";
                    SortAppointments(allAppointments);

                    foreach (Appointment app in allAppointments)
                    {
                        app.InitData();
                        AvailableAppointments.Add(app);
                        /*if (AvailableAppointments.Count >= 1)
                            break;*/
                    }
                }
            }

            AvailableComboBox.ItemsSource = AvailableAppointments;
        }

        private List<Appointment> GetAvailableAppointmentsForAllDoctors(Appointment appointmentValues, int numberOfDays)
        {
            List<Appointment> retVal = new List<Appointment>();
            List<Appointment> allAppointments = new List<Appointment>();

            foreach (Doctor doctor in DoctorFileRepository.Instance.ReadBySpecialization(GetSelectedSpecialization()))
            {
                List<DateTime> potentialAppointmentTimeList = GetNearPotentialAppointments(numberOfDays);
                //int counterByDoctor = 0;

                foreach (DateTime appTime in potentialAppointmentTimeList)
                {
                    //TODO: Promeniti prostoriju!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                    Appointment appointment = new Appointment(appTime, appointmentValues.Duration, appointmentValues.Type, doctor, appointmentValues.Patient, RoomFileRepository.Instance.GetAll()[0]);
                    allAppointments.Add(appointment);
                    if (doctorController.CheckIfFree(doctor, appointment) && appointment.StartTime >= appointmentValues.StartTime)
                    {
                        //counterByDoctor++;
                        retVal.Add(appointment);
                        goto Exit;
                    }

                    /*if (counterByDoctor >= 5)
                        break;*/
                }
            }
        Exit:
            if (retVal.Count == 0)
                return allAppointments;
            else
                return retVal;
        }

        private void SortAppointments(List<Appointment> appointments)
        {
            for (int i = 0; i < appointments.Count - 1; i++)
                for (int j = 0; j < appointments.Count - i - 1; j++)
                    if (appointments[j].StartTime > appointments[j + 1].StartTime)
                    {
                        var temp = appointments[j];
                        appointments[j] = appointments[j + 1];
                        appointments[j + 1] = temp;
                    }
        }

        private List<DateTime> GetNearPotentialAppointments(int numberOfDays)
        {
            DateTime currentTime = DateTime.Now;

            List<string> availableTimes = new List<string>() { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00" };
            List<string> availableDates = new List<string>();
            for (int i = 0; i < numberOfDays; i++)
            {
                DateTime currentDate = DateTime.Today.AddDays(i);
                availableDates.Add(currentDate.ToString("dd.MM.yyyy."));
            }

            List<DateTime> potentialAppointmentTimeList = new List<DateTime>();
            foreach (string date in availableDates)
                foreach (string time in availableTimes)
                {
                    string dateAndTime = date + " " + time;
                    DateTime appointmentTime = DateTime.Parse(dateAndTime);
                    if (appointmentTime >= currentTime)
                        potentialAppointmentTimeList.Add(appointmentTime);
                    if (numberOfDays == 2 && potentialAppointmentTimeList.Count > numberOfDays)
                        goto Exit;
                }
            Exit:
            return potentialAppointmentTimeList;
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            PatientList = new ObservableCollection<Patient>(PatientFileRepository.Instance.GetAll());
            PatientComboBox.ItemsSource = PatientList;
        }

        private void MovePreviousAppointment(Appointment selectedApp)
        {
            List<Appointment> appointmentsForMoving = FindReservedAppointments(selectedApp);
            foreach (Appointment app in appointmentsForMoving)
            {
                MoveAppointmentToNearestDate(app);
            }
        }

        private void MoveAppointmentToNearestDate(Appointment appointment)
        {
            List<Appointment> appointments = GetAvailableAppointmentsForAllDoctors(appointment, 10);
            AppointmentFileRepository.Instance.Delete(appointment.AppointmentID);
            AppointmentFileRepository.Instance.Save(appointments[0]);
            SendNotification(appointments[0], true);
        }

        private List<Appointment> FindReservedAppointments(Appointment selectedApp)
        {
            List<Appointment> appointmentsForMoving = new List<Appointment>();
            List<Appointment> allAppointments = AppointmentFileRepository.Instance.GetAll();
            foreach (Appointment app in allAppointments)
            {
                if (app.GetEndTime() > selectedApp.StartTime && app.StartTime < selectedApp.GetEndTime())
                    appointmentsForMoving.Add(app);

            }
            return appointmentsForMoving;
        }
    }
}