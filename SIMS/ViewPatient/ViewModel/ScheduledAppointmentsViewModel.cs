﻿using SIMS.Commands;
using SIMS.Controller;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SIMS.PacijentGUI.ViewModel
{
    public class ScheduledAppointmentsViewModel:ViewModelPatient
    {
        public ObservableCollection<Appointment> PatientAppointments { get; set; }
        private Patient patient;
        private AppointmentController appointmentController;
        public RelayCommand DeleteAppointmentCommand { get; set; }
        public RelayCommand EditAppointmentCommand { get; set; }
        public Appointment SelectedItem { get; set; }

        public ScheduledAppointmentsViewModel(Patient patient)
        {
            this.patient = patient;
            appointmentController = new AppointmentController();
            PatientAppointments=new ObservableCollection<Appointment>(appointmentController.GetFutureAppointments(patient));
            DeleteAppointmentCommand = new RelayCommand(Execute_DeleteAppointmentCommand,CanExecute_DeleteAppointmentCommand);
            EditAppointmentCommand = new RelayCommand(Execute_EditAppointmentCommand, CanExecute_EditAppointmentCommand);
            LoadPatientAndDoctorData();
            
        }

        

        #region actions
        public bool CanExecute_DeleteAppointmentCommand(object obj)
        {
            return true;
        }

         public void Execute_DeleteAppointmentCommand(object obj)
        {
            if (new AntiTrollController().CanChangeAppointment(patient) == false)
            {
                banUser();
            }
            else
            {
                Appointment appointment = GetOriginalAppointment();
                appointment.Patient = patient;
                appointmentController.CancelAppointment(appointment);
                RemoveFromView(appointment);
                
            }
            
        }
       
        public bool CanExecute_EditAppointmentCommand(object obj)
        {
            return true;
        }

        public void Execute_EditAppointmentCommand(object obj)
        {
            if (new AntiTrollController().CanChangeAppointment(patient) == false)
            {
                banUser();
            }
            else
            {
                Appointment appointment = GetOriginalAppointment();
                appointment.Patient = patient;

                IzmjenaPregleda izmjenaPregleda = new IzmjenaPregleda(appointment);
                PocetnaStranica poc = PocetnaStranica.getInstance();
                poc.frame.Content = izmjenaPregleda;
            }
        }
        #endregion
      

        #region helper functions

        private void LoadPatientAndDoctorData()
        {
            foreach (Appointment appointment in PatientAppointments)
            {
                appointment.Patient = new PatientController().GetPatient(appointment.Patient.Jmbg);
                appointment.Doctor = new DoctorController().GetDoctor(appointment.Doctor.Jmbg);
            }
        }

        private void RemoveFromView(Appointment appointment)
        {
            for (int i = 0; i < PatientAppointments.Count; i++)
            {
                if (PatientAppointments[i].AppointmentID == SelectedItem.AppointmentID)
                {
                    PatientAppointments.RemoveAt(i);

                }
            }
        }

        private Appointment GetOriginalAppointment()
        {
            for (int i = 0; i < PatientAppointments.Count; i++)
            {
                if (PatientAppointments[i].AppointmentID == SelectedItem.AppointmentID)
                {
                    return PatientAppointments[i];

                }
            }
            return null;
        }
        #endregion


        #region constructor
        private void banUser()
        {
            ObavjestenjeOTerminu o = new ObavjestenjeOTerminu();
            o.Height = 250;
            o.TekstObavjestenja.Text = "Zbog učestalih izmjena zakazanih termina Vaš nalog je blokiran. Za dodatne informacije obratite se sekretaru bolnice!";
            o.ShowDialog();
            new AntiTrollController().BanUser(patient);
            new MainWindow().Show();
            PocetnaStranica.getInstance().Close();
            PocetnaStranica.getInstance().SetInstance();
        }

        #endregion




    }
}